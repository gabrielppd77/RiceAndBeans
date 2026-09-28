import api from "@libs/api";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { fireError } from "@libs/alert";
import { ProductResponse } from "../@types/ProductResponse";

const queryKey = ["/products/list-all"];

export function useListAll() {
  async function handleRequest() {
    const response = await api.get<ProductResponse[]>(queryKey[0]);
    return response.data;
  }

  const result = useQuery({
    queryKey,
    queryFn: handleRequest,
  });

  if (result.error) {
    fireError(result.error);
  }

  return result;
}

export function useUpdateListAll() {
  const queryClient = useQueryClient();

  function handleChange() {
    queryClient.invalidateQueries({
      queryKey,
    });
  }

  return { handleChange };
}
