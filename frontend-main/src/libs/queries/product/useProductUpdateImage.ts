import { useMutation } from "@tanstack/react-query";

import { useInvalidate } from "./useProductGetById";

import api from "@libs/api";

import { notifyUpdate } from "@libs/notification";
import { fireError } from "@libs/alert";

interface RequestProps {
  params: { id: string };
  data: FormData;
}

export function useProductUpdateImage() {
  const { handleInvalidate } = useInvalidate();

  async function handleRequest({ params, data }: RequestProps) {
    await api.put("/product/update-image", data, { params });
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => {
      notifyUpdate();
      handleInvalidate();
    },
    onError: fireError,
  });
}
