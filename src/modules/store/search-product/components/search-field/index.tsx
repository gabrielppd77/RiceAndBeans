import { CircleX, Search } from "lucide-react";

interface SearchFieldProps {
  value: string;
  onChange: (value: string) => void;
}

export function SearchField({ value, onChange }: SearchFieldProps) {
  return (
    <div className="transform py-1.5 transition-transform duration-200 ease-out">
      <div className="relative h-full">
        <div className="pointer-events-none absolute inset-y-0 start-0 flex items-center ps-2">
          <Search className="size-6" />
        </div>
        <input
          value={value}
          onChange={(e) => onChange(e.target.value)}
          type="text"
          className="block h-full w-full rounded ps-10 font-normal focus:outline-none"
          placeholder="Buscar no cardápio"
          autoFocus
        />
        {value && (
          <div
            onClick={() => onChange("")}
            className="absolute inset-y-0 end-2 flex items-center"
          >
            <CircleX className="size-6 rounded-full" />
          </div>
        )}
      </div>
    </div>
  );
}
