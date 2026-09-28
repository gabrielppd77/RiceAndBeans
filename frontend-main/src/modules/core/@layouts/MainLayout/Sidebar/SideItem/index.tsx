import {
  Icon,
  ListItem,
  ListItemButton,
  ListItemText,
  Tooltip,
} from "@mui/material";
import { Link, useLocation } from "react-router-dom";

interface SideItemProps {
  icon: string;
  title: string;
  link: string;
  showText: boolean;
}

export function SideItem({ icon, title, link, showText }: SideItemProps) {
  const { pathname } = useLocation();

  const isSelected = pathname == link;

  return (
    <Link
      to={link}
      style={{
        textDecoration: "none",
      }}
    >
      <Tooltip title={showText ? "" : title} placement="right">
        <ListItem
          sx={{
            borderRadius: 1,
            bgcolor: isSelected ? "primary.light" : "",
            transition: "background-color 0.3s ease",
            filter: isSelected ? "brightness(100%)" : "brightness(80%)",
            color: isSelected ? "primary.contrastText" : "text.primary",
            height: 40,
          }}
          disableGutters
          disablePadding
        >
          <ListItemButton
            sx={{
              px: 2,
              py: 0.5,
              display: "flex",
              gap: 1,
              height: "100%",
              borderRadius: 1,
            }}
            disableGutters
          >
            <Icon>{icon}</Icon>

            {showText && <ListItemText primary={title} />}
          </ListItemButton>
        </ListItem>
      </Tooltip>
    </Link>
  );
}
