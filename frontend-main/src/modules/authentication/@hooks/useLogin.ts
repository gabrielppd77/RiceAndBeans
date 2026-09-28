import { useMutation } from "@tanstack/react-query";

import { fireError } from "@libs/alert";
import api from "@libs/api";

import { AuthenticationResponse } from "../@types/AuthenticationResponse";
import { LoginRequest } from "../@types/LoginRequest";

export function useLogin() {
  return useMutation({
    mutationFn: async (data: LoginRequest) => {
      const response = await api.post<AuthenticationResponse>(
        "/auth/login",
        data,
      );
      return response.data;
    },
    onError: fireError,
  });
}
