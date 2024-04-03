import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { getNames, logIn, logOut } from '../helpers/api-helpers';

type LoginInformation = {
    email: string;
    password: string;
};

export function useLogin() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (info: LoginInformation) => logIn(info.email, info.password),
        onSuccess: (data) => {
            window.localStorage.setItem('namesToken', JSON.stringify(data));
            if (data) queryClient.invalidateQueries({ queryKey: ['names'] });
        },
    });
}

export function useLogout() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: logOut,
        onSuccess: () => {
            window.localStorage.removeItem('namesToken');
            queryClient.invalidateQueries({ queryKey: ['names'] });
        },
    });
}

export function useGetNames() {
    return useQuery({
        queryKey: ['names'],
        queryFn: () => getNames(),
        retry: 0,
        refetchOnWindowFocus: false,
    });
}