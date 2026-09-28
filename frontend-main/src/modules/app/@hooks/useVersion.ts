import { useQuery } from "@tanstack/react-query";

import api from "@libs/api";

import { VersionResponse } from "../@types/VersionResponse";
import { fireError } from "@libs/alert";

export function useVersion() {
  async function handleRequest() {
    const response = await api.get<VersionResponse>("/version");
    return response.data;
  }

  const result = useQuery({
    queryKey: ["version"],
    queryFn: handleRequest,
  });

  if (result.error) {
    fireError(result.error);
  }

  return result;
}
