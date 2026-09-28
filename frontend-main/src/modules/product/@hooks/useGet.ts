import api from "@libs/api";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { fireError } from "@libs/alert";
import { GetProductResponse } from "../@types/GetProductResponse";

const queryKey = ["/products/get"];

interface RequestProps {
  params: {
    productId: string | null;
  };
}

export function useGet({ params }: RequestProps) {
  async function handleRequest() {
    if (!params.productId) return null;
    const response = await api.get<GetProductResponse>(queryKey[0], { params });
    return response.data;
  }

  const result = useQuery({
    queryKey: [...queryKey, params.productId],
    queryFn: handleRequest,
  });

  if (result.error) {
    fireError(result.error);
  }

  return result;
}

export function useUpdateGet() {
  const queryClient = useQueryClient();

  function handleChange(
    productId: string,
    newData: Partial<GetProductResponse>,
  ) {
    queryClient.setQueryData<GetProductResponse>(
      [...queryKey, productId],
      (oldData) => {
        if (!oldData) return oldData;
        return {
          ...oldData,
          ...newData,
        };
      },
    );
  }

  return { handleChange };
}
