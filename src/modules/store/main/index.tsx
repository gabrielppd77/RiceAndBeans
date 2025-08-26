import { House, ShoppingCart, User } from "lucide-react";

import { Outlet } from "react-router";

export function Store() {
  return (
    <main className="flex h-dvh w-full flex-col">
      <div className="relative flex-1 overflow-hidden">
        <Outlet />
      </div>

      <nav className="flex justify-between border-t-1 border-gray-600 px-4 py-3">
        <button>
          <User />
        </button>
        <button>
          <House />
        </button>
        <button>
          <ShoppingCart />
        </button>
      </nav>
    </main>
  );
}
