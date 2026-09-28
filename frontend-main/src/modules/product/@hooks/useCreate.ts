import { useMutation } from "@tanstack/react-query";
import api from "@libs/api";
import { fireError } from "@libs/alert";

import { CreateProductRequest } from "../@types/CreateProductRequest";
import { notifyCreate } from "@libs/notification";

export function useCreate() {
  return useMutation({
    mutationFn: async (data: CreateProductRequest) => {
      await api.post("/products/create", data);
    },
    onError: fireError,
    onSuccess: () => notifyCreate(),
  });
}
