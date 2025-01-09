import React, { useEffect } from "react";
import { useObserver } from "mobx-react-lite";
import entitiesStore, { Release, Record } from "../../Store/EntitiesStore";
import "./Card.css";

interface Props {
  release: Release;
}

export const toMinute = (secs: number) => {
  const minutes = Math.floor(secs / 60);
  const rem = secs % 60;
  return `${String(minutes).padStart(2, "0")}:${String(rem).padStart(2, "0")}`;
};

export default function Card({ release }: Props) {
  useEffect(() => {
    entitiesStore.fetchArtist(release.artistId);
    entitiesStore.fetchRecordsByReleaseId(release.id);
  }, [release.artistId, release.id]);

  return useObserver(() => {
    const artist = entitiesStore.artists.find((a) => a.id === release.artistId);
    const records = entitiesStore.records.filter(
      (r) => r.releaseId === release.id
    );
    const { loading, error } = entitiesStore;

    if (loading) {
      return <div>Loading...</div>;
    }
    if (error) {
      return <div>Error: {error}</div>;
    }
    return (
      <div className="CardCont">
        <span className="Titles">
          <h2>{release.name}</h2>
          <h2>{artist?.name}</h2>
        </span>
        <div className="SecondRow">
          <img className="ReleaseImage" src={release.image}></img>
          {records.length > 0 ? (
            <ul>
              {records.map((record) => (
                <li className="recRow" key={record.id}>
                  <h2>{toMinute(record.durationSeconds)}</h2>
                  <h2>{record.name}</h2>
                </li>
              ))}
            </ul>
          ) : (
            <p>Песни этого релиза пока не доступны.</p>
          )}
          <h3 className="country">{artist?.country}</h3>
        </div>
      </div>
    );
  });
}
