import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getNames, logIn, logOut, rejectNames } from "../helpers/api-helpers";

type LoginInformation = {
  email: string;
  password: string;
};

export function useLogin() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (info: LoginInformation) => logIn(info.email, info.password),
    onSuccess: (data) => {
      window.localStorage.setItem("namesToken", JSON.stringify(data));
      if (data) queryClient.invalidateQueries({ queryKey: ["names"] });
    },
  });
}

export function useLogout() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: logOut,
    onSuccess: () => {
      window.localStorage.removeItem("namesToken");
      queryClient.invalidateQueries({ queryKey: ["names"] });
    },
  });
}

export function useGetNames(limit: number = 5) {
  return useQuery({
    queryKey: ["names", limit],
    queryFn: () => getNames(limit),
    retry: 0,
    refetchOnWindowFocus: false,
  });
}

export function useRejectNames() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (names: number[]) => rejectNames(names),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["names"] }),
  });
}
