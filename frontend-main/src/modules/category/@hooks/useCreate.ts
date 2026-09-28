import { useMutation } from "@tanstack/react-query";
import api from "@libs/api";
import { fireError } from "@libs/alert";

import { CreateCategoryRequest } from "../@types/CreateCategoryRequest";
import { notifyCreate } from "@libs/notification";

export function useCreate() {
  return useMutation({
    mutationFn: async (data: CreateCategoryRequest) => {
      await api.post("/categories/create", data);
    },
    onError: fireError,
    onSuccess: () => notifyCreate(),
  });
}
