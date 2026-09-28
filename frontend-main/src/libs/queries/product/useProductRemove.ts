import { useMutation } from "@tanstack/react-query";

import api from "@libs/api";

import { notifyRemove } from "@libs/notification";
import { fireError } from "@libs/alert";

import { useInvalidate } from "./useProductGetAll";

interface RequestProps {
  params: {
    id: string;
  };
}

export function useProductRemove() {
  const { handleInvalidate } = useInvalidate();

  async function handleRequest({ params }: RequestProps) {
    await api.delete("/product", { params });
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => {
      notifyRemove();
      handleInvalidate();
    },
    onError: fireError,
  });
}
