import clsx from "clsx";
import { Store } from "lucide-react";

type AvatarSize = "normal" | "large";

const sizeMap: Record<AvatarSize, string> = {
  normal: "size-20",
  large: "size-24",
};

interface AvatarProps {
  alt?: string;
  image?: string | null;
  size?: AvatarSize;
}

export function Avatar({
  alt = "Imagem do avatar",
  image,
  size = "normal",
}: AvatarProps) {
  const classNameProp = clsx(
    sizeMap[size],
    "flex justify-center items-center",
    "rounded-full border p-[2px]",
  );

  if (image) {
    return (
      <img alt={alt} src={image} className={classNameProp} loading="lazy" />
    );
  }

  return (
    <div className={classNameProp}>
      <Store className="size-14 text-gray-400" />
    </div>
  );
}
