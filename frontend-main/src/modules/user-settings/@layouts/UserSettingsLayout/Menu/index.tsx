import Divider from "@mui/material/Divider";
import MenuList from "@mui/material/MenuList";
import MenuItem from "@mui/material/MenuItem";
import ListItemText from "@mui/material/ListItemText";
import ListItemIcon from "@mui/material/ListItemIcon";
import { Person } from "@mui/icons-material";

import { useLocation, useNavigate } from "react-router-dom";
import { routes } from "@modules/routing/@consts/routes";

export function Menu() {
  const location = useLocation();
  const navigate = useNavigate();
  const currentPath = location.pathname;

  return (
    <MenuList>
      <MenuItem
        selected={currentPath == routes.profile}
        onClick={() => navigate(routes.profile)}
      >
        <ListItemIcon>
          <Person fontSize="small" />
        </ListItemIcon>
        <ListItemText>Perfil</ListItemText>
      </MenuItem>
      <Divider />
    </MenuList>
  );
}
