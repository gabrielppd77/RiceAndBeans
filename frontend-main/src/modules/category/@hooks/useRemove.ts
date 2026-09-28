import { useMutation } from "@tanstack/react-query";
import api from "@libs/api";
import { fireError } from "@libs/alert";

import { RemoveCategoryRequest } from "../@types/RemoveCategoryRequest";
import { notifyRemove } from "@libs/notification";

export function useRemove() {
  return useMutation({
    mutationFn: async (data: RemoveCategoryRequest) => {
      await api.delete("/categories/remove", { data });
    },
    onError: fireError,
    onSuccess: () => notifyRemove(),
  });
}
