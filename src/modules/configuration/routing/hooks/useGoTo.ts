import { useNavigate } from "react-router";

interface ParamQuery {
  query?: string;
}

function toQueryString(params: ParamQuery): string {
  const searchParams = new URLSearchParams();

  Object.entries(params).forEach(([key, value]) => {
    searchParams.append(key, String(value));
  });

  return `?${searchParams.toString()}`;
}

export function useGoTo() {
  const navigate = useNavigate();

  function goToHome() {
    navigate("/");
  }

  function goToStore(companyPath: string, productName?: string) {
    navigate(`/${companyPath}${productName ? `/${productName}` : ""}`);
  }

  function goToSearchProduct(companyPath: string, productName?: string) {
    navigate(
      `/${companyPath}${productName ? `/${productName}` : ""}/pesquisar-produto`,
    );
  }

  function goToSearchCategory(
    companyPath: string,
    params: ParamQuery,
    productName?: string,
  ) {
    navigate(
      `/${companyPath}${productName ? `/${productName}` : ""}/pesquisar-categoria${toQueryString(params)}`,
    );
  }

  return { goToHome, goToStore, goToSearchProduct, goToSearchCategory };
}
