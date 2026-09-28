import api from "@libs/api";
import { GeneralDataResponse } from "../@types/GeneralDataResponse";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { fireError } from "@libs/alert";

const queryKey = ["get-general-data"];

export function useGetGeneralData() {
  async function handleRequest() {
    const response = await api.get<GeneralDataResponse>(
      "/users/get-general-data",
    );
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

export function useUpdateGetGeneralData() {
  const queryClient = useQueryClient();

  function handleChange(newData: Partial<GeneralDataResponse>) {
    queryClient.setQueryData<GeneralDataResponse>(queryKey, (oldData) => {
      if (!oldData) return oldData;
      return {
        ...oldData,
        ...newData,
      };
    });
  }

  return { handleChange };
}
