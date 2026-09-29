type RuntimeEnv = {
  VITE_BASE_URL?: string;
  VITE_URL_REDIRECT_REGISTER?: string;
};

declare global {
  interface Window {
    __ENV__?: RuntimeEnv;
  }
}

const runtimeEnv = window.__ENV__ ?? {};

export const env = {
  baseUrl: runtimeEnv.VITE_BASE_URL || import.meta.env.VITE_BASE_URL,
  urlRedirectRegister:
    runtimeEnv.VITE_URL_REDIRECT_REGISTER ||
    import.meta.env.VITE_URL_REDIRECT_REGISTER,
};
