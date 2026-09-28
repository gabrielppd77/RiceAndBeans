import { Button, Grid, Stack } from "@mui/material";
import { TextField } from "@modules/core/@components/TextField";
import { SimpleLoadingPage } from "@modules/core/@components/SimpleLoadingPage";
import { LinearProgress } from "@modules/core/@components/LinearProgress";

import { useUpdateFormData } from "../@hooks/useUpdateFormData";
import { useGetFormData } from "../@hooks/useGetFormData";
import { FormValidateProvider, z } from "@modules/core/@validation";
import { MainPhoto } from "./MainPhoto";

const schema = z.object({
  name: z.string({ message: "Informe o Nome da loja" }).min(1),
  description: z.string({ message: "Informe a Descrição da loja" }).nullish(),
  path: z.string({ message: "Informe o Caminho para acesso da loja" }).min(1),
});

type DataType = z.infer<typeof schema>;

export function Main() {
  const { data, isLoading, isFetching } = useGetFormData();
  const { mutateAsync, isPending } = useUpdateFormData();

  async function onSubmit(d: DataType) {
    await mutateAsync({
      data: d,
    });
  }

  if (isLoading) {
    return <SimpleLoadingPage />;
  }

  return (
    <div className="p-2">
      <LinearProgress isLoading={isFetching} />

      <Grid container spacing={2} className="p-2">
        <Grid size={{ xs: 12, sm: 7, md: 8, lg: 10 }}>
          <FormValidateProvider
            schema={schema}
            values={data}
            onSubmit={onSubmit}
          >
            <Stack spacing={1} className="flex-1">
              <TextField
                label="Nome da loja"
                name="name"
                className="max-w-2xl"
                autoFocus
              />
              <TextField
                label="Caminho para acesso da loja"
                name="path"
                className="max-w-2xl"
              />
              <TextField
                label="Descrição da loja"
                name="description"
                className="max-w-2xl"
                multiline
                rows={4}
                slotProps={{
                  htmlInput: {
                    maxLength: 500,
                  },
                }}
              />
            </Stack>

            <div className="mt-2">
              <Button type="submit" loading={isPending}>
                Salvar Alterações
              </Button>
            </div>
          </FormValidateProvider>
        </Grid>

        <Grid size={{ xs: 12, sm: 5, md: 4, lg: 2 }}>
          <MainPhoto urlImage={data?.urlImage} />
        </Grid>
      </Grid>
    </div>
  );
}
