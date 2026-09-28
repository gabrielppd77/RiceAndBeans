import { useState } from "react";

import { TextField, TextFieldProps } from "../TextField";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { IconButton, InputAdornment } from "@mui/material";

interface TextFieldPasswordProps extends TextFieldProps {}

export function TextFieldPassword({ ...props }: TextFieldPasswordProps) {
  const [showPassword, setShowPassword] = useState(false);
  return (
    <TextField
      type={showPassword ? "text" : "password"}
      placeholder="••••••"
      slotProps={{
        input: {
          endAdornment: (
            <InputAdornment position="end">
              <IconButton
                aria-label="toggle password visibility"
                onClick={() => setShowPassword((prev) => !prev)}
              >
                {showPassword ? <Visibility /> : <VisibilityOff />}
              </IconButton>
            </InputAdornment>
          ),
        },
      }}
      {...props}
    />
  );
}
