import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { VehicleApi } from "../../api/vehicleService";

export default function MakeEdit() {
  const { id } = useParams<{ id: string }>();
  const nav = useNavigate();
  const isNew = id === "new";

  const [name, setName] = useState("");
  const [loading, setLoading] = useState(isNew);
  useEffect(() => {
    if (!isNew && id) {
      (async () => {
        setLoading(true);
        const m = await VehicleApi.getMake(Number(id));
        setName(m.name);
        setLoading(false);
      })();
    }
  }, [id, isNew]);

  const save = async () => {
    if (isNew) {
      await VehicleApi.createMake({ name });
    } else if (id) {
      await VehicleApi.updateMake(Number(id), { name });
    }
    nav("/makes");
  };

  const remove = async () => {
    if (!isNew && id && confirm("Delete this make?")) {
      await VehicleApi.deleteMake(Number(id));
      nav("/makes");
    }
  };

  return (
    <div>
      <h2>{isNew ? "New Make" : "Edit Make"}</h2>
      <label>Name<input value={name} onChange={(e) => setName(e.target.value)}/></label>
      <div style={{ display: "flex", gap: 8, marginTop: 12 }}>
        <button disabled={loading} onClick={save}>Save</button> 
        {!isNew && <button onClick={remove}>Delete</button>}
        <button onClick={() => nav(-1)}>Cancel</button>
      </div>
    </div>
  );
}