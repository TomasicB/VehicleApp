import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState, AppDispatch } from "../../store/store";
import { setParams } from "../../store/modelsSlice";
import { loadFilters } from "../../utils/filterState";
import SortableTh from "../../components/SortableTh";
import ListBar from "../../components/ListBar";
import { Link } from "react-router-dom"; 
import type {VehicleModel } from "../../types/index";
import axios from "axios";

const KEY = "models";

export default function ModelsList() {
  const dispatch = useDispatch<AppDispatch>();
  const vehicleModelState = useSelector((state: RootState) => state.VehicleModel);
  const {
	  data = [],
	  loading = false,
	  filters = {},
	} = vehicleModelState ?? {};

  useEffect(() => {
    const restored = loadFilters(KEY, filters);
    dispatch(setParams(restored));
  }, []); 


const [items, setItems] = useState<any[]>([]);
const http = axios.create({
  baseURL: "https://localhost:44311",
  timeout: 15000,
  withCredentials: false,
});

  useEffect(() => {
	http.get("/api/VehicleModel")
		.then(r => setItems(r.data))
		.catch(console.error);
  }, []); 

  const onSort = (sort: string) => dispatch(setParams({ sort }));
  const onQChange = (q: string) => dispatch(setParams({ q, page: 1 }));
  const onPageChange = (page: number) => dispatch(setParams({ page }));

  return (
    <div>
      <h2>Vehicle Models</h2>

      <ListBar
        q={filters.q ?? ""}
        onQChange={onQChange}
        page={filters.page ?? 1}
        pageSize={filters.pageSize ?? 10}
        total={data?.length ?? 0}
        onPageChange={onPageChange}
      />

      <Link to="./new">
        <button>New Vehicle Model</button>
      </Link>

      {loading && <p>Loading...</p>}

      {data && (
        <table>
          <thead>
            <tr>
              <SortableTh field="name" current={filters.sort ?? null} onSort={onSort} label="Name" direction={null} />
              <th>Abrv</th>
			  <th>MakeId</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {items.map((model: VehicleModel) => (
              <tr key={model.id}>
                <td key={model.name}>{model.name}</td>
                <td key={model.abrv}>{model.abrv}</td>
                <td key={model.makeId}>{model.makeId}</td>
                <td>
                  <Link to={"./${model.id}"}>
                    <button>Edit</button>
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}