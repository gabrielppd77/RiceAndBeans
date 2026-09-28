import { useQuery } from "@tanstack/react-query";
import api from "../../../configuration/api/api";
import { fireError } from "../../../configuration/alert/alert";

import type { GetStoreDataResponse } from "../types/GetStoreDataResponse";
import { useGoTo } from "../../../configuration/routing/hooks/useGoTo";

const queryKey = ["/stores/get-store-data"];

interface RequestProps {
  params: {
    companyPath?: string;
  };
}

export function useGetStoreData({ params }: RequestProps) {
  const { goToHome } = useGoTo();

  async function handleRequest() {
    if (!params.companyPath) return;
    const response = await api.get<GetStoreDataResponse>(queryKey[0], {
      params,
    });
    return response.data;
  }

  const result = useQuery({
    queryKey,
    queryFn: handleRequest,
    staleTime: Infinity,
  });

  if (result.error) {
    fireError(result.error);
    goToHome();
  }

  return result;
}
