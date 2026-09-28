import { useMutation, useQueryClient } from "@tanstack/react-query";

import { CategoryRequestDTO } from "./dtos/CategoryRequestDTO";

import api from "@libs/api";

import { notifyCreate } from "@libs/notification";
import { fireError } from "@libs/alert";

import { query } from "./useCategoryGetAll";

interface RequestProps {
  data: CategoryRequestDTO;
}

export function useCategoryCreate() {
  const queryClient = useQueryClient();

  async function handleRequest({ data }: RequestProps) {
    await api.post("/Category", data);
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => {
      notifyCreate();
      queryClient.invalidateQueries({
        queryKey: query,
      });
    },
    onError: fireError,
  });
}
