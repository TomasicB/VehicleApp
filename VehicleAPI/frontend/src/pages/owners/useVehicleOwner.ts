import { useCallback, useEffect, useState } from "react";
import {
  getVehicleOwners,
  createVehicleOwner,
  updateVehicleOwner,
  deleteVehicleOwner,
} from "./vehicleOwnerApi";
import type { VehicleOwner, ListParams } from "../../types";

interface UseVehicleOwnerOptions {
  initialFilters?: ListParams;
}

export function useVehicleOwner(options?: UseVehicleOwnerOptions) {
  const [data, setData] = useState<VehicleOwner[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [filters, setFilters] = useState<ListParams>(() => {
    const saved = localStorage.getItem('vehicleOwnerFilters');
    return (
      options?.initialFilters ??
      (saved ? JSON.parse(saved) : { page: 1, pageSize: 10, sortField: 'LastName', sortOrder: 'asc', filters: {} })
    );
  });

  const loadData = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      localStorage.setItem('vehicleOwnerFilters', JSON.stringify(filters));
      const response = await getVehicleOwners(filters);
      setData(response.items);
      setTotal(response.total);
    } catch (err: any) {
      setError(err.message || 'Error loading vehicle owners');
    } finally {
      setLoading(false);
    }
  }, [filters]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const create = async (owner: VehicleOwner) => {
    await createVehicleOwner(owner);
    await loadData();
  };

  const update = async (id: number, owner: VehicleOwner) => {
    await updateVehicleOwner(id, owner);
    await loadData();
  };

  const remove = async (id: number) => {
    await deleteVehicleOwner(id);
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
