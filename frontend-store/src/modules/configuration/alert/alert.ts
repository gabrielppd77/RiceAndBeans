import Swal, { type SweetAlertIcon } from "sweetalert2";

import { AxiosError, HttpStatusCode } from "axios";

interface ProblemDetails {
  type: string;
  title: string;
  status: HttpStatusCode;
  traceId?: string;
  errors?: {
    [key: string]: string[];
  };
  errorCodes?: string[];
  detail?: string;
}

export function extractError(err: unknown) {
  let title = "Oops...";
  let text = "Algo deu errado!";
  let icon: SweetAlertIcon = "error";

  if (err instanceof AxiosError) {
    const responseData: ProblemDetails | undefined = err?.response?.data;
    if (responseData) {
      title = responseData.status + " " + responseData.title;
      text = "";
      if (responseData.errors) {
        const allErrors = Object.values(responseData.errors);
        if (allErrors.length > 0 && allErrors[0].length > 0) {
          text += " " + allErrors[0][0];
        }
      }
      if (responseData.errorCodes) {
        responseData.errorCodes.forEach((err) => (text += " " + err));
      }
      if (responseData.detail) {
        text = responseData.detail;
      }
      text.trim();
      icon =
        responseData.status === HttpStatusCode.BadRequest ||
        responseData.status === HttpStatusCode.Conflict
          ? "warning"
          : "error";
    }
  }

  return { title, text, icon };
}

export function fireError(err: unknown) {
  const { icon, title, text } = extractError(err);

  Swal.fire({
    icon,
    title,
    text,
    confirmButtonText: "Beleza!",
    showCloseButton: true,
  });
}
