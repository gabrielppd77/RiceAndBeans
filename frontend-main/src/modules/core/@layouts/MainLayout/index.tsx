import { Box, Theme, useMediaQuery } from "@mui/material";

import { Navigate, Outlet } from "react-router-dom";

import { Appbar } from "./Appbar";
import { Sidebar } from "./Sidebar";

import { useAuth } from "@modules/core/@hooks/useAuth";
import { useStore } from "@modules/core/@store";

import { drawerWidthClose, drawerWidthOpen } from "@store/constants";

export function MainLayout() {
  const { token } = useAuth();
  const isOpenMenu = useStore((store) => store.menu.isOpen);
  const isSmallScreen = useMediaQuery((theme: Theme) =>
    theme.breakpoints.down("sm"),
  );

  const drawerWidth = isSmallScreen
    ? 0
    : isOpenMenu
      ? drawerWidthOpen
      : drawerWidthClose;

  if (!token) {
    return <Navigate to="/" />;
  }

  return (
    <Box>
      <Appbar />
      <Sidebar />
      <Box
        sx={(theme) => ({
          marginLeft: drawerWidth + "px",
          transition: theme.transitions.create(["margin", "width"], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
          }),
        })}
      >
        <div className="h-14" />
        <Outlet />
      </Box>
    </Box>
  );
}
