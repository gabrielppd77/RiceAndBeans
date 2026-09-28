import { Avatar, Container, Grid } from "@mui/material";
import { Outlet } from "react-router-dom";

import { Menu } from "./Menu";

import { useGetGeneralData } from "@modules/user-settings/@hooks/useGetGeneralData";

export function UserSettingsLayout() {
  const { data } = useGetGeneralData();

  return (
    <Container maxWidth="xl" className="py-4">
      <Grid container spacing={2}>
        <Grid size={{ xs: 12 }}>
          <div className="flex items-center gap-4">
            <Avatar
              alt="Foto da pessoa"
              src={data?.urlImage}
              sx={{ width: 56, height: 56 }}
            />
            <div className="flex flex-col">
              <div className="font-medium">{data?.name || "-"}</div>
              <div className="text-xs">Essa é a sua conta pessoal</div>
            </div>
          </div>
        </Grid>

        <Grid size={{ xs: 12, sm: 5, md: 4, lg: 2 }}>
          <Menu />
        </Grid>
        <Grid size={{ xs: 12, sm: 7, md: 8, lg: 10 }}>
          <Outlet />
        </Grid>
      </Grid>
    </Container>
  );
}
