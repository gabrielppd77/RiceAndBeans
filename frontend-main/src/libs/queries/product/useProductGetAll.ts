import { useQuery, useQueryClient } from "@tanstack/react-query";

import api from "@libs/api";

import { ProductResponseTableDTO } from "./dtos/ProductResponseTableDTO";
import { fireError } from "@libs/alert";

const query = ["product"];

export function useProductGetAll() {
  async function handleRequest() {
    const response = await api.get<ProductResponseTableDTO[]>("/product");
    return response.data;
  }

  const { error, ...rest } = useQuery({
    queryKey: query,
    queryFn: handleRequest,
  });

  if (error) {
    fireError(error);
  }

  return rest;
}

export function useInvalidate() {
  const queryClient = useQueryClient();
  function handleInvalidate() {
    queryClient.invalidateQueries({
      queryKey: query,
    });
  }
  return { handleInvalidate };
}
