import {
  Avatar,
  Link,
  Grid,
  Box,
  Typography,
  Container,
  Stack,
  Button,
} from "@mui/material";
import { LockOutlined as LockOutlinedIcon } from "@mui/icons-material";
import { TextField } from "@modules/core/@components/TextField";
import { TextFieldPassword } from "@modules/core/@components/TextFieldPassword";

import { useNavigate } from "react-router-dom";
import { useAuth } from "@modules/core/@hooks/useAuth";
import { useLogin } from "../@hooks/useLogin";
import { routes } from "@modules/routing/@consts/routes";
import { FormValidateProvider, z } from "@modules/core/@validation";

const schema = z.object({
  email: z
    .string({ message: "Informe o Email" })
    .min(1)
    .email("Informe um Email válido"),
  password: z.string({ message: "Informe a senha" }).min(1),
});

export function Login() {
  const { mutateAsync, isPending } = useLogin();

  const { setToken } = useAuth();
  const navigate = useNavigate();

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Entre
        </Typography>
        <Box sx={{ mt: 1, width: "100%" }}>
          <FormValidateProvider
            schema={schema}
            values={{ email: "", password: "" }}
            onSubmit={async (d) => {
              const response = await mutateAsync(d);
              setToken(response.token);
              navigate(routes.home);
            }}
          >
            <Stack gap={1}>
              <TextField
                id="email"
                type="email"
                name="email"
                placeholder="seuemail@email.com"
                autoComplete="email"
                autoFocus
                required
                variant="outlined"
                sx={{ ariaLabel: "email" }}
                label="Email"
              />
              <TextFieldPassword
                name="password"
                autoComplete="current-password"
                required
                label="Senha"
              />
            </Stack>

            <Button
              loading={isPending}
              type="submit"
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              fullWidth
            >
              Entre
            </Button>
          </FormValidateProvider>

          <Grid container>
            <Grid size={{ xs: 12, sm: 6 }}>
              <div className="flex items-center justify-center sm:justify-start">
                <Link
                  className="hover:cursor-pointer"
                  onClick={() => navigate(routes.register)}
                  variant="body2"
                >
                  Não tem uma conta? Cadastre
                </Link>
              </div>
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <div className="flex items-center justify-center sm:justify-end">
                <Link
                  className="hover:cursor-pointer"
                  onClick={() => navigate(routes.forgotPasword)}
                  variant="body2"
                >
                  Esqueceu a senha?
                </Link>
              </div>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
