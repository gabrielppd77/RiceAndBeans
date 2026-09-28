import { useMutation } from "@tanstack/react-query";

import { fireError } from "@libs/alert";
import api from "@libs/api";

interface RequestProps {
  params: {
    password: string;
  };
}

export function useRemoveAccount() {
  return useMutation({
    mutationFn: async ({ params }: RequestProps) => {
      await api.delete("/users/remove-account", { params });
    },
    onError: fireError,
  });
}
