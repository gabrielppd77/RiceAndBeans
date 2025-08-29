import { useEffect, useState } from "react";

import { useParams } from "react-router";

import { useGetStoreData } from "../../store/data/hooks/useGetStoreData";

import { ChevronLeft } from "lucide-react";

import { LinearProgress } from "../../components/linear-progress";
import { ProductCard } from "../../components/product-card";
import { SearchField } from "./components/search-field";

import type { GetStoreProductResponse } from "../../store/data/types/GetStoreProductResponse";
import { useGoTo } from "../../configuration/routing/hooks/useGoTo";

interface CategoryWithProductData {
  category: string;
  products: GetStoreProductResponse[];
}

export function SearchProduct() {
  const [queryDebounced, setQueryDebounced] = useState("");
  const [query, setQuery] = useState<string | null>(null);
  const { goToStore } = useGoTo();

  const { companyPath = "", productName } = useParams<{
    companyPath: string;
    productName: string;
  }>();

  const { data, isLoading, isFetching } = useGetStoreData({
    params: { companyPath },
  });

  useEffect(() => {
    const timer = setTimeout(() => {
      if (query === null) return;
      setQueryDebounced(query);
    }, 500);
    return () => clearTimeout(timer);
  }, [query]);

  const products = data?.products || [];

  const productsFiltred = queryDebounced
    ? products.filter((x) =>
        x.name.toLowerCase().includes(queryDebounced.toLowerCase()),
      )
    : products;

  const categoryWithProducts: CategoryWithProductData[] = Object.values(
    productsFiltred.reduce<Record<string, CategoryWithProductData>>(
      (acc, product) => {
        if (!acc[product.categoryName]) {
          acc[product.categoryName] = {
            category: product.categoryName,
            products: [],
          };
        }
        acc[product.categoryName].products.push(product);
        return acc;
      },
      {},
    ),
  );

  return (
    <main className="flex h-full w-full flex-col">
      <header className="flex items-center justify-between gap-4 p-3">
        <a
          className="absolute"
          onClick={() => goToStore(companyPath, productName)}
        >
          <ChevronLeft />
        </a>

        {isLoading ? (
          <div className="mx-10 flex-1 animate-pulse rounded-sm py-1.5 pr-2 pl-3 shadow">
            <p>Carregando...</p>
          </div>
        ) : (
          <div className="mx-10 grid w-full grid-cols-1 rounded-sm">
            <SearchField value={query || ""} onChange={setQuery} />
          </div>
        )}
      </header>
      <LinearProgress active={isFetching} />

      {isLoading ? (
        <div className="mt-1 animate-pulse">
          <h1 className="mx-4 mb-3 w-2/3 min-w-12 rounded-sm bg-white/20 text-lg font-medium">
            <div className="invisible">empty</div>
          </h1>
          <div className="grid grid-cols-3 gap-1 bg-white/20">
            <div className="invisible h-44" />
          </div>
        </div>
      ) : productsFiltred.length === 0 ? (
        <div className="mt-12 flex flex-col items-center gap-4 p-8">
          <img
            src="/searching.svg"
            className="size-52"
            alt="Icone de procura"
          />
          <div className="flex flex-col items-center justify-center gap-2">
            <h1 className="text-center text-lg">Qual o pedido de hoje?</h1>
            <h2 className="text-center">
              Digite alguma palavra relacionada ao produto que você procura
            </h2>
          </div>
        </div>
      ) : (
        <div className="scrollbar-hide mt-1 flex flex-1 flex-col gap-4 overflow-y-auto px-4 py-1">
          {categoryWithProducts.map((d) => (
            <div key={d.category} className="flex flex-col">
              <h1 className="mb-3 text-lg font-medium select-none">
                {d.category}
              </h1>
              <span className="-mx-4 grid grid-cols-3 gap-1">
                {d.products.map((x) => (
                  <ProductCard key={x.id} name={x.name} urlImage={x.urlImage} />
                ))}
              </span>
            </div>
          ))}
        </div>
      )}
    </main>
  );
}
