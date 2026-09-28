import { z } from "zod";

z.setErrorMap((issue, ctx) => {
  let message = ctx.defaultError;

  switch (issue.code) {
    case z.ZodIssueCode.too_small:
      if (issue.type === "string") {
        message = `O texto deve ter pelo menos ${issue.minimum} caractere(s).`;
      }
      if (issue.type === "array") {
        message = `A lista deve conter pelo menos ${issue.minimum} item(ns).`;
      }
      if (issue.type === "number") {
        message = `O número deve ser maior ou igual a ${issue.minimum}`;
      }
      break;
    case z.ZodIssueCode.too_big:
      if (issue.type === "string") {
        message = `O texto deve ter no máximo ${issue.maximum} caractere(s).`;
      }
      if (issue.type === "array") {
        message = `A lista deve conter no máximo ${issue.maximum} item(ns).`;
      }
      if (issue.type === "number") {
        message = `O número deve ser menor ou igual a ${issue.maximum}`;
      }
      break;
  }

  return { message };
});
