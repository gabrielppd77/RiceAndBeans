import { useQuery } from "@tanstack/react-query";
import api from "../../../configuration/api/api";
import { fireError } from "../../../configuration/alert/alert";

import type { StoreData } from "../types/StoreData";

const queryKey = ["/stores/get-stores"];

export function useGetStores() {
  async function handleRequest() {
    const response = await api.get<StoreData[]>(queryKey[0]);
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
