import { fireError } from "@libs/alert";
import api from "@libs/api";
import { useMutation } from "@tanstack/react-query";
import { notifyUpdate } from "@libs/notification";
import { ChangePositionRequest } from "@modules/core/@common/@types/ChangePositionRequest";

export function useChangePosition() {
  return useMutation({
    mutationFn: async (data: ChangePositionRequest[]) => {
      await api.patch("/products/change-position", data);
    },
    onError: fireError,
    onSuccess: () => notifyUpdate(),
  });
}
