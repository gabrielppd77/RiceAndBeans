import { useMutation, useQueryClient } from "@tanstack/react-query";

import api from "@libs/api";

import { notifyRemove } from "@libs/notification";
import { fireError } from "@libs/alert";

import { query } from "./useCategoryGetAll";

interface RequestProps {
  id: string;
}

export function useCategoryRemove() {
  const queryClient = useQueryClient();

  async function handleRequest({ id }: RequestProps) {
    await api.delete("/Category/" + id);
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => {
      notifyRemove();
      queryClient.invalidateQueries({
        queryKey: query,
      });
    },
    onError: fireError,
  });
}
