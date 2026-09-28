import { useMutation } from "@tanstack/react-query";

import { useInvalidate } from "./useCompanyGetCompany";

import { CompanyUpdateDTO } from "./dtos/CompanyUpdateDTO";

import api from "@libs/api";

import { notifyUpdate } from "@libs/notification";
import { fireError } from "@libs/alert";

interface RequestProps {
  id: string;
  data: CompanyUpdateDTO;
}

export function useCompanyUpdate() {
  const { handleInvalidate } = useInvalidate();

  async function handleRequest({ id, data }: RequestProps) {
    await api.put("/Company/" + id, data);
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
