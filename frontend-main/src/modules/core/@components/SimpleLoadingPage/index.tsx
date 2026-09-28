import { CircularProgress } from "@mui/material";

export function SimpleLoadingPage() {
  return (
    <div className="flex flex-col items-center justify-center gap-4 p-20">
      <h1 className="text-2xl font-medium">Carregando ...</h1>
      <CircularProgress />
    </div>
  );
}
