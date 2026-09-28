import { useEffect, useState } from "react";

import { SortableTable } from "@modules/core/@components/SortableTable";
import { XDialog } from "@modules/core/@components/XDialog";

import DragIndicatorIcon from "@mui/icons-material/DragIndicator";

import { useListAll, useUpdateListAll } from "../@hooks/useListAll";
import { useChangePosition } from "../@hooks/useChangePosition";

import { ProductResponse } from "../@types/ProductResponse";
import { formatToCurrency } from "@libs/currency";

interface ChangePositionProps {
  isOpen: boolean;
  onClose: () => void;
}

export function ChangePosition({ isOpen, onClose }: ChangePositionProps) {
  const [dataToChangePosition, setDataToChangePosition] = useState<
    ProductResponse[]
  >([]);

  const { data, isPending } = useListAll();
  const { handleChange } = useUpdateListAll();
  const {
    mutateAsync: mutateAsyncChangePosition,
    isPending: isPendingChangePosition,
  } = useChangePosition();

  useEffect(() => {
    setDataToChangePosition(data || []);
  }, [data]);

  async function handleSubmit() {
    const categoriesWithNewPosition = dataToChangePosition.map(
      (categoria, index) => ({
        id: categoria.id,
        newPosition: index + 1,
      }),
    );
    await mutateAsyncChangePosition(categoriesWithNewPosition);
    handleChange();
    onClose();
  }

  return (
    <XDialog.Root
      isOpen={isOpen}
      onClose={onClose}
      maxWidth="md"
      isLoading={isPendingChangePosition}
      onSubmit={() => handleSubmit()}
    >
      <XDialog.Title title="Ajustar posição dos produtos" />
      <XDialog.Content>
        <SortableTable.Root
          data={dataToChangePosition}
          isLoading={isPending}
          columns={[
            {
              field: "name",
              headerName: "Nome",
              width: "20%",
            },
            {
              field: "description",
              headerName: "Descrição",
              width: "35%",
            },
            {
              field: "price",
              headerName: "Preço",
              renderRow: (d) => formatToCurrency(d.price),
            },
            {
              field: "categoryName",
              headerName: "Categoria",
            },
            {
              field: "id",
              headerName: "Ação",
              renderRow: () => (
                <SortableTable.DragHandle className="h-8 w-8 cursor-grab rounded-full hover:bg-gray-100">
                  <DragIndicatorIcon color="primary" />
                </SortableTable.DragHandle>
              ),
            },
          ]}
          onChange={setDataToChangePosition}
        />
      </XDialog.Content>
      <XDialog.Actions />
    </XDialog.Root>
  );
}
