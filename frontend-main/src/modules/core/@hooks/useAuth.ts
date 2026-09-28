import { useContext } from "react";

import AuthContext, { AuthContextState } from "@contexts/AuthContext";

export const useAuth = () => {
  return useContext<AuthContextState>(AuthContext);
};
