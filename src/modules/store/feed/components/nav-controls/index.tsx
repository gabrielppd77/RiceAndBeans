import { ChevronDown, ChevronUp } from "lucide-react";

interface NavControlsProps {
  currentIndex: number;
  listCount: number;
  onClick: (index: number) => void;
}

export function NavControls({
  currentIndex,
  listCount,
  onClick,
}: NavControlsProps) {
  return (
    <div className="absolute top-1/2 right-4 flex -translate-y-1/2 transform flex-col gap-2">
      <button
        className="flex h-10 w-10 items-center justify-center rounded-full bg-white/20 backdrop-blur-sm hover:bg-white/30"
        onClick={() => onClick(Math.max(0, currentIndex - 1))}
        disabled={currentIndex === 0}
      >
        <ChevronUp className="h-5 w-5 text-white" />
      </button>

      <button
        className="flex h-10 w-10 items-center justify-center rounded-full bg-white/20 backdrop-blur-sm hover:bg-white/30"
        onClick={() => onClick(Math.min(listCount - 1, currentIndex + 1))}
        disabled={currentIndex === listCount - 1}
      >
        <ChevronDown className="h-5 w-5 text-white" />
      </button>
    </div>
  );
}
