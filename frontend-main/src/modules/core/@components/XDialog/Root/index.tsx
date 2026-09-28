import { Breakpoint, Dialog, Theme, useMediaQuery } from "@mui/material";
import { Context } from "../Context";

interface RootProps {
  isOpen: boolean;
  maxWidth?: Breakpoint;
  onClose: () => void;
  onSubmit?: () => void;
  children: React.ReactNode;
  isLoading?: boolean;
}

export function Root({
  isOpen,
  maxWidth = "xs",
  onClose,
  onSubmit,
  children,
  isLoading,
}: RootProps) {
  const fullScreen = useMediaQuery((theme: Theme) =>
    theme.breakpoints.down("sm"),
  );
  return (
    <Dialog
      fullScreen={fullScreen}
      open={isOpen}
      keepMounted={false}
      onClose={() => onClose()}
      aria-describedby="action-dialog"
      fullWidth
      maxWidth={maxWidth}
    >
      <Context.Provider value={{ onClose, onSubmit, isLoading }}>
        {children}
      </Context.Provider>
    </Dialog>
  );
}
