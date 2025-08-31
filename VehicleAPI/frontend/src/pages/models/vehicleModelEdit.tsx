import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { VehicleApi } from "../../api/vehicleService";
import { http } from "../../api/http";

export default function ModelEdit() {
  const { id } = useParams<{ id: string }>();
  const nav = useNavigate();
  const isNew = id === "new";
  const [name, setName] = useState<string>("");
  const [makeId, setMakeId] = useState<number>(0);
  const [loading, setLoading] = useState(isNew);
  useEffect(() => {
    if (!isNew && id) {
      (async () => {
        setLoading(true);
        const m = await VehicleApi.getModel(Number(id));
        setName(m.name);
		setMakeId(m.makeId);
        setLoading(false);
      })();
    }
  }, [id, isNew]);

  const save = async () => {
    if (isNew) {
      await http.post("/VehicleModel", { name, makeId });
    } else if (id) {
        await http.put("/VehicleModel", { id, name, VehicleMakeId: makeId});
    }
    nav("/models");
  };

  const remove = async () => {
    if (!isNew && id && confirm("Delete this model?")) {
      await VehicleApi.deleteModel(Number(id));
      nav("/models");
    }
  };

  return (
    <div>
      <h2>{isNew ? "New Model" : "Edit Model"}</h2>
      <label>Name<input value={name} onChange={(e) => setName(e.target.value)}/></label>
      <label>Make ID<input value={makeId} onChange={(e) => setMakeId(Number(e.target.value))}/></label>
      <div style={{ display: "flex", gap: 8, marginTop: 12 }}>
        <button disabled={!loading} onClick={save}>Save</button> 
        {!isNew && <button onClick={remove}>Delete</button>}
        <button onClick={() => nav(-1)}>Cancel</button>
      </div>
    </div>
  );
}