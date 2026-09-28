import { useMutation } from "@tanstack/react-query";

import { fireError } from "@libs/alert";
import api from "@libs/api";

import { ResetRequest } from "../@types/ResetRequest";

export function useResetPassword() {
  return useMutation({
    mutationFn: async (request: ResetRequest) => {
      await api.post("/recover-password/reset", request);
    },
    onError: fireError,
  });
}
