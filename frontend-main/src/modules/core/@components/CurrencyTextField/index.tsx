import {
  TextField as MUITextField,
  TextFieldProps as MUITextFieldProps,
} from "@mui/material";
import { Controller } from "react-hook-form";

import { NumericFormat, NumericFormatProps } from "react-number-format";

interface CurrencyTextFieldProps
  extends Omit<NumericFormatProps<MUITextFieldProps>, "label" | "name"> {
  label: string;
  name: string;
  isValidate?: boolean;
}

function CurrencyTextFieldDefault(
  props: NumericFormatProps<MUITextFieldProps>,
) {
  return (
    <NumericFormat
      customInput={MUITextField}
      allowNegative={false}
      allowLeadingZeros
      thousandSeparator="."
      decimalSeparator=","
      {...props}
    />
  );
}

function CurrencyTextFieldControlled({
  name,
  ...rest
}: CurrencyTextFieldProps) {
  return (
    <Controller
      name={name}
      render={({ field: { ref, onChange, value }, fieldState: { error } }) => (
        <CurrencyTextFieldDefault
          helperText={error ? error.message : null}
          error={!!error}
          onValueChange={({ floatValue }) => onChange(floatValue)}
          value={value}
          name={name}
          inputRef={ref}
          {...rest}
        />
      )}
    />
  );
}

export function CurrencyTextField({
  isValidate = true,
  ...rest
}: CurrencyTextFieldProps) {
  if (isValidate) {
    return <CurrencyTextFieldControlled {...rest} />;
  }
  return <CurrencyTextFieldDefault {...rest} />;
}
