import QueryClientProvider from "./modules/configuration/react-query/providers/QueryClientProvider";
import RouterProvider from "./modules/configuration/routing/providers/RouterProvider";

export function App() {
  return (
    <QueryClientProvider>
      <RouterProvider />
    </QueryClientProvider>
  );
}
