import clsx from "clsx";
import { useGoTo } from "../../../../configuration/routing/hooks/useGoTo";
import { env } from "../../../../configuration/env/env";

function PageTitle() {
  const { goToHome } = useGoTo();

  return (
    <a className="flex items-center gap-1" onClick={() => goToHome()}>
      <img
        src="rice-and-beans-logo.svg"
        className="h-12"
        alt="rice and beans Logo"
      />
      <span className="text-sm font-medium whitespace-nowrap sm:text-base md:text-lg lg:text-xl">
        Rice&Beans
      </span>
    </a>
  );
}

export function Navbar() {
  return (
    <nav
      className={clsx(
        "h-appbar fixed top-0 right-0 left-0 z-10",
        "flex items-center justify-between px-2 shadow-md md:px-4",
      )}
    >
      <PageTitle />

      <button
        onClick={() =>
          window.open(env.urlRedirectRegister, "_blank")
        }
        className={clsx(
          "bg-red-500 text-white hover:bg-red-700",
          "rounded-full px-4 py-2 text-xs font-medium sm:text-base",
          "duration-300 hover:scale-105",
        )}
      >
        Cadastre sua Loja
      </button>
    </nav>
  );
}
