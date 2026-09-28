import {
  Avatar,
  Grid,
  Box,
  Link,
  Typography,
  Container,
  Button,
} from "@mui/material";
import { LockOutlined as LockOutlinedIcon } from "@mui/icons-material";
import { TextField } from "@modules/core/@components/TextField";
import { TextFieldPassword } from "@modules/core/@components/TextFieldPassword";

import { FormValidateProvider, z } from "@modules/core/@validation";
import { useNavigate } from "react-router-dom";
import { useAuth } from "@modules/core/@hooks/useAuth";
import { useRegister } from "../@hooks/useRegister";
import { routes } from "@modules/routing/@consts/routes";

const schema = z
  .object({
    name: z.string({ message: "Informe o Nome" }).min(1),
    email: z
      .string({ message: "Informe o Email" })
      .min(1)
      .email("Informe um Email válido"),
    companyName: z.string({ message: "Informe o Nome da Loja" }).min(1),
    password: z.string({ message: "Informe a senha" }).min(1),
    confirmPassword: z
      .string({ message: "Informe a confirmação da senha" })
      .min(1),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "As senhas não correspondem",
    path: ["confirmPassword"],
  });

export function Register() {
  const { mutateAsync, isPending } = useRegister();

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
          Cadastre
        </Typography>
        <Box sx={{ mt: 3 }}>
          <FormValidateProvider
            schema={schema}
            values={{
              companyName: "",
              confirmPassword: "",
              email: "",
              name: "",
              password: "",
            }}
            onSubmit={async (data) => {
              const response = await mutateAsync(data);
              setToken(response.token);
              navigate(routes.home);
            }}
          >
            <Grid container spacing={1}>
              <Grid size={12}>
                <TextField
                  name="name"
                  autoComplete="given-name"
                  autoFocus
                  required
                  sx={{ ariaLabel: "nome" }}
                  label="Nome"
                />
              </Grid>

              <Grid size={12}>
                <TextField
                  type="email"
                  name="email"
                  placeholder="seuemail@email.com"
                  autoComplete="email"
                  required
                  sx={{ ariaLabel: "email" }}
                  label="Email"
                />
              </Grid>
              <Grid size={12}>
                <TextField
                  name="companyName"
                  required
                  variant="outlined"
                  sx={{ ariaLabel: "companyName" }}
                  label="Nome da loja"
                />
              </Grid>
              <Grid size={12}>
                <TextFieldPassword
                  required
                  label="Senha"
                  name="password"
                  autoComplete="new-password"
                />
              </Grid>
              <Grid size={12}>
                <TextFieldPassword
                  required
                  name="confirmPassword"
                  label="Confirme a senha"
                  autoComplete="new-password"
                />
              </Grid>
            </Grid>

            <Button
              loading={isPending}
              variant="contained"
              type="submit"
              sx={{ mt: 3, mb: 2 }}
              fullWidth
            >
              Cadastrar
            </Button>
          </FormValidateProvider>

          <Grid container justifyContent="flex-end">
            <Grid>
              <Link
                className="hover:cursor-pointer"
                onClick={() => navigate(routes.initial)}
                variant="body2"
              >
                Já tem uma conta? Entre
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
