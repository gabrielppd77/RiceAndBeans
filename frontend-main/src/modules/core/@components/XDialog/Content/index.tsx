import { DialogContent } from "@mui/material";

interface ContentProps {
  children: React.ReactNode;
}

export function Content({ children }: ContentProps) {
  return (
    <DialogContent dividers className="relative">
      {children}
    </DialogContent>
  );
}
