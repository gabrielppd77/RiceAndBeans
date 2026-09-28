import { useMutation } from "@tanstack/react-query";

import { fireError } from "@libs/alert";
import api from "@libs/api";

export function useConfirmEmail() {
  return useMutation({
    mutationFn: async (token: string) => {
      await api.post("/auth/confirm-email", null, {
        params: { token },
      });
    },
    onError: fireError,
  });
}
