import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState, AppDispatch } from "../../store/store";
import { getMakes, setParams } from "../../store/makesSlice";
import { saveFilters, loadFilters } from "../../utils/filterState";
import SortableTh from "../../components/SortableTh";
import ListBar from "../../components/ListBar";
import { Link } from "react-router-dom"; 
import type { VehicleMake } from "../../types/index";

const KEY = "makes";

export default function MakesList() {
  const dispatch = useDispatch<AppDispatch>();
  const vehicleMakeState = useSelector((state: RootState) => state.VehicleMake);
  const {
	  data = [],
	  loading = false,
	  filters = {},
	} = vehicleMakeState ?? {};

  useEffect(() => {
    const restored = loadFilters(KEY, filters);
    dispatch(setParams(restored));
  }, []); 

  useEffect(() => {
    const ctrl = new AbortController();
    dispatch(getMakes({ ...filters }));
    saveFilters(KEY, filters);
    return () => ctrl.abort();
  }, [dispatch, filters]); 

  const onSort = (sort: string) => dispatch(setParams({ sort }));
  const onQChange = (q: string) => dispatch(setParams({ q, page: 1 }));
  const onPageChange = (page: number) => dispatch(setParams({ page }));

  return (
    <div>
      <h2>Vehicle Makes</h2>

      <ListBar
        q={filters.q ?? ""}
        onQChange={onQChange}
        page={filters.page ?? 1}
        pageSize={filters.pageSize ?? 10}
        total={data?.length ?? 0}
        onPageChange={onPageChange}
      />

      <Link to="./makes/new">
        <button>New Vehicle Make</button>
      </Link>

      {loading && <p>Loading...</p>}

      {data && (
        <table>
          <thead>
            <tr>
              <SortableTh field="name" current={filters.sort ?? null} onSort={onSort} label="Name" direction={null} />
              <th>Abrv</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {data.map((make: VehicleMake) => (
              <tr key={make.id}>
                <td key={make.name}>{make.name}</td>
                <td key={make.abrv}>{make.abrv}</td>
                <td>
                  <Link to={"./makes/${make.id}"}>
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