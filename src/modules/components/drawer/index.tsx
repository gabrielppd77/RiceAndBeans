import clsx from "clsx";

interface DrawerProps {
  isOpen: boolean;
  onChange: (isOpen: boolean) => void;
  content: React.ReactNode;
}

export function Drawer({ isOpen, onChange, content }: DrawerProps) {
  return (
    <div>
      <div
        className={clsx(
          "fixed inset-0 bg-black/50 transition-opacity duration-300",
          isOpen ? "visible opacity-100" : "invisible opacity-0",
        )}
        onClick={() => onChange(false)}
      />

      <div
        className={clsx(
          "fixed bottom-0 flex h-[85%] min-h-64 w-full transform flex-col rounded-md bg-white shadow-lg transition-all duration-300",
          isOpen ? "translate-y-0" : "translate-y-full",
        )}
      >
        <button
          className="flex items-center justify-center p-2"
          onClick={() => onChange(false)}
        >
          <div className="h-1 w-8 rounded-lg bg-gray-300" />
        </button>

        <div className="scrollbar-hide flex-1 overflow-auto">{content}</div>
      </div>
    </div>
  );
}
