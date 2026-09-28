import { useState } from "react";

export function useDialog<TData>(startData: TData) {
  const [data, setData] = useState(startData);
  const [isOpen, setOpen] = useState(false);

  function toggle(d: TData) {
    setOpen((prev) => !prev);
    setData(d);
  }

  return {
    toggle,
    data,
    isOpen,
  };
}
