import { useGetStores } from "../data/hooks/useGetStores";
import { useGoTo } from "../../configuration/routing/hooks/useGoTo";

import { Container } from "../../components/container";
import { LinearProgress } from "../../components/linear-progress";

import { Navbar } from "./components/nav-bar";
import { CompanyCard } from "./components/company-card";
import { CompanyCardSkeleton } from "./components/company-card-skeleton";
import { Footer } from "./components/footer";

export function Home() {
  const { data: _d, isLoading, isFetching } = useGetStores();
  const { goToStore } = useGoTo();

  const data = _d || [];

  return (
    <main>
      <Navbar />

      <div className="h-appbar mb-2" />

      <LinearProgress active={isFetching} />

      <Container>
        <div className="mb-10">
          <h2 className="mb-2 text-lg font-medium">Lojas</h2>

          <div className="mb-2 grid grid-cols-1 gap-3 md:grid-cols-2 lg:grid-cols-3">
            {isLoading
              ? Array.from({ length: 5 }).map((_, index) => (
                  <CompanyCardSkeleton key={index} />
                ))
              : data.map((d) => (
                  <CompanyCard
                    key={d.companyId}
                    name={d.companyName}
                    image={d.companyUrlImage}
                    onClick={() => goToStore(d.companyPath)}
                  />
                ))}
          </div>
        </div>
        <Footer />
      </Container>
    </main>
  );
}
