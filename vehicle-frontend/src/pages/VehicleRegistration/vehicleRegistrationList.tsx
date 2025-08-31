import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState, AppDispatch } from "../../store/store";
import { getOwners, setParams } from "../../store/ownersSlice";
import { saveFilters, loadFilters } from "../../utils/filterState";
import SortableTh from "../../components/SortableTh";
import ListBar from "../../components/ListBar";
import { Link } from "react-router-dom";
import type { VehicleRegistration } from "../../types/index";

const KEY = "owners";

export default function OwnersList() {
  const dispatch = useDispatch<AppDispatch>();
  const vehicleOwnerState = useSelector((state: RootState) => state.VehicleRegistration);
  const {
	  data = [],
	  loading = false,
	  filters = {},
	} = vehicleOwnerState ?? {};

  useEffect(() => {
    const restored = loadFilters(KEY, filters);
    dispatch(setParams(restored));
  }, []); 

  useEffect(() => {
    const ctrl = new AbortController();
    dispatch(getOwners({ ...filters }));
    saveFilters(KEY, filters);
    return () => ctrl.abort();
  }, [dispatch, filters]);

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

      <Link to="./owners/new">
        <button>New Vehicle Owner</button>
      </Link>

      {loading && <p>Loading...</p>}

      {data && (
        <table>
          <thead>
            <tr>
              <SortableTh field="registrationNumber" current={filters.sort ?? null} onSort={onSort} label="Registration number" direction={null} />
              <th>Abrv</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {data.map((reg: VehicleRegistration) => (
			  <tr key={reg.id}>
				<td key={reg.registrationNumber}>{reg.registrationNumber}</td>
			  </tr>
			))}
          </tbody>
        </table>
      )}
    </div>
  );
}