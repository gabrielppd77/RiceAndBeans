import clsx from "clsx";
import { Avatar } from "../avatar";

export function CompanyCardSkeleton() {
  return (
    <div
      className={clsx("animate-pulse", "flex h-28 w-full gap-2 rounded-md p-4")}
    >
      <Avatar />
      <div className="flex w-full flex-col items-center justify-center gap-2 px-8">
        <div className="h-5 w-full rounded-full bg-gray-300" />
        <div className="h-3 w-full rounded-full bg-gray-300" />
        <div className="h-3 w-full rounded-full bg-gray-300" />
      </div>
    </div>
  );
}
