import { useContext } from "react";

import { Context } from "../Context";

import { Box, DialogTitle, IconButton, Tooltip } from "@mui/material";
import { Close } from "@mui/icons-material";

interface TitleProps {
  title: string;
}

export function Title({ title }: TitleProps) {
  const { onClose } = useContext(Context);

  return (
    <Box>
      <DialogTitle>{title}</DialogTitle>
      <Tooltip title="Fechar">
        <IconButton
          aria-label="close"
          onClick={() => onClose()}
          sx={{
            position: "absolute",
            right: 12,
            top: 12,
          }}
        >
          <Close />
        </IconButton>
      </Tooltip>
    </Box>
  );
}
