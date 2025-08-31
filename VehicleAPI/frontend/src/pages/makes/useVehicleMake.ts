import { useCallback, useEffect, useState } from "react";
import {
  getVehicleMakes,
  createVehicleMake,
  updateVehicleMake,
  deleteVehicleMake,
} from "./vehicleMakeApi";
import type { VehicleMake, ListParams } from "../../types";

interface UseVehicleMakeOptions {
  initialFilters?: ListParams;
}

export function useVehicleMake(options?: UseVehicleMakeOptions) {
  const [data, setData] = useState<VehicleMake[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [filters, setFilters] = useState<ListParams>(() => {
    const saved = localStorage.getItem('vehicleMakeFilters');
    return (
      options?.initialFilters ??
      (saved ? JSON.parse(saved) : { page: 1, pageSize: 10, sortField: 'name', sortOrder: 'asc', filters: {} })
    );
  });

  const loadData = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      localStorage.setItem('vehicleMakeFilters', JSON.stringify(filters));
        const response = await getVehicleMakes(filters);
        console.log(response);
      setData(response.items);
      setTotal(response.total);
    } catch (err: any) {
      setError(err.message || 'Error loading vehicle makes');
    } finally {
      setLoading(false);
    }
  }, [filters]);

  useEffect(() => {
      loadData();
  }, []);

  const create = async (make: VehicleMake) => {
    await createVehicleMake(make);
    await loadData();
  };

  const update = async (id: number, make: VehicleMake) => {
    await updateVehicleMake(id, make);
    await loadData();
  };

  const remove = async (id: number) => {
    await deleteVehicleMake(id);
    await loadData();
  };

  return {
    data,
    total,
    loading,
    error,
    filters,
    setFilters,
    reload: loadData,
    create,
    update,
    remove,
  };
}
