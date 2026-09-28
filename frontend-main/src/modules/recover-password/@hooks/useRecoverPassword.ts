import { useMutation } from "@tanstack/react-query";

import { fireError } from "@libs/alert";
import api from "@libs/api";

export function useRecoverPassword() {
  return useMutation({
    mutationFn: async (email: string) => {
      await api.post("/recover-password/recover", null, {
        params: { email },
      });
    },
    onError: fireError,
  });
}
