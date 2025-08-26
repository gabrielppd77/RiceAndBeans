import { Image, Store } from "lucide-react";

export function ProductSkeleton() {
  return (
    <div className="relative flex h-full w-full snap-start snap-always items-center justify-center">
      <div className="absolute inset-0 flex h-full w-full items-center justify-center object-cover">
        <Store className="h-2/3 min-h-28 w-2/3 min-w-28 animate-pulse" />
      </div>

      <div className="absolute inset-0 bg-black/20" />

      <div className="absolute right-0 bottom-0 left-0 p-4">
        <div className="flex items-end justify-between gap-4">
          <div className="flex-1">
            <div className="animate-pulse">
              <h3 className="mb-1 w-2/3 rounded bg-white/20 text-lg font-semibold">
                <div className="invisible">empty</div>
              </h3>

              <div className="flex flex-col gap-1 text-sm">
                <div className="w-full rounded bg-white/20">
                  <p className="invisible">empty</p>
                </div>
                <div className="w-full rounded bg-white/20">
                  <p className="invisible">empty</p>
                </div>
              </div>
            </div>
          </div>

          <div className="flex animate-pulse flex-col gap-4">
            <button className="h-12 w-12 rounded-full bg-white/20 backdrop-blur-sm">
              <Image className="w-full" />
            </button>

            <button className="h-12 w-12 rounded-full bg-white/20 backdrop-blur-sm" />
          </div>
        </div>
      </div>
    </div>
  );
}
