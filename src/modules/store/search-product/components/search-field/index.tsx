import { useRef } from "react";
import { CircleX, Search } from "lucide-react";

interface SearchFieldProps {
  value: string;
  onChange: (value: string) => void;
}

export function SearchField({ value, onChange }: SearchFieldProps) {
  const inputRef = useRef<HTMLInputElement | null>(null);

  return (
    <div className="transform rounded-sm py-1.5 shadow transition-transform duration-200 ease-out">
      <div className="relative h-full">
        <div className="pointer-events-none absolute inset-y-0 start-0 flex items-center ps-2">
          <Search className="size-6" />
        </div>
        <input
          ref={inputRef}
          value={value}
          onChange={(e) => onChange(e.target.value)}
          type="text"
          className="block h-full w-full rounded ps-10 font-normal focus:outline-none"
          placeholder="Buscar na loja"
          autoFocus
        />
        {value && (
          <div
            onClick={() => {
              onChange("");
              if (inputRef.current) inputRef.current.focus();
            }}
            className="absolute inset-y-0 end-2 flex items-center"
          >
            <CircleX className="size-6 rounded-full" />
          </div>
        )}
      </div>
    </div>
  );
}
