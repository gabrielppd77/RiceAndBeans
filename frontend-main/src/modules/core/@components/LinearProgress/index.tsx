import { LinearProgress as LinearProgressMui } from "@mui/material";

interface LinearProgressProps {
  isLoading?: boolean;
}

export function LinearProgress({ isLoading }: LinearProgressProps) {
  return (
    <LinearProgressMui
      className={`invisible w-full ${isLoading && "visible"}`}
    />
  );
}
