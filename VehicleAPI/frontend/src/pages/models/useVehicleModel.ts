import { useCallback, useEffect, useState } from "react";
import {
  getVehicleModels,
  createVehicleModel,
  updateVehicleModel,
  deleteVehicleModel,
} from "./vehicleModelApi";
import type { VehicleModel, ListParams } from "../../types";

interface UseVehicleModelOptions {
  initialFilters?: ListParams;
}

export function useVehicleModel(options?: UseVehicleModelOptions) {
  const [data, setData] = useState<VehicleModel[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [filters, setFilters] = useState<ListParams>(() => {
    const saved = localStorage.getItem('vehicleModelFilters');
    return (
      options?.initialFilters ??
      (saved ? JSON.parse(saved) : { page: 1, pageSize: 10, sortField: 'name', sortOrder: 'asc', filters: {} })
    );
  });

  const loadData = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      localStorage.setItem('vehicleModelFilters', JSON.stringify(filters));
      const response = await getVehicleModels(filters);
      setData(response.items);
      setTotal(response.total);
    } catch (err: any) {
      setError(err.message || 'Error loading vehicle models');
    } finally {
      setLoading(false);
    }
  }, [filters]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const create = async (model: VehicleModel) => {
    await createVehicleModel(model);
    await loadData();
  };

  const update = async (id: number, model: VehicleModel) => {
    await updateVehicleModel(id, model);
    await loadData();
  };

  const remove = async (id: number) => {
    await deleteVehicleModel(id);
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
