import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { VehicleApi } from "../../api/vehicleService";

export default function OwnerEdit() {
  const { id } = useParams<{ id: string }>();
  const nav = useNavigate();
  const isNew = id === "new";
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [DOB, setDOB] = useState<string>("1900-01-01");
  const [loading, setLoading] = useState(isNew);
  
  useEffect(() => {
    if (!isNew && id) {
      (async () => {
        setLoading(true);
        const o = await VehicleApi.getOwner(Number(id));
        setFirstName(o.firstName);
        setLastName(o.lastName);
		setDOB(o.DOB);
        setLoading(false);
      })();
    }
  }, [id, isNew]);

  const save = async () => {
    if (isNew) {
      await VehicleApi.createOwner({ firstName, lastName, DOB });
    } else if (id) {
      await VehicleApi.updateOwner(Number(id), { firstName, lastName, DOB});
    }
    nav("/owners");
  };

  const remove = async () => {
    if (!isNew && id && confirm("Delete this owner?")) {
      await VehicleApi.deleteOwner(Number(id));
      nav("/owners");
    }
  };

  return (
    <div>
      <h2>{isNew ? "New Owner" : "Edit Owner"}</h2>
      <label>First Name<input value={firstName} onChange={(e) => setFirstName(e.target.value)}/></label>
      <label>Last Name<input value={lastName} onChange={(e) => setLastName(e.target.value)}/></label>
      <label>DOB<input value={DOB} onChange={(e) => setDOB(e.target.value)}/></label>
      <div style={{ display: "flex", gap: 8, marginTop: 12 }}>
        <button disabled={loading} onClick={save}>Save</button> 
        {!isNew && <button onClick={remove}>Delete</button>}
        <button onClick={() => nav(-1)}>Cancel</button>
      </div>
    </div>
  );
}