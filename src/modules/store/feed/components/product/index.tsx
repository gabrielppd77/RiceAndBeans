import { useEffect, useState, useRef } from "react";
import clsx from "clsx";
import { formatToCurrency } from "../../../../utils/currency";

import { Image, Plus } from "lucide-react";

import { Information } from "./components/information";

interface ProductProps {
  currentIndex: number;
  id: string;
  name: string;
  description?: string | null;
  urlImage?: string | null;
  categoryName: string;
  price: number;
}

export function Product({
  currentIndex,
  id,
  name,
  description,
  urlImage,
  categoryName,
  price,
}: ProductProps) {
  const [isOpenInformation, setOpenInformation] = useState(false);

  const [isOverflowing, setOverflowing] = useState(false);

  const clampedRef = useRef<HTMLParagraphElement>(null);
  const fullRef = useRef<HTMLParagraphElement>(null);

  useEffect(() => {
    setOpenInformation(false);
  }, [currentIndex]);

  useEffect(() => {
    if (clampedRef.current && fullRef.current) {
      const clampedHeight = clampedRef.current.offsetHeight;
      const fullHeight = fullRef.current.offsetHeight;
      setOverflowing(fullHeight > clampedHeight);
    }
  }, [description]);

  function handleToggleInformation() {
    if (isOverflowing) setOpenInformation((prev) => !prev);
  }

  return (
    <div
      key={id}
      className="relative flex h-full w-full snap-start snap-always items-center justify-center"
    >
      {urlImage ? (
        <img
          src={urlImage}
          alt={name}
          className="absolute inset-0 h-full w-full object-cover"
        />
      ) : (
        <div className="absolute inset-0 flex items-center justify-center object-cover">
          <Image className="h-2/3 min-h-28 w-2/3 min-w-28" />
        </div>
      )}

      <div
        className={clsx(
          "absolute inset-0 transition-colors duration-300",
          isOpenInformation ? "bg-black/40" : "bg-black/20",
        )}
        onClick={() => {
          if (isOpenInformation) handleToggleInformation();
        }}
      />

      <div className="absolute right-0 bottom-0 left-0 p-4">
        <div className="flex items-end justify-between gap-4">
          <div className="flex-1">
            <Information
              title={name}
              description={description || ""}
              categoryName={categoryName}
              isOpen={isOpenInformation}
              toggleOpen={() => handleToggleInformation()}
              clampedRef={clampedRef}
              fullRef={fullRef}
            />
          </div>

          <div className="flex flex-col gap-4">
            {urlImage ? (
              <div className="relative hover:brightness-95">
                <img
                  src={urlImage}
                  alt={name}
                  className="size-12 rounded-full bg-white/20 backdrop-blur-sm"
                />
                <div className="absolute right-0 -bottom-2.5 left-0 flex items-center justify-center">
                  <Plus
                    strokeWidth={4}
                    className="size-5 rounded-full bg-red-500/90 p-1"
                  />
                </div>
              </div>
            ) : (
              <button className="size-12 rounded-full bg-white/20 backdrop-blur-sm hover:brightness-95">
                <Image className="w-full" />
              </button>
            )}

            <button>
              <p className="text-2xl font-extrabold">R$</p>
              <p className="-mt-1.5 text-xs font-semibold">
                {formatToCurrency(price)}
              </p>
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
