interface PositionIndicatorProps {
  listCount: number;
  currentIndex: number;
  onClick: (index: number) => void;
}

export function PositionIndicator({
  listCount,
  currentIndex,
  onClick,
}: PositionIndicatorProps) {
  // TODO: component are over when open description of product
  return (
    <div className="absolute top-1/2 left-4 flex -translate-y-1/2 transform flex-col gap-2">
      {Array.from({ length: listCount }).map((_, index) => (
        <div
          key={index}
          className={`h-8 w-2 rounded-full transition-all duration-300 ${
            index === currentIndex ? "bg-white" : "bg-white/30"
          }`}
          onClick={() => onClick(index)}
        />
      ))}
    </div>
  );
}
