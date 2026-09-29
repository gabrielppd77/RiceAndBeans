type RuntimeEnv = {
  VITE_BASE_URL?: string;
};

declare global {
  interface Window {
    __ENV__?: RuntimeEnv;
  }
}

const runtimeEnv = window.__ENV__ ?? {};

export const env = {
  baseUrl: runtimeEnv.VITE_BASE_URL || import.meta.env.VITE_BASE_URL,
};
