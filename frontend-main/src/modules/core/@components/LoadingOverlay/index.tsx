import { CircularProgress } from "@mui/material";

interface LoadingContainerProps {
  isLoading?: boolean;
}

export function LoadingOverlay({ isLoading }: LoadingContainerProps) {
  if (!isLoading) return null;

  return (
    <div className="absolute inset-0 z-10 flex items-center justify-center gap-2 bg-gray-100/40">
      <h1 className="text-xl font-medium">Carregando...</h1>{" "}
      <CircularProgress />
    </div>
  );
}
