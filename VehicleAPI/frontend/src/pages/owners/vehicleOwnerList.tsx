import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState, AppDispatch } from "../../store/store";
import { setParams } from "../../store/ownersSlice";
import { loadFilters } from "../../utils/filterState";
import SortableTh from "../../components/SortableTh";
import ListBar from "../../components/ListBar";
import { Link } from "react-router-dom";
import type { VehicleOwner } from "../../types/index";
import axios from "axios";

const KEY = "owners";

export default function OwnersList() {
  const dispatch = useDispatch<AppDispatch>();
  const vehicleOwnerState = useSelector((state: RootState) => state.VehicleOwner);
  const {
	  data = [],
	  loading = false,
	  filters = {},
	} = vehicleOwnerState ?? {};

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
	http.get("/api/VehicleOwner")
		.then(r => setItems(r.data))
		.catch(console.error);
  }, []); 


  const onSort = (sort: string) => dispatch(setParams({ sort }));
  const onQChange = (q: string) => dispatch(setParams({ q, page: 1 }));
  const onPageChange = (page: number) => dispatch(setParams({ page }));

  return (
    <div>
      <h2>Vehicle Owners</h2>

      <ListBar
        q={filters.q ?? ""}
        onQChange={onQChange}
        page={filters.page ?? 1}
        pageSize={filters.pageSize ?? 10}
        total={data?.length ?? 0}
        onPageChange={onPageChange}
      />

      <Link to="./new">
        <button>New Vehicle Owner</button>
      </Link>

      {loading && <p>Loading...</p>}

      {data && (
        <table>
          <thead>
            <tr>
              <SortableTh field="LastName" current={filters.sort ?? null} onSort={onSort} label="Last name" direction={null} />
              <th>Abrv</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {items.map((owner: VehicleOwner) => (
			  <tr key={owner.id}>
				<td key={owner.firstName}>{owner.firstName}</td>
				<td key={owner.lastName}>{owner.lastName}</td>
				<td key={owner.DOB}>{owner.DOB}</td>
				<td>
				  <Link to={"./${owner.id}"}>
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