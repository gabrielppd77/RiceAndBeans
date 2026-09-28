import { Button, Grid, Stack } from "@mui/material";
import { LinearProgress } from "@modules/core/@components/LinearProgress";
import { SimpleLoadingPage } from "@modules/core/@components/SimpleLoadingPage";

import { RemoveAccount } from "./RemoveAccount";
import { MainPhoto } from "./MainPhoto";

import {
  useGetGeneralData,
  useUpdateGetGeneralData,
} from "../@hooks/useGetGeneralData";
import { FormValidateProvider, z } from "@modules/core/@validation";
import { TextField } from "@modules/core/@components/TextField";
import { useUpdateFormData } from "../@hooks/useUpdateFormData";

const schema = z.object({
  name: z.string({ message: "Informe o Nome" }).min(1),
});

type DataType = z.infer<typeof schema>;

export function Profile() {
  const { data, isLoading, isFetching } = useGetGeneralData();
  const { mutateAsync, isPending } = useUpdateFormData();
  const { handleChange } = useUpdateGetGeneralData();

  async function onSubmit(d: DataType) {
    await mutateAsync({
      data: d,
    });
    handleChange(d);
  }

  if (isLoading) {
    return <SimpleLoadingPage />;
  }

  return (
    <Stack>
      <LinearProgress isLoading={isFetching} />
      <Grid container spacing={2}>
        <Grid size={{ xs: 12, sm: 7, md: 8, lg: 10 }}>
          <FormValidateProvider
            schema={schema}
            values={data}
            onSubmit={onSubmit}
          >
            <Stack spacing={1} className="flex-1">
              <TextField
                label="Nome"
                name="name"
                className="max-w-2xl"
                autoFocus
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
      <RemoveAccount />
    </Stack>
  );
}
