import React, { useState, useEffect } from "react";
import { Modal, Button, Form, ListGroup } from "react-bootstrap";
import entitiesStore, { Artist } from "../../Store/EntitiesStore";
import "./ArtistModalWIndow.css";

interface Props {
  show: boolean;
  onHide: () => void;
  isEditMode: boolean;
  setArtist: (artist: Artist | undefined) => void;
}

const ArtistModal: React.FC<Props> = ({
  show,
  onHide,
  isEditMode,
  setArtist,
}) => {
  const [name, setName] = useState("");
  const [country, setCountry] = useState("");
  const [selectedArtist, setSelectedArtist] = useState<Artist | undefined>(
    undefined
  );

  useEffect(() => {
    if (isEditMode && selectedArtist) {
      setName(selectedArtist.name);
      setCountry(selectedArtist.country);
    } else {
      setName("");
      setCountry("");
    }
  }, [selectedArtist, isEditMode]);
  useEffect(() => {
    if (show) {
      entitiesStore.fetchEntities();
    }
  }, [show]);

  const handleArtistSelect = (artist: Artist) => {
    setSelectedArtist(artist);
    setName(artist.name);
    setCountry(artist.country);
  };
  const handleSubmit = async () => {
    if (isEditMode && selectedArtist) {
      await entitiesStore.updateArtist(selectedArtist.id, { name, country });
    } else {
      await entitiesStore.addArtist({ name, country });
    }
    setArtist(undefined);
    setSelectedArtist(undefined);
    setName("");
    setCountry("");
    onHide();
  };
  const handleDelete = async () => {
    if (isEditMode && selectedArtist) {
      await entitiesStore.deleteArtist(selectedArtist.id);
    }
    onHide();
  };

  return (
    <Modal
      className="ArtistModalWindow"
      show={show}
      onHide={() => {
        setName("");
        setArtist(undefined);
        setSelectedArtist(undefined);
        setCountry("");
        onHide();
      }}
      size="lg"
    >
      <Modal.Header closeButton>
        <Modal.Title>Исполнители</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="ArtistBody">
          <div className="Form">
            {isEditMode ? (
              <ListGroup style={{ marginTop: "1rem" }}>
                {entitiesStore.artists.map((artist) => (
                  <ListGroup.Item
                    key={artist.id}
                    onClick={() => handleArtistSelect(artist)}
                    active={selectedArtist?.id === artist.id}
                    style={{ cursor: "pointer" }}
                  >
                    {artist.name} - {artist.country}
                  </ListGroup.Item>
                ))}
              </ListGroup>
            ) : (
              <></>
            )}
          </div>
          <div style={{ width: "100%" }}>
            <Form>
              <Form.Group className="mb-3">
                <Form.Label>Имя: </Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Введите имя"
                  className="field"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Страна: </Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Введите страну"
                  value={country}
                  className="field"
                  onChange={(e) => setCountry(e.target.value)}
                />
              </Form.Group>
            </Form>
          </div>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button
          variant="secondary"
          onClick={() => {
            setName("");
            setArtist(undefined);
            setSelectedArtist(undefined);
            setCountry("");
            onHide();
          }}
        >
          Отменить
        </Button>
        {isEditMode && selectedArtist ? (
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

export default ArtistModal;
