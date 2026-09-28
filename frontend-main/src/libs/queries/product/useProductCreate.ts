import { useMutation } from "@tanstack/react-query";

import { ProductRequestDTO } from "./dtos/ProductRequestDTO";

import api from "@libs/api";

import { notifyCreate } from "@libs/notification";
import { fireError } from "@libs/alert";

import { useInvalidate } from "./useProductGetAll";

interface RequestProps {
  data: ProductRequestDTO;
}

export function useProductCreate() {
  const { handleInvalidate } = useInvalidate();

  async function handleRequest({ data }: RequestProps) {
    await api.post("/product", data);
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => {
      notifyCreate();
      handleInvalidate();
    },
    onError: fireError,
  });
}
