import { Avatar } from "../avatar";
import clsx from "clsx";

interface CompanyCardProps {
  name: string;
  image?: string | null;
  onClick: () => void;
}

export function CompanyCard({ name, image, onClick }: CompanyCardProps) {
  return (
    <div
      onClick={() => onClick()}
      className={clsx(
        "flex h-28 gap-2 rounded-md p-4",
        "shadow-md transition duration-200 ease-in-out hover:cursor-pointer",
      )}
    >
      <Avatar image={image} alt="icone da loja" />
      <div className="flex items-center">
        <div className="text-md line-clamp-1 font-medium">{name}</div>
      </div>
    </div>
  );
}
