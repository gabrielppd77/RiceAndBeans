import { Button, Container, IconButton, Stack } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";

import { PageHeader } from "@modules/core/@components/PageHeader";
import { DataTable } from "@modules/core/@components/DataTable";

import { useDialog } from "@modules/core/@hooks/useDialog";
import { confirmDelete } from "@libs/alert";

import { Form } from "./Form";
import { ChangePosition } from "../ChangePosition";

import { useListAll, useUpdateListAll } from "../@hooks/useListAll";
import { useRemove } from "../@hooks/useRemove";
import { formatToCurrency } from "@libs/currency";

export function Main() {
  const {
    toggle: toggleForm,
    isOpen: isOpenForm,
    data: dataForm,
  } = useDialog<string | null>(null);

  const { toggle: toggleChangePosition, isOpen: isOpenChangePosition } =
    useDialog(null);
  const { mutateAsync: mutateAsyncRemove } = useRemove();

  const { data, isLoading, isFetching } = useListAll();
  const { handleChange } = useUpdateListAll();

  async function handleRemove(productId: string) {
    await mutateAsyncRemove({ productId });
    handleChange();
  }

  return (
    <Container maxWidth="xl" className="p-2">
      <Stack gap={1}>
        <PageHeader
          title="Produtos"
          renderRight={
            <div className="flex flex-col gap-2 sm:flex-row">
              <Button onClick={() => toggleChangePosition(null)}>
                Mudar posição
              </Button>
              <Button onClick={() => toggleForm(null)}>Adicionar</Button>
            </div>
          }
        />

        <DataTable
          onKeyDown={(key, rows) => {
            const row = rows[0];
            if (key === "+") {
              toggleForm(null);
            }
            if (key === "F2" && row) {
              toggleForm(row.id);
            }
            if (key === "Delete" && row) {
              confirmDelete(async () => await handleRemove(row.id));
            }
          }}
          data={data || []}
          isLoading={isLoading}
          isFetching={isFetching}
          columns={[
            {
              field: "name",
              headerName: "Nome",
              flex: 1,
            },
            {
              field: "description",
              headerName: "Descrição",
              flex: 2,
            },
            {
              field: "price",
              headerName: "Preço",
              renderCell: ({ value }) => formatToCurrency(value),
            },
            {
              field: "categoryName",
              headerName: "Categoria",
            },
            {
              field: "id",
              headerName: "Ações",
              renderCell: ({ value, row }) => (
                <Stack direction="row" height="100%" gap={0.5}>
                  <IconButton onClick={() => toggleForm(row.id)}>
                    <Edit />
                  </IconButton>
                  <IconButton
                    onClick={() =>
                      confirmDelete(async () => await handleRemove(value))
                    }
                  >
                    <Delete />
                  </IconButton>
                </Stack>
              ),
            },
          ]}
        />

        <Form
          isOpen={isOpenForm}
          data={dataForm}
          onClose={() => toggleForm(null)}
        />

        <ChangePosition
          isOpen={isOpenChangePosition}
          onClose={() => toggleChangePosition(null)}
        />
      </Stack>
    </Container>
  );
}
