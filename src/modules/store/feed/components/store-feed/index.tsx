import { Image } from "lucide-react";

interface StoreFeedProps {
  name: string;
  description?: string | null;
  urlImage?: string | null;
}

export function StoreFeed({ name, description, urlImage }: StoreFeedProps) {
  return (
    <div className="relative flex h-full w-full snap-start snap-always items-center justify-center">
      <div className="flex flex-col items-center gap-4">
        {urlImage ? (
          <img
            src={urlImage}
            alt={name}
            className="size-20 rounded-full bg-white/20 backdrop-blur-sm"
          />
        ) : (
          <button className="size-20 rounded-full bg-white/20 backdrop-blur-sm hover:brightness-95">
            <Image className="w-full" />
          </button>
        )}
        <h1 className="font-semibold">{name}</h1>
        <span className="line-clamp-3">{description}</span>
      </div>
    </div>
  );
}
