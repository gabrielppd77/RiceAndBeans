import { Box, Drawer, List, Stack, Theme, useMediaQuery } from "@mui/material";

import { SideItem } from "./SideItem";

import { useStore } from "@modules/core/@store";

import menu from "@store/menu";
import { drawerWidthOpen, drawerWidthClose } from "@store/constants";

export function Sidebar() {
  const { isOpen, toggle } = useStore((store) => store.menu);

  const drawerWidth = isOpen ? drawerWidthOpen : drawerWidthClose;

  const isSmallScreen = useMediaQuery((theme: Theme) =>
    theme.breakpoints.down("sm"),
  );

  return (
    <Drawer
      open={isOpen}
      variant={isSmallScreen ? "temporary" : "permanent"}
      onClose={() => toggle()}
    >
      <Box
        sx={(theme) => ({
          height: "100%",
          color: "primary.contrastText",
          width: drawerWidth + "px",
          transition: theme.transitions.create(["margin", "width"], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
          }),
        })}
      >
        <Box
          sx={{
            px: 2,
            py: 1,
          }}
          className="mt-14"
        >
          <List disablePadding>
            <Stack spacing={1}>
              {menu.map(({ title, icon, link }) => (
                <SideItem
                  key={link}
                  title={title}
                  icon={icon}
                  link={link}
                  showText={isOpen}
                />
              ))}
            </Stack>
          </List>
        </Box>
      </Box>
    </Drawer>
  );
}
