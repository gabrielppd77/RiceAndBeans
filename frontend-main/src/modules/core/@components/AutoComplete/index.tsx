import { Autocomplete, CircularProgress, TextField } from "@mui/material";
import { useEffect } from "react";
import { Controller, FieldError } from "react-hook-form";

interface AutoCompleteProps<TData> {
  label: string;
  name: string;
  isValidate?: boolean;
  options: TData[];
  renderOptions: (d: TData) => string;
  onRefetch: () => void;
  isLoading?: boolean;
  idField: keyof TData extends string ? keyof TData : never;
  required?: boolean;
  onChange?: (d?: string) => void;
  value?: string;
  error?: FieldError;
  ref?: React.Ref<unknown>;
}

function AutoCompleteDefault<TData>({
  label,
  name,
  options,
  renderOptions,
  onRefetch,
  isLoading,
  idField,
  required,
  onChange,
  value,
  error,
  ref,
}: AutoCompleteProps<TData>) {
  useEffect(() => {
    if (value && options.length <= 0) {
      onRefetch();
    }
  }, [value, onRefetch, options]);

  return (
    <Autocomplete
      id="auto-complete"
      onChange={(_, obj) =>
        onChange && onChange(obj ? (obj[idField] as string) : undefined)
      }
      ref={ref}
      value={options.find((d) => d[idField] === value) || null}
      getOptionLabel={renderOptions}
      getOptionKey={(d) => d[idField] as string}
      options={options}
      loading={isLoading}
      openText="Abrir"
      clearText="Limpar"
      closeText="Fechar"
      loadingText="Carregando..."
      noOptionsText="Sem opções"
      onOpen={() => options.length <= 0 && onRefetch()}
      renderInput={(params) => (
        <TextField
          {...params}
          name={name}
          helperText={error ? error.message : null}
          error={!!error}
          label={label}
          required={required}
          InputProps={{
            ...params.InputProps,
            endAdornment: (
              <>
                {isLoading ? (
                  <CircularProgress color="inherit" size={20} />
                ) : null}
                {params.InputProps.endAdornment}
              </>
            ),
          }}
        />
      )}
    />
  );
}

function AutoCompleteControlled<TData>({
  name,
  ...rest
}: AutoCompleteProps<TData>) {
  return (
    <Controller
      name={name}
      render={({ field: { ref, onChange, value }, fieldState: { error } }) => (
        <AutoCompleteDefault
          {...rest}
          name={name}
          onChange={onChange}
          value={value}
          error={error}
          ref={ref}
        />
      )}
    />
  );
}

export function AutoComplete<TData>({
  isValidate = true,
  ...rest
}: AutoCompleteProps<TData>) {
  if (isValidate) {
    return <AutoCompleteControlled {...rest} />;
  }
  return <AutoCompleteDefault {...rest} />;
}
