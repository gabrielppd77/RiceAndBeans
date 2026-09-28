import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, DefaultValues } from "react-hook-form";
import { z } from "zod";

interface UseValidateFormProps<TSchema extends z.ZodTypeAny> {
  schema: TSchema;
  values: DefaultValues<z.infer<TSchema>>;
}

export function useValidateForm<TSchema extends z.ZodTypeAny>({
  schema,
  values,
}: UseValidateFormProps<TSchema>) {
  type FormValues = z.infer<typeof schema>;

  return useForm<FormValues>({
    resolver: zodResolver(schema),
    values,
  });
}
