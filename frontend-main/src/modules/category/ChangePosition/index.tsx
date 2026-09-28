import { useEffect, useState } from "react";

import { SortableTable } from "@modules/core/@components/SortableTable";
import { XDialog } from "@modules/core/@components/XDialog";

import DragIndicatorIcon from "@mui/icons-material/DragIndicator";

import { useListAll, useUpdateListAll } from "../@hooks/useListAll";
import { useChangePosition } from "../@hooks/useChangePosition";

import { CategoryResponse } from "../@types/CategoryResponse";

interface ChangePositionProps {
  isOpen: boolean;
  onClose: () => void;
}

export function ChangePosition({ isOpen, onClose }: ChangePositionProps) {
  const [dataToChangePosition, setDataToChangePosition] = useState<
    CategoryResponse[]
  >([]);

  const { data, isPending } = useListAll({});
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
      maxWidth="sm"
      isLoading={isPendingChangePosition}
      onSubmit={() => handleSubmit()}
    >
      <XDialog.Title title="Ajustar posição das categorias" />
      <XDialog.Content>
        <SortableTable.Root
          data={dataToChangePosition}
          isLoading={isPending}
          columns={[
            {
              field: "name",
              headerName: "Nome",
              width: "80%",
            },
            {
              field: "id",
              headerName: "Ação",
              width: "20%",
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
