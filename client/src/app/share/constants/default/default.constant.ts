import {
  NavIndicatorProps,
  NavDIWithImageProps,
  NavDIWithIconProps,
  SidebarSubMenuItemProps,
  SidebarLegendItemProps,
} from "@ShareUis";

import { FooterProps } from "@ShareLayouts";

const itemFooter: FooterProps = {
  website: "Elsaspeak.com",
  year: 2024,
  link: "/",
  version: "0.0.0.1",
};

const itemWImages: NavDIWithImageProps[] = [
  {
    id: "1",
    image: "/images/avatar/2.svg",
    imageAlt: "image",
    title: "David Grey",
    content: "The meeting is cancelled",
    link: "#",
  },
  {
    id: "2",
    image: "/images/avatar/3.svg",
    imageAlt: "image",
    title: "David Grey",
    content: "The meeting is cancelled",
    link: "#",
  },
];

const itemWIcons: NavDIWithIconProps[] = [
  {
    id: "1",
    icon: "typcn-info-large",
    iconType: "bg-success",
    title: "Application Error",
    content: "Just now",
    link: "#",
  },
  {
    id: "2",
    icon: "typcn-info-large",
    iconType: "bg-success",
    title: "Application Error",
    content: "Just now",
    link: "#",
  },
];

const itemIndicatorMessage: NavIndicatorProps = {
  icon: "typcn-message-typing",
  link: "#",
  count: 2,
  countType: "bg-success",
};

const itemIndicatorNotification: NavIndicatorProps = {
  icon: "typcn-bell",
  link: "#",
  count: 2,
  countType: "bg-danger",
};

const detailSubMenus1: SidebarSubMenuItemProps[] = [
  {
    title: "Buttons",
    link: "#",
    id: "1",
  },
  {
    title: "Dropdowns",
    link: "#",
    id: "2",
  },
  {
    title: "Typography",
    link: "#",
    id: "3",
  },
];

const detailSubMenus2: SidebarSubMenuItemProps[] = [
  {
    title: "Basic Elements",
    link: "#",
    id: "1",
  },
];

const detailSubMenus3: SidebarSubMenuItemProps[] = [
  {
    title: "Login",
    link: "#",
    id: "1",
  },
  {
    title: "Register",
    link: "#",
    id: "2",
  },
];

const detailLegends: SidebarLegendItemProps[] = [
  {
    title: "#Sales",
    link: "#",
    id: "1",
  },
  {
    title: "#Marketing",
    link: "#",
    id: "2",
  },
];

export const DefaultData = {
  itemFooter,
  itemWImages,
  itemWIcons,
  itemIndicatorMessage,
  itemIndicatorNotification,
  detailSubMenus1,
  detailSubMenus2,
  detailSubMenus3,
  detailLegends,
};

export default DefaultData;
