import { useMutation } from "@tanstack/react-query";

import api from "@libs/api";

import { notifySuccess } from "@libs/notification";
import { fireError } from "@libs/alert";

import { openDownloadData } from "@store/utils";

export function useCompanyGetQRCode() {
  async function handleRequest() {
    const response = await api.get("/company/get-qr-code", {
      responseType: "blob",
    });
    return response.data;
  }

  return useMutation({
    mutationFn: handleRequest,
    onSuccess: (d) => {
      openDownloadData("qrcode.png", d);
      notifySuccess("QR-Code gerado com sucesso");
    },
    onError: fireError,
  });
}
