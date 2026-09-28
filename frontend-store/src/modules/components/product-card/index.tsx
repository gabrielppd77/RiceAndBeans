import { Image } from "lucide-react";
import { useParams } from "react-router";
import { useGoTo } from "../../configuration/routing/hooks/useGoTo";
import { formatToCurrency } from "../../utils/currency";

interface ProductCardProps {
  name: string;
  price: number;
  urlImage?: string | null;
}

export function ProductCard({ name, price, urlImage }: ProductCardProps) {
  const { companyPath = "" } = useParams<{
    companyPath: string;
  }>();
  const { goToStore } = useGoTo();

  return (
    <div
      className="flex h-44 items-center justify-center"
      onClick={() => goToStore(companyPath, name)}
    >
      {urlImage ? (
        <div className="relative flex h-full w-full items-center justify-center bg-black object-cover">
          <div className="absolute top-0 right-0 left-0 flex px-1 pt-1">
            <span
              className="line-clamp-2 rounded-xs bg-red-500/90 px-1 text-xs text-white"
              title={name}
            >
              {name}
            </span>
          </div>
          <img
            alt={name}
            src={urlImage}
            loading="lazy"
            className="max-h-full max-w-full"
          />
          <div className="absolute bottom-0 left-0 pb-1 pl-1">
            <button className="drop-shadow-outline flex items-center justify-center gap-1 text-xs font-semibold text-white">
              <p>R$</p>
              <p>{formatToCurrency(price)}</p>
            </button>
          </div>
        </div>
      ) : (
        <Image className="h-44 w-full rounded-sm" />
      )}
    </div>
  );
}
