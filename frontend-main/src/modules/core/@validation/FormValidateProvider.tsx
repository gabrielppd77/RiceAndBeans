import {
  DefaultValues,
  FormProvider as FormProviderOther,
} from "react-hook-form";
import { z } from "zod";
import { useValidateForm } from "./useValidateForm";

interface FormProviderProps<TSchema extends z.ZodTypeAny> {
  schema: TSchema;
  values?: DefaultValues<z.infer<TSchema>> | null;
  onSubmit: (data: z.TypeOf<TSchema>) => void;
  children: React.ReactNode;
}

export function FormValidateProvider<TSchema extends z.ZodTypeAny>({
  schema,
  values: _values,
  onSubmit,
  children,
}: FormProviderProps<TSchema>) {
  const methods = useValidateForm({
    schema,
    values: _values ? _values : ({} as DefaultValues<z.infer<TSchema>>),
  });

  return (
    <FormProviderOther {...methods}>
      <form
        onSubmit={methods.handleSubmit(onSubmit)}
        noValidate
        className="flex flex-col overflow-y-auto pt-2"
      >
        {children}
      </form>
    </FormProviderOther>
  );
}
