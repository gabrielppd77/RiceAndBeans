import api from "@libs/api";
import { FormDataResponse } from "../@types/FormDataResponse";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { fireError } from "@libs/alert";

const queryKey = ["get-form-data"];

export function useGetFormData() {
  async function handleRequest() {
    const response = await api.get<FormDataResponse>(
      "/companies/get-form-data",
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

export function useUpdateGetFormData() {
  const queryClient = useQueryClient();

  function handleChange(newData: Partial<FormDataResponse>) {
    queryClient.setQueryData<FormDataResponse>(queryKey, (oldData) => {
      if (!oldData) return oldData;
      return {
        ...oldData,
        ...newData,
      };
    });
  }

  return { handleChange };
}
