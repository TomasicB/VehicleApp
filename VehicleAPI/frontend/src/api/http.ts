import { api } from "./axios";

export async function get<T>(url: string, params?: unknown, signal?: AbortSignal) {
  const res = await api.get<T>(url, { params, signal });
  return res.data;
}
export async function post<TReq, TRes>(url: string, body: TReq, signal?: AbortSignal) {
  const res = await api.post<TRes>(url, body, { signal });
  return res.data;
}
export async function put<TReq, TRes>(url: string, body: TReq, signal?: AbortSignal) {
  const res = await api.put<TRes>(url, body, { signal });
  return res.data;
}
export async function del<T>(url: string, signal?: AbortSignal) {
  const res = await api.delete<T>(url, { signal });
  return res.data;
}