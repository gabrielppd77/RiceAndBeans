import { Image } from "lucide-react";
import { useParams } from "react-router";
import { useGoTo } from "../../configuration/routing/hooks/useGoTo";

interface ProductCardProps {
  name: string;
  urlImage?: string | null;
}

export function ProductCard({ name, urlImage }: ProductCardProps) {
  const { companyPath = "" } = useParams<{
    companyPath: string;
  }>();
  const { goToStore } = useGoTo();

  //todo: add price and name
  return (
    <div
      className="flex h-44 w-full items-center justify-center"
      onClick={() => goToStore(companyPath, name)}
    >
      {urlImage ? (
        <img alt={name} src={urlImage} className="h-44 w-full rounded-sm" />
      ) : (
        <Image className="h-44 w-full rounded-sm" />
      )}
    </div>
  );
}
