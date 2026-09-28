import { routes } from "@modules/routing/@consts/routes";

interface MenuProps {
  title: string;
  icon: string;
  link: string;
}

const menu: MenuProps[] = [
  {
    title: "Loja",
    icon: "apartment",
    link: routes.companyMain,
  },
  {
    title: "Categorias",
    icon: "category",
    link: routes.category,
  },
  {
    title: "Produtos",
    icon: "store",
    link: routes.product,
  },
];

export default menu;
