import { useMutation } from "@tanstack/react-query";

import { ProductRequestDTO } from "./dtos/ProductRequestDTO";

import api from "@libs/api";

import { notifyUpdate } from "@libs/notification";
import { fireError } from "@libs/alert";

import { useInvalidate } from "./useProductGetAll";

interface RequestProps {
  params: { id: string };
  data: ProductRequestDTO;
}

export function useProductUpdate() {
  const { handleInvalidate } = useInvalidate();

  async function handleRequest({ params, data }: RequestProps) {
    await api.put("/product", data, { params });
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => {
      notifyUpdate();
      handleInvalidate();
    },
    onError: fireError,
  });
}
