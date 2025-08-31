import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState, AppDispatch } from "../../store/store";
import { getModels, setParams } from "../../store/modelsSlice";
import { saveFilters, loadFilters } from "../../utils/filterState";
import SortableTh from "../../components/SortableTh";
import ListBar from "../../components/ListBar";
import { Link } from "react-router-dom"; 
import type {VehicleModel } from "../../types/index";

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

  useEffect(() => {
    const ctrl = new AbortController();
    dispatch(getModels({ ...filters }));
    saveFilters(KEY, filters);
    return () => ctrl.abort();
  }, [dispatch, filters]); 

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

      <Link to="./models/new">
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
            {data.map((model: VehicleModel) => (
              <tr key={model.id}>
                <td key={model.name}>{model.name}</td>
                <td key={model.abrv}>{model.abrv}</td>
                <td key={model.makeId}>{model.makeId}</td>
                <td>
                  <Link to={"./models/${model.id}"}>
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