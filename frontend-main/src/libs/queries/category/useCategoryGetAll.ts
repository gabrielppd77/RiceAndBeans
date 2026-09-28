import { useQuery } from "@tanstack/react-query";

import api from "@libs/api";

import { CategoryResponseDTO } from "./dtos/CategoryResponseDTO";
import { fireError } from "@libs/alert";

export const query = ["category"];

interface RequestProps {
  enabled?: boolean;
}

export function useCategoryGetAll({ enabled = true }: RequestProps) {
  async function handleRequest() {
    const response = await api.get<CategoryResponseDTO[]>("/category");
    return response.data;
  }

  const { error, ...rest } = useQuery({
    queryKey: query,
    queryFn: handleRequest,
    enabled,
  });

  if (error) {
    fireError(error);
  }

  return rest;
}
