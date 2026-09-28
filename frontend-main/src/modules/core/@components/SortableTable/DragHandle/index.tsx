import { useContext } from "react";
import { SortableItemContext } from "../Context";

interface DragHandleProps {
  children: React.ReactNode;
  className: string;
}

export function DragHandle({ children, className }: DragHandleProps) {
  const { attributes, listeners, ref } = useContext(SortableItemContext);

  return (
    <button className={className} {...attributes} {...listeners} ref={ref}>
      {children}
    </button>
  );
}
