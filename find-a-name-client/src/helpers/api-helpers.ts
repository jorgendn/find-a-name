import { QueryClient } from "@tanstack/react-query";
import axios, { InternalAxiosRequestConfig } from "axios";

export const queryClient = new QueryClient();

const apiClient = axios.create({
  baseURL: "http://localhost:5246",
});

apiClient.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    const localStorageToken = window.localStorage.getItem("namesToken");

    const token = localStorageToken ? JSON.parse(localStorageToken) : {};

    if (config.headers) {
      config.headers["Authorization"] = `Bearer ${token.accessToken}`;
    }

    return config;
  }
);

export function logIn(email: string, password: string) {
  return apiClient
    .post("/login", {
      email,
      password,
    })
    .then((r) => r.data);
}

export function logOut() {
  return apiClient.post("/logout", {}).then((r) => r.data);
}

export function getNames(
  limit: number = 5
): Promise<{ id: number; name: string; rejectedBy: number[] }[]> {
  return apiClient.get(`/names?limit=${limit}`).then((r) => r.data);
}

export function rejectNames(names: number[]) {
  return apiClient.post("/rejectNames", names).then((r) => r.data);
}
