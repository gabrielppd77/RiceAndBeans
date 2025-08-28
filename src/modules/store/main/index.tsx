import { House, ShoppingCart, User } from "lucide-react";

import { Outlet } from "react-router";
import clsx from "clsx";

interface NavButtonProps {
  icon: React.ReactNode;
  label: string;
  selected: boolean;
}

function NavButton({ icon, label, selected }: NavButtonProps) {
  return (
    <button
      className={clsx(
        "flex flex-col items-center justify-center gap-1 text-center",
        selected ? "font-bold" : "font-normal",
      )}
    >
      <div>{icon}</div>
      <span className="text-xs">{label}</span>
    </button>
  );
}

export function Store() {
  return (
    <main className="flex h-dvh w-full flex-col">
      <div className="relative flex-1 overflow-hidden">
        <Outlet />
      </div>

      <nav className="flex justify-between border-t-1 border-t-gray-400 px-6 py-2">
        <NavButton
          icon={<House className="size-5" />}
          label="Início"
          selected
        />
        <NavButton
          icon={<ShoppingCart className="size-5" />}
          label="Carrinho"
          selected={false}
        />
        <NavButton
          icon={<User className="size-5" />}
          label="Perfil"
          selected={false}
        />
      </nav>
    </main>
  );
}
