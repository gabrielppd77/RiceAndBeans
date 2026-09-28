import { useListAll } from "@modules/category/@hooks/useListAll";
import { AutoComplete } from "../AutoComplete";

interface AutoCompleteCategoryProps {
  name: string;
}

export function AutoCompleteCategory({ name }: AutoCompleteCategoryProps) {
  const {
    data,
    isLoading: _isLoading,
    isFetching,
    refetch,
  } = useListAll({ enabled: false });
  const isLoading = _isLoading || isFetching;
  return (
    <AutoComplete
      options={data || []}
      isLoading={isLoading}
      label="Categoria"
      name={name}
      idField="id"
      renderOptions={(d) => d.name}
      onRefetch={refetch}
    />
  );
}
