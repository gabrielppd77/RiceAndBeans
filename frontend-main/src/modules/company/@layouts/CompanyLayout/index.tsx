import { Outlet, useLocation, useNavigate } from "react-router-dom";

import Box from "@mui/material/Box";
import Tab from "@mui/material/Tab";
import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";
import { Container } from "@mui/material";

import { routes } from "@modules/routing/@consts/routes";

export function CompanyLayout() {
  const location = useLocation();
  const navigate = useNavigate();
  const currentPath = location.pathname;

  return (
    <Container maxWidth="xl" className="p-2">
      <TabContext value={currentPath}>
        <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
          <TabList
            onChange={(_, value) => navigate(value)}
            aria-label="lab API tabs example"
          >
            <Tab label="Principal" value={routes.companyMain} />
          </TabList>
        </Box>
        <Outlet />
      </TabContext>
    </Container>
  );
}
