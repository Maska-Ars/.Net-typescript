import React, { useState, useEffect } from "react";
import { Modal, Button, Form, ListGroup } from "react-bootstrap";
import entitiesStore, { Release, Artist } from "../../Store/EntitiesStore";
import "./ReleaseModal.css";

interface Props {
  show: boolean;
  onHide: () => void;
  isEditMode: boolean;
  setRelease: (release: Release | undefined) => void;
}

const ReleaseModal: React.FC<Props> = ({
  show,
  onHide,
  isEditMode,
  setRelease,
}) => {
  const [name, setName] = useState("");
  const [image, setImage] = useState<string | null>(null);
  const [selectedArtistId, setSelectedArtistId] = useState<string | null>(null);
  const [selectedRelease, setSelectedRelease] = useState<Release | undefined>(
    undefined
  );

  useEffect(() => {
    if (isEditMode && selectedRelease) {
      setName(selectedRelease.name);
      setImage(selectedRelease.image);
      setSelectedArtistId(selectedRelease.artistId);
    } else {
      setName("");
      setImage(null);
      setSelectedArtistId(null);
    }
  }, [selectedRelease, isEditMode]);

  useEffect(() => {
    if (show) {
      entitiesStore.fetchEntities();
    }
  }, [show]);

  const handleReleaseSelect = (release: Release) => {
    setSelectedRelease(release);
    setName(release.name);
    setImage(release.image);
    setSelectedArtistId(release.artistId);
  };

  const handleSubmit = async () => {
    if (isEditMode && selectedRelease) {
      if (selectedArtistId) {
        await entitiesStore.updateRelease(selectedRelease.id, {
          name,
          image,
          artistId: selectedArtistId,
        });
      }
    } else {
      if (selectedArtistId) {
        await entitiesStore.addRelease({
          name,
          image,
          artistId: selectedArtistId,
        });
      }
      setRelease(undefined);
      setSelectedRelease(undefined);
      setName("");
      setImage(null);
      setSelectedArtistId(null);
    }
    onHide();
  };

  const handleDelete = async () => {
    if (isEditMode && selectedRelease) {
      await entitiesStore.deleteRelease(selectedRelease.id);
    }
    onHide();
  };

  return (
    <Modal
      className="ReleaseModal"
      show={show}
      onHide={() => {
        setRelease(undefined);
        setSelectedRelease(undefined);
        setName("");
        setImage(null);
        setSelectedArtistId(null);
        onHide();
      }}
      size="lg"
    >
      <Modal.Header closeButton>Релизы</Modal.Header>
      <Modal.Body>
        <div className="ReleaseBody">
          <div style={{ width: "100%" }}>
            <ListGroup>
              {entitiesStore.releases.map((release) => (
                <ListGroup.Item
                  key={release.id}
                  onClick={() => handleReleaseSelect(release)}
                  active={selectedRelease?.id === release.id}
                  style={{ cursor: "pointer" }}
                >
                  {release.name}
                </ListGroup.Item>
              ))}
            </ListGroup>
          </div>
          <div style={{ width: "100%" }}>
            <Form>
              <Form.Group className="mb-3">
                <Form.Label>Название: </Form.Label>
                <Form.Control
                  type="text"
                  className="Field"
                  placeholder="Введите название"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Изображение: </Form.Label>
                <Form.Control
                  type="text"
                  className="Field"
                  placeholder="Добавьте url изображения"
                  value={image || ""}
                  onChange={(e) => setImage(e.target.value)}
                />
              </Form.Group>
              <Form.Group className="ArtistSelect">
                <Form.Label>Исполнитель: </Form.Label>
                <Form.Control
                  as="select"
                  value={selectedArtistId || ""}
                  onChange={(e) => setSelectedArtistId(e.target.value)}
                >
                  <option value="">Выберите исполнителя</option>
                  {entitiesStore.artists.map((artist) => (
                    <option key={artist.id} value={artist.id}>
                      {artist.name}
                    </option>
                  ))}
                </Form.Control>
              </Form.Group>
            </Form>
          </div>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button
          variant="secondary"
          onClick={() => {
            setRelease(undefined);
            setSelectedRelease(undefined);
            setName("");
            setImage(null);
            setSelectedArtistId(null);
            onHide();
          }}
        >
          Отменить
        </Button>
        {isEditMode && selectedRelease ? (
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

export default ReleaseModal;
