import { useEffect, useState } from "react";
import "./App.css";
import entitiesStore from "../Store/EntitiesStore";
import NavBar from "../Components/NavBar/NavBar";
import Card from "../Components/Card/Card";
import { observer } from "mobx-react-lite";

const App = observer(() => {
  
  useEffect(() => {
    entitiesStore.fetchEntities();
  }, []);

  return (
    <div className="RootDiv">
      <NavBar></NavBar>
      <div className="Page">
        <h1 className="Releases">Доступные релизы</h1>
      </div>
      <div className="ReleasesCont">
        {entitiesStore.releases.map((release) => (
          <Card release={release}></Card>
        ))}
      </div>
    </div>
  );
});

export default App;
