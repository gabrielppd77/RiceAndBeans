import { XDialog } from "@modules/core/@components/XDialog";
import { TextField } from "@modules/core/@components/TextField";

import { FormValidateProvider, z } from "@modules/core/@validation";

import { useCreate } from "../../@hooks/useCreate";
import { useUpdate } from "../../@hooks/useUpdate";
import { useUpdateListAll } from "../../@hooks/useListAll";

const schema = z.object({
  id: z.string().nullish(),
  name: z.string({ message: "Informe o nome" }).min(1),
});

export type FormDataType = z.infer<typeof schema>;

interface FormProps {
  isOpen: boolean;
  data: FormDataType | null;
  onClose: () => void;
}

export function Form({ isOpen, data, onClose }: FormProps) {
  const { mutateAsync: mutateAsyncCreate, isPending: isPendingCreate } =
    useCreate();
  const { mutateAsync: mutateAsyncUpdate, isPending: isPendingUpdate } =
    useUpdate();
  const { handleChange } = useUpdateListAll();

  const isPending = isPendingCreate || isPendingUpdate;

  async function handleSubmit(d: FormDataType) {
    if (d.id) {
      await mutateAsyncUpdate({ categoryId: d.id, name: d.name });
    } else {
      await mutateAsyncCreate(d);
    }
    handleChange();
    onClose();
  }

  return (
    <XDialog.Root
      isOpen={isOpen}
      onClose={() => onClose()}
      isLoading={isPending}
    >
      <FormValidateProvider
        schema={schema}
        values={data}
        onSubmit={handleSubmit}
      >
        <XDialog.Title title="Cadastro de categoria" />
        <XDialog.Content>
          <TextField required label="Nome" name="name" autoFocus />
        </XDialog.Content>
        <XDialog.Actions />
      </FormValidateProvider>
    </XDialog.Root>
  );
}
