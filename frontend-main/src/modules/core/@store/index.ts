import { create } from "zustand";

interface StoreType {
  menu: {
    isOpen: boolean;
    toggle: () => void;
  };
}

export const useStore = create<StoreType>((set) => {
  return {
    menu: {
      isOpen: false,
      toggle: () =>
        set((state) => ({
          menu: {
            ...state.menu,
            isOpen: !state.menu.isOpen,
          },
        })),
    },
  };
});
