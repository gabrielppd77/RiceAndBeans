import { createContext } from "react";

export interface AuthContextState {
  token: string | null;
  setToken: (newToken: string) => void;
}

const AuthContext = createContext<AuthContextState>({
  token: "",
  setToken: () => undefined,
});

export default AuthContext;
