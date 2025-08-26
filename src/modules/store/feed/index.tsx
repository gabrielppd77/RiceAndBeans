import { useCallback, useEffect, useMemo, useRef, useState } from "react";

import { Search } from "lucide-react";

import { useNavigate, useParams } from "react-router";
import { useGoTo } from "../../configuration/routing/hooks/useGoTo";
import { useGetStoreData } from "../data/hooks/useGetStoreData";

import { LinearProgress } from "../../components/linear-progress";

import { ProductSkeleton } from "./components/product-skeleton";
import { Product } from "./components/product";
import { PositionIndicator } from "./components/position-indicator";

export function Feed() {
  const [currentIndex, setCurrentIndex] = useState(0);
  const containerRef = useRef<HTMLDivElement>(null);

  const { companyPath = "", productName } = useParams<{
    companyPath: string;
    productName: string;
  }>();

  const { goToStore, goToSearchProduct } = useGoTo();
  const navigate = useNavigate();

  const { data, isLoading, isFetching } = useGetStoreData({
    params: { companyPath },
  });

  const products = useMemo(() => data?.products || [], [data]);

  const categories = [...new Set(products.map((p) => p.categoryName))];

  const indexCategory = categories.findIndex(
    (x) => x === products[currentIndex].categoryName,
  );

  const handleScroll = useCallback(() => {
    if (containerRef.current) {
      const scrollTop = containerRef.current.scrollTop;
      const itemHeight = containerRef.current.clientHeight;
      const newIndex = Math.round(scrollTop / itemHeight);

      if (newIndex !== currentIndex) {
        navigate(`/${companyPath}/${products[newIndex].name}`);
        setCurrentIndex(newIndex);
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [products, currentIndex]);

  useEffect(() => {
    const container = containerRef.current;
    if (container) {
      container.addEventListener("scroll", handleScroll);
      return () => container.removeEventListener("scroll", handleScroll);
    }
  }, [handleScroll]);

  useEffect(() => {
    if (productName) {
      const index = products.findIndex((x) => x.name === productName);
      if (index >= 0) {
        scrollToProduct(index);
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [products]);

  const scrollToProduct = (index: number) => {
    if (containerRef.current) {
      const itemHeight = containerRef.current.clientHeight;
      containerRef.current.scrollTo({
        top: index * itemHeight,
        behavior: "smooth",
      });
    }
  };

  function handleScrollToCategory(categoryIndex: number) {
    const index = products.findIndex(
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
          <ProductSkeleton />
        ) : (
          products.map((product) => (
            <Product
              currentIndex={currentIndex}
              key={product.id}
              id={product.id}
              name={product.name}
              description={product.description}
              urlImage={product.urlImage}
              categoryName={product.categoryName}
              price={product.price}
            />
          ))
        )}
      </div>

      <div className="absolute top-0 right-0 left-0 flex flex-col">
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
            <h1 className="font-medium">{data?.name || "Rice & Beans"}</h1>
          </a>

          <a
            onClick={() =>
              goToSearchProduct(companyPath, { query: "" }, productName)
            }
          >
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

      {/* <NavControls
        currentIndex={indexCategory}
        listCount={categories.length}
        onClick={handleScrollToCategory}
      /> */}
    </div>
  );
}
