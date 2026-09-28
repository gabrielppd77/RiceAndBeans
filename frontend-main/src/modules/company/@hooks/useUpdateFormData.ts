import { useMutation } from "@tanstack/react-query";

import api from "@libs/api";

import { fireError } from "@libs/alert";
import { notifyUpdate } from "@libs/notification";

import { FormDataRequest } from "../@types/FormDataRequest";

interface RequestProps {
  data: FormDataRequest;
}

export function useUpdateFormData() {
  async function handleRequest({ data }: RequestProps) {
    await api.put("/companies/update-form-data", data);
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: () => notifyUpdate(),
    onError: fireError,
  });
}
