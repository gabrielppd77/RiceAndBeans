import { useQuery, useQueryClient } from "@tanstack/react-query";

import api from "@libs/api";

import { ProductResponseFormDTO } from "./dtos/ProductResponseFormDTO";
import { fireError } from "@libs/alert";

const query = "product-by-id";

interface RequestProps {
  id: string | null;
}

export function useProductGetById({ id }: RequestProps) {
  async function handleRequest() {
    if (!id) return {} as ProductResponseFormDTO;
    const response = await api.get<ProductResponseFormDTO>("/product/" + id);
    return response.data;
  }

  const { error, ...rest } = useQuery({
    queryKey: [query, id],
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
      queryKey: [query],
    });
  }
  return { handleInvalidate };
}
