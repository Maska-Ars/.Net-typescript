import React, { useEffect, useState } from "react";
import "./NavBar.css";
import musicNote from "../../Assets/music-notes-svgrepo-com.svg";
import entitiesStore, {
  Artist,
  Record,
  Release,
} from "../../Store/EntitiesStore";
import { observer } from "mobx-react-lite";
import ArtistModal from "../ArtistModal/ArtistModal";
import ReleaseModal from "../ReleaseModal/ReleaseModal";
import RecordModal from "../RecordModal/RecordModal";

export const NavBar = observer(() => {
  const [showArtistModal, setShowArtistModal] = useState(false);
  const [showReleaseModal, setShowReleaseModal] = useState(false);
  const [showRecordModal, setShowRecordModal] = useState(false);
  const [selectedRelease, setSelectedRelease] = useState<Release | undefined>(
    undefined
  );
  const [selectedRecord, setSelectedRecord] = useState<Record | undefined>(
    undefined
  );
  const [selectedArtist, setSelectedArtist] = useState<Artist | undefined>(
    undefined
  );
  const [isArtistEditMode, setIsArtistEditMode] = useState(false);
  const [isReleaseEditMode, setIsReleaseEditMode] = useState(false);
  const [isRecordEditMode, setIsRecordEditMode] = useState(false);

  useEffect(() => {
    entitiesStore.fetchEntities();
  }, []);

  const handleOpenArtistModal = (isEdit: boolean = false, artist?: Artist) => {
    setSelectedArtist(artist);
    setIsArtistEditMode(isEdit);
    setShowArtistModal(true);
  };

  const handleCloseArtistModal = () => {
    setSelectedArtist(undefined);
    setShowArtistModal(false);
  };
  const handleOpenReleaseModal = (
    isEdit: boolean = false,
    release?: Release
  ) => {
    setSelectedRelease(release);
    setIsReleaseEditMode(isEdit);
    setShowReleaseModal(true);
  };

  const handleCloseReleaseModal = () => {
    setSelectedRelease(undefined);
    setShowReleaseModal(false);
  };
  const handleOpenRecordModal = (isEdit: boolean = false, record?: Record) => {
    setSelectedRecord(record);
    setIsRecordEditMode(isEdit);
    setShowRecordModal(true);
  };

  const handleCloseRecordModal = () => {
    setSelectedRecord(undefined);
    setShowRecordModal(false);
  };

  return (
    <div className="NavCont">
      <h1 className="NavTitle">MusicLib</h1>
      <div className="Section">
        <span
          className="ModalTitle"
          onClick={() => handleOpenArtistModal(true)}
        >
          Исполнители
        </span>
        <span
          className="ModalTitle"
          onClick={() => handleOpenReleaseModal(true)}
        >
          Релизы
        </span>
        <span className="ModalTitle" onClick={() => handleOpenRecordModal(true)}>Записи</span>
      </div>
      <img className="NavImage" src={musicNote}></img>
      <ArtistModal
        show={showArtistModal}
        onHide={handleCloseArtistModal}
        isEditMode={isArtistEditMode}
        setArtist={setSelectedArtist}
      />
      <ReleaseModal
        show={showReleaseModal}
        onHide={handleCloseReleaseModal}
        isEditMode={isReleaseEditMode}
        setRelease={setSelectedRelease}
      />
      <RecordModal
        show={showRecordModal}
        onHide={handleCloseRecordModal}
        isEditMode={isRecordEditMode}
        setRecord={setSelectedRecord}
      />
    </div>
  );
});

export default NavBar;
