import { useMutation } from "@tanstack/react-query";

import api from "@libs/api";

import { notifyUpdate } from "@libs/notification";
import { fireError } from "@libs/alert";

interface RequestProps {
  data: FormData;
}

export function useUploadImage() {
  async function handleRequest({ data }: RequestProps) {
    const response = await api.patch<string>("/users/upload-image", data);
    return response.data;
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => notifyUpdate(),
    onError: fireError,
  });
}
