import { Button, DialogActions } from "@mui/material";
import { useContext } from "react";
import { Context } from "../Context";

export function Actions() {
  const { onClose, onSubmit, isLoading } = useContext(Context);

  return (
    <DialogActions>
      <Button variant="outlined" onClick={() => onClose()}>
        Cancelar
      </Button>
      <Button
        variant="contained"
        loading={isLoading}
        type="submit"
        onClick={() => onSubmit && onSubmit()}
      >
        Salvar
      </Button>
    </DialogActions>
  );
}
