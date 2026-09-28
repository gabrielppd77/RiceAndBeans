import React from "react";

import {
  AppBar,
  Box,
  IconButton,
  Typography,
  Menu,
  MenuItem,
  Divider,
  Avatar,
} from "@mui/material";
import { Menu as MenuIcon } from "@mui/icons-material";

import { Link, useNavigate } from "react-router-dom";

import { useAuth } from "@modules/core/@hooks/useAuth";
import { routes } from "@modules/routing/@consts/routes";
import { useGetGeneralData } from "@modules/user-settings/@hooks/useGetGeneralData";
import { useStore } from "@modules/core/@store";

export function Appbar() {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

  const toggleMenu = useStore((store) => store.menu.toggle);
  const { setToken } = useAuth();
  const navigate = useNavigate();

  const { data } = useGetGeneralData();

  function closeMenu() {
    setAnchorEl(null);
  }

  function handleLogout() {
    setToken("");
    closeMenu();
    navigate(routes.initial);
  }

  return (
    <AppBar
      position="fixed"
      elevation={1}
      sx={{
        bgcolor: "primary.light",
        zIndex: (theme) => theme.zIndex.drawer + 1,
      }}
      className="px-6"
    >
      <Box
        sx={{
          display: "flex",
          gap: 1,
          width: "100%",
          justifyContent: "space-between",
        }}
      >
        <Box
          sx={{
            display: "flex",
            gap: 1,
            color: "primary.contrastText",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <IconButton color="inherit" onClick={() => toggleMenu()}>
            <MenuIcon />
          </IconButton>
          <Link
            to={routes.home}
            style={{
              textDecoration: "none",
            }}
          >
            <Typography sx={{ color: "primary.contrastText" }}>
              Rice & Beans Admin
            </Typography>
          </Link>
        </Box>

        <Box>
          <IconButton
            size="large"
            aria-label="account of current user"
            aria-controls="menu-appbar"
            aria-haspopup="true"
            onClick={(event) => setAnchorEl(event.currentTarget)}
            color="inherit"
          >
            <Avatar
              alt="Foto da pessoa"
              src={data?.urlImage}
              sx={{ width: 32, height: 32 }}
            />
          </IconButton>
          <Menu
            id="menu-appbar"
            anchorEl={anchorEl}
            anchorOrigin={{
              vertical: "bottom",
              horizontal: "center",
            }}
            keepMounted
            transformOrigin={{
              vertical: "top",
              horizontal: "center",
            }}
            open={Boolean(anchorEl)}
            onClose={() => closeMenu()}
          >
            <div className="w-60 p-2 py-0">
              <MenuItem
                onClick={() => {
                  navigate(routes.profile);
                  closeMenu();
                }}
              >
                Perfil
              </MenuItem>
              <Divider />
              <MenuItem onClick={handleLogout}>Sair</MenuItem>
            </div>
          </Menu>
        </Box>
      </Box>
    </AppBar>
  );
}
