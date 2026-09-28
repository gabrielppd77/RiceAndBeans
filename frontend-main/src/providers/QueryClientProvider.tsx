import {
  QueryClient,
  QueryClientProvider as QueryClientProviderMain,
} from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

const queryClient = new QueryClient();

interface QueryClientProviderProps {
  children: React.ReactNode;
}

export default function QueryClientProvider({
  children,
}: QueryClientProviderProps) {
  return (
    <QueryClientProviderMain client={queryClient}>
      {children}
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProviderMain>
  );
}
