import { useState } from "react";
import { useParams, useSearchParams } from "react-router";
import { useGoTo } from "../../configuration/routing/hooks/useGoTo";

import { useGetStoreData } from "../../store/data/hooks/useGetStoreData";

import { ChevronDown, ChevronLeft, Image } from "lucide-react";
import { LinearProgress } from "../../components/linear-progress";
import { Drawer } from "../../components/drawer";
import { ProductCard } from "../../components/product-card";

import { DrawerContent } from "./components/drawer-content";

export const paramQueryFieldCategory = "query";

export function SearchCategory() {
  const [isOpenDrawer, setOpenDrawer] = useState(false);

  const { companyPath = "", productName } = useParams<{
    companyPath: string;
    productName: string;
  }>();
  const [searchParams, setSearchParams] = useSearchParams();
  const { goToStore } = useGoTo();

  const queryCategory = searchParams.get(paramQueryFieldCategory);

  const { data, isLoading, isFetching } = useGetStoreData({
    params: { companyPath },
  });

  const products = data?.products || [];

  const productsFiltred = queryCategory
    ? products.filter((x) => x.categoryName === queryCategory)
    : products;

  return (
    <main className="flex h-dvh w-full flex-col">
      <header className="flex items-center justify-between gap-4 p-3">
        <a
          className="absolute"
          onClick={() => goToStore(companyPath, productName)}
        >
          <ChevronLeft />
        </a>

        {isLoading ? (
          <div className="mx-10 flex-1 animate-pulse rounded-sm bg-white/20 py-1.5 pr-2 pl-3">
            <p>Carregando...</p>
          </div>
        ) : (
          <button
            className="mx-10 grid w-full grid-cols-1 rounded-sm bg-white/10 py-1.5 pr-2 pl-3"
            onClick={() => setOpenDrawer(true)}
          >
            <span className="col-start-1 row-start-1 flex items-center gap-3 pr-6">
              <span className="block truncate">
                {queryCategory || "Selecione"}
              </span>
            </span>
            <ChevronDown
              aria-hidden="true"
              className="col-start-1 row-start-1 size-5 self-center justify-self-end"
            />
          </button>
        )}
      </header>

      <LinearProgress active={isFetching} />

      <div className="scrollbar-hide mt-1 flex-1 overflow-y-auto">
        <div className="grid grid-cols-3 gap-1">
          {isLoading ? (
            <div className="flex h-44 w-full animate-pulse items-center justify-center">
              <Image className="h-44 w-full rounded-sm" />
            </div>
          ) : (
            productsFiltred.map((x) => (
              <ProductCard key={x.id} name={x.name} urlImage={x.urlImage} />
            ))
          )}
        </div>
      </div>

      <Drawer
        isOpen={isOpenDrawer}
        onChange={setOpenDrawer}
        content={
          <DrawerContent
            products={products}
            categoryCurrent={queryCategory || ""}
            onChangeCategory={(d) => {
              setSearchParams({ [paramQueryFieldCategory]: d || "" });
              setOpenDrawer(false);
            }}
          />
        }
      />
    </main>
  );
}
