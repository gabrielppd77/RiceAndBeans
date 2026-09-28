import {
  RouterProvider as RouterProviderMain,
  createBrowserRouter,
} from "react-router";

import { Home } from "../../../home/main";
import { Store } from "../../../store/main";
import { Feed } from "../../../store/feed";
import { SearchProduct } from "../../../store/search-product";
import { SearchCategory } from "../../../store/search-category";

const routes = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/:companyPath/:productName?",
    element: <Store />,
    children: [
      {
        path: "/:companyPath/:productName?",
        element: <Feed />,
      },
      {
        path: "/:companyPath/:productName?/pesquisar-produto",
        element: <SearchProduct />,
      },
      {
        path: "/:companyPath/:productName?/pesquisar-categoria",
        element: <SearchCategory />,
      },
    ],
  },
]);

export default function RouterProvider() {
  return <RouterProviderMain router={routes} />;
}
