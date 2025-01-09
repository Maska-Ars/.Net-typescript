import React, { useState, useEffect } from "react";
import { Modal, Button, Form, ListGroup } from "react-bootstrap";
import entitiesStore, { Record, Release } from "../../Store/EntitiesStore";
import "./RecordModal.css";

interface Props {
  show: boolean;
  onHide: () => void;
  isEditMode: boolean;
  setRecord: (record: Record | undefined) => void;
}

const RecordModal: React.FC<Props> = ({
  show,
  onHide,
  isEditMode,
  setRecord,
}) => {
  const [name, setName] = useState("");
  const [durationSeconds, setDurationSeconds] = useState<number | null>(null);
  const [selectedReleaseId, setSelectedReleaseId] = useState<string | null>(
    null
  );
  const [selectedRecord, setSelectedRecord] = useState<Record | undefined>(
    undefined
  );

  useEffect(() => {
    if (isEditMode && selectedRecord) {
      setName(selectedRecord.name);
      setDurationSeconds(selectedRecord.durationSeconds);
      setSelectedReleaseId(selectedRecord.releaseId);
    } else {
      setName("");
      setDurationSeconds(null);
      setSelectedReleaseId(null);
    }
  }, [selectedRecord, isEditMode]);

  useEffect(() => {
    if (show) {
      entitiesStore.fetchEntities();
    }
  }, [show]);

  const handleRecordSelect = (record: Record) => {
    setSelectedRecord(record);
    setName(record.name);
    setDurationSeconds(record.durationSeconds);
    setSelectedReleaseId(record.releaseId);
  };

  const handleSubmit = async () => {
    if (isEditMode && selectedRecord) {
      if (selectedReleaseId && durationSeconds) {
        await entitiesStore.updateRecord(selectedRecord.id, {
          name,
          durationSeconds,
          releaseId: selectedReleaseId,
        });
      }
    } else {
      if (selectedReleaseId && durationSeconds) {
        await entitiesStore.addRecord({
          name,
          durationSeconds,
          releaseId: selectedReleaseId,
        });
      }
    }
    setRecord(undefined);
    setSelectedRecord(undefined);
    setName("");
    setDurationSeconds(null);
    setSelectedReleaseId(null);
    onHide();
  };
  const handleDelete = async () => {
    if (isEditMode && selectedRecord) {
      await entitiesStore.deleteRecord(selectedRecord.id);
    }
    onHide();
  };

  return (
    <Modal
      className="RecordModal"
      show={show}
      onHide={() => {
        setRecord(undefined);
        setSelectedRecord(undefined);
        setName("");
        setDurationSeconds(null);
        setSelectedReleaseId(null);
        onHide();
      }}
      size="lg"
    >
      <Modal.Header closeButton>
        <Modal.Title>{isEditMode ? "Edit Record" : "Add Record"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="RecordBody">
          <div style={{ width: "100%" }}>
            <ListGroup style={{ marginTop: "1rem" }}>
              {entitiesStore.records.map((record) => (
                <ListGroup.Item
                  key={record.id}
                  onClick={() => handleRecordSelect(record)}
                  active={selectedRecord?.id === record.id}
                  style={{ cursor: "pointer" }}
                >
                  {record.name}
                </ListGroup.Item>
              ))}
            </ListGroup>
          </div>
          <div style={{ width: "100%" }}>
            <Form>
              <Form.Group className="mb-3">
                <Form.Label>Название</Form.Label>
                <Form.Control
                  type="text"
                  className="field"
                  placeholder="Введите название"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Длительность</Form.Label>
                <Form.Control
                  type="number"
                  className="field"
                  placeholder="Введите длительность в секундах"
                  value={
                    durationSeconds === null ? "" : durationSeconds.toString()
                  }
                  onChange={(e) =>
                    setDurationSeconds(
                      e.target.value === ""
                        ? null
                        : parseInt(e.target.value, 10)
                    )
                  }
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Релиз: </Form.Label>
                <Form.Control
                  as="select"
                  value={selectedReleaseId || ""}
                  onChange={(e) => setSelectedReleaseId(e.target.value)}
                >
                  <option value="">Выбреите релиз</option>
                  {entitiesStore.releases.map((release) => (
                    <option key={release.id} value={release.id}>
                      {release.name}
                    </option>
                  ))}
                </Form.Control>
              </Form.Group>
            </Form>
          </div>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>
          Отменить
        </Button>
        {isEditMode && selectedRecord ? (
          <>
            <Button variant="danger" onClick={handleDelete}>
              Удалить
            </Button>
            <Button variant="primary" onClick={handleSubmit}>
              Изменить
            </Button>
          </>
        ) : (
          <Button variant="primary" onClick={handleSubmit}>
            Добавить
          </Button>
        )}
      </Modal.Footer>
    </Modal>
  );
};

export default RecordModal;
