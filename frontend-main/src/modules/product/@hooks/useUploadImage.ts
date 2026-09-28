import { useMutation } from "@tanstack/react-query";

import api from "@libs/api";

import { notifyUpdate } from "@libs/notification";
import { fireError } from "@libs/alert";

interface RequestProps {
  data: FormData;
  params: {
    productId: string;
  };
}

export function useUploadImage() {
  async function handleRequest({ data, params }: RequestProps) {
    const response = await api.patch<string>("/products/upload-image", data, {
      params,
    });
    return response.data;
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => notifyUpdate(),
    onError: fireError,
  });
}
