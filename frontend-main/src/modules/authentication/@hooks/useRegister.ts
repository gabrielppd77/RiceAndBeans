import { useMutation } from "@tanstack/react-query";

import { fireError } from "@libs/alert";
import api from "@libs/api";

import { RegisterRequest } from "../@types/RegisterRequest";
import { AuthenticationResponse } from "../@types/AuthenticationResponse";

export function useRegister() {
  return useMutation({
    mutationFn: async (data: RegisterRequest) => {
      const response = await api.post<AuthenticationResponse>(
        "/auth/register",
        data,
      );
      return response.data;
    },
    onError: fireError,
  });
}
