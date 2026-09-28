import { useMutation, useQueryClient } from "@tanstack/react-query";

import { CategoryRequestDTO } from "./dtos/CategoryRequestDTO";

import api from "@libs/api";

import { notifyUpdate } from "@libs/notification";
import { fireError } from "@libs/alert";

import { query } from "./useCategoryGetAll";

interface RequestProps {
  id: string;
  data: CategoryRequestDTO;
}

export function useCategoryUpdate() {
  const queryClient = useQueryClient();

  async function handleRequest({ id, data }: RequestProps) {
    await api.put("/Category/" + id, data);
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => {
      notifyUpdate();
      queryClient.invalidateQueries({
        queryKey: query,
      });
    },
    onError: fireError,
  });
}
