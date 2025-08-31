import axios from "axios";

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL ?? "https://localhost:44311/api",
  timeout: 15000,
  withCredentials: false,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("access_token");
  if (token) {
    config.headers = config.headers ?? {};
    (config.headers as Record<string, string>).Authorization = `Bearer ${token}`;
  }
  config.headers = { Accept: "application/json", ...config.headers } as any;
  return config;
});

api.interceptors.response.use(
  (res) => res,
  (err) => Promise.reject(err)
);