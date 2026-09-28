import { createContext } from "react";

interface ContextType {
  onClose: () => void;
  onSubmit?: () => void;
  isLoading?: boolean;
}

export const Context = createContext<ContextType>({
  onClose: () => undefined,
  onSubmit: undefined,
  isLoading: undefined,
});
