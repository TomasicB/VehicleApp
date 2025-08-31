import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState, AppDispatch } from "../../store/store";
import { fetchVehicleEngines } from "../../store/vehicleEngineSlice";
import type { IVehicleEngine } from "../../types/vehicleEngine";

const VehicleEngineList = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { items, loading } = useSelector((state: RootState) => state.IVehicleEngine);

  useEffect(() => {
    dispatch(fetchVehicleEngines());
  }, [dispatch]);

  return (
    <div>
      <h1>Vehicle Engines</h1>
      {loading ? <p>Loading...</p> : (
        <table>
          <thead>
            <tr><th>Type</th><th>Abrv</th></tr>
          </thead>
          <tbody>
            {items.map((engine: IVehicleEngine, i: number) => (
              <tr key={i}>
                <td>{engine.type}</td>
                <td>{engine.abrv}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default VehicleEngineList;