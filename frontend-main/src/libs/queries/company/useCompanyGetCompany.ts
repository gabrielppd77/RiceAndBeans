import { useQuery, useQueryClient } from "@tanstack/react-query";

import api from "@libs/api";

import { CompanyResponseDTO } from "./dtos/CompanyResponseDTO";
import { fireError } from "@libs/alert";

const query = ["Company"];

export function useCompanyGetCompany() {
  async function handleRequest() {
    const response = await api.get<CompanyResponseDTO>("/Company");
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
