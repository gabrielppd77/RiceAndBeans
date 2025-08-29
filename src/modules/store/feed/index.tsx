import { useCallback, useEffect, useMemo, useRef, useState } from "react";

import { Search } from "lucide-react";

import { useParams } from "react-router";
import { useGoTo } from "../../configuration/routing/hooks/useGoTo";
import { useGetStoreData } from "../data/hooks/useGetStoreData";

import { LinearProgress } from "../../components/linear-progress";

import { Skeleton } from "./components/skeleton";
import { ProductFeed } from "./components/product-feed";
import { PositionIndicator } from "./components/position-indicator";
import { StoreFeed } from "./components/store-feed";

export function Feed() {
  const [currentIndex, setCurrentIndex] = useState(0);
  const containerRef = useRef<HTMLDivElement>(null);

  const { companyPath = "", productName } = useParams<{
    companyPath: string;
    productName: string;
  }>();

  const { goToStore, goToSearchProduct } = useGoTo();

  const { data, isLoading, isFetching } = useGetStoreData({
    params: { companyPath },
  });

  const products = useMemo(() => data?.products || [], [data]);
  const productsWithIndexFixed = useMemo(
    () => [
      { index: 0, name: "", categoryName: "" },
      ...products.map((p, index) => ({
        index: index + 1,
        name: p.name,
        categoryName: p.categoryName,
      })),
    ],
    [products],
  );

  const categories = [...new Set(products.map((p) => p.categoryName))];

  const indexCategory = categories.findIndex(
    (x) => x === productsWithIndexFixed[currentIndex].categoryName,
  );

  const handleScroll = useCallback(() => {
    if (containerRef.current) {
      const scrollTop = containerRef.current.scrollTop;
      const itemHeight = containerRef.current.clientHeight;
      const newIndex = Math.round(scrollTop / itemHeight);
      if (newIndex !== currentIndex) {
        goToStore(companyPath, productsWithIndexFixed[newIndex]?.name);
        setCurrentIndex(newIndex);
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [productsWithIndexFixed, currentIndex]);

  useEffect(() => {
    const container = containerRef.current;
    if (container) {
      container.addEventListener("scrollend", handleScroll);
      return () => container.removeEventListener("scrollend", handleScroll);
    }
  }, [handleScroll]);

  useEffect(() => {
    if (productName) {
      const index = productsWithIndexFixed.findIndex(
        (x) => x.name === productName,
      );
      if (index >= 0) {
        scrollToProduct(index, false);
      }
    }
  }, [productName, productsWithIndexFixed]);

  const scrollToProduct = (index: number, isSmooth: boolean = true) => {
    if (containerRef.current) {
      const itemHeight = containerRef.current.clientHeight;
      containerRef.current.scrollTo({
        top: index * itemHeight,
        behavior: isSmooth ? "smooth" : "instant",
      });
    }
  };

  function handleScrollToCategory(categoryIndex: number) {
    const index = productsWithIndexFixed.findIndex(
      (x) => x.categoryName === categories[categoryIndex],
    );
    if (index >= 0) {
      scrollToProduct(index);
    }
  }

  return (
    <div className="h-full">
      <div
        ref={containerRef}
        className="scrollbar-hide h-full w-full snap-y snap-mandatory overflow-y-auto"
      >
        {isLoading ? (
          <Skeleton />
        ) : (
          <>
            {data && (
              <StoreFeed
                name={data.name}
                description={data.description}
                urlImage={data.urlImage}
              />
            )}
            {products.map((product) => (
              <ProductFeed
                currentIndex={currentIndex}
                key={product.id}
                id={product.id}
                name={product.name}
                description={product.description}
                urlImage={product.urlImage}
                categoryName={product.categoryName}
                price={product.price}
              />
            ))}
          </>
        )}
      </div>

      <div className="drop-shadow-outline absolute top-0 right-0 left-0 flex flex-col text-white">
        <div className="flex items-center justify-between p-3">
          <a
            className="flex items-center gap-2"
            onClick={() => {
              scrollToProduct(0);
              goToStore(companyPath);
            }}
          >
            <img
              src="/rice-and-beans-logo.svg"
              alt="logo rice and beans"
              className="size-8"
            />
            <h1 className="font-medium">{data?.name || "Rice&Beans"}</h1>
          </a>

          <a onClick={() => goToSearchProduct(companyPath, productName)}>
            <Search />
          </a>
        </div>
        <LinearProgress active={isFetching} />
      </div>

      <PositionIndicator
        currentIndex={indexCategory}
        listCount={categories.length}
        onClick={handleScrollToCategory}
      />
    </div>
  );
}
