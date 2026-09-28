import { useMutation } from "@tanstack/react-query";
import api from "@libs/api";
import { fireError } from "@libs/alert";

import { RemoveProductRequest } from "../@types/RemoveProductRequest";
import { notifyRemove } from "@libs/notification";

export function useRemove() {
  return useMutation({
    mutationFn: async (data: RemoveProductRequest) => {
      await api.delete("/products/remove", { data });
    },
    onError: fireError,
    onSuccess: () => notifyRemove(),
  });
}
