import {
  RouterProvider as RouterProviderMain,
  createBrowserRouter,
} from "react-router-dom";

import { PublicLayout } from "@modules/core/@layouts/PublicLayout";
import { MainLayout } from "@modules/core/@layouts/MainLayout";
import { UserSettingsLayout } from "@modules/user-settings/@layouts/UserSettingsLayout";
import { CompanyLayout } from "@modules/company/@layouts/CompanyLayout";

import { Version } from "@modules/app/Version";
import { Login } from "@modules/authentication/Login";
import { Register } from "@modules/authentication/Register";
import { ConfirmEmail } from "@modules/authentication/ConfirmEmail";
import { ForgotPassword } from "@modules/recover-password/ForgotPassword";
import { RecoverPassword } from "@modules/recover-password/RecoverPassword";
import { Main as MainHome } from "@modules/home/Main";
import { Profile } from "@modules/user-settings/Profile";
import { Main as MainCompany } from "@modules/company/Main";
import { Main as MainCategory } from "@modules/category/Main";
import { Main as MainProduct } from "@modules/product/Main";

import { routes as routesPaths } from "@modules/routing/@consts/routes";

const routes = createBrowserRouter([
  {
    element: <PublicLayout />,
    children: [
      {
        path: routesPaths.version,
        element: <Version />,
      },
      {
        path: routesPaths.initial,
        element: <Login />,
      },
      {
        path: routesPaths.register,
        element: <Register />,
      },
      {
        path: routesPaths.confirmEmail,
        element: <ConfirmEmail />,
      },
      {
        path: routesPaths.forgotPasword,
        element: <ForgotPassword />,
      },
      {
        path: routesPaths.recoverPassword,
        element: <RecoverPassword />,
      },
    ],
  },
  {
    element: <MainLayout />,
    children: [
      {
        path: routesPaths.home,
        element: <MainHome />,
      },
      {
        element: <UserSettingsLayout />,
        children: [
          {
            path: routesPaths.profile,
            element: <Profile />,
          },
        ],
      },
      {
        element: <CompanyLayout />,
        children: [
          {
            path: routesPaths.companyMain,
            element: <MainCompany />,
          },
        ],
      },
      {
        element: <MainCategory />,
        path: routesPaths.category,
      },
      {
        element: <MainProduct />,
        path: routesPaths.product,
      },
    ],
  },
]);

export default function RouterProvider() {
  return <RouterProviderMain router={routes} />;
}
