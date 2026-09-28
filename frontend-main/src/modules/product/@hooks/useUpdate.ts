import { useMutation } from "@tanstack/react-query";
import api from "@libs/api";
import { fireError } from "@libs/alert";

import { UpdateProductRequest } from "../@types/UpdateProductRequest";
import { notifyUpdate } from "@libs/notification";

export function useUpdate() {
  return useMutation({
    mutationFn: async (data: UpdateProductRequest) => {
      await api.put("/products/update", data);
    },
    onError: fireError,
    onSuccess: () => notifyUpdate(),
  });
}
