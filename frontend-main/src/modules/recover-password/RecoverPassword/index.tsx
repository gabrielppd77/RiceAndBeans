import { useEffect } from "react";

import {
  Avatar,
  Box,
  Button,
  Container,
  Grid,
  Typography,
} from "@mui/material";
import { TextFieldPassword } from "@modules/core/@components/TextFieldPassword";
import { LockOutlined } from "@mui/icons-material";

import { useNavigate, useSearchParams } from "react-router-dom";
import { useResetPassword } from "../@hooks/useResetPassword";
import { FormValidateProvider, z } from "@modules/core/@validation";

import { confirmMessage } from "@libs/alert";
import { routes } from "@modules/routing/@consts/routes";

const schema = z
  .object({
    password: z.string({ message: "Informe a senha" }).min(1),
    confirmPassword: z
      .string({ message: "Informe a confirmação da senha" })
      .min(1),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "As senhas não correspondem",
    path: ["confirmPassword"],
  });

export function RecoverPassword() {
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token") || "";

  const navigate = useNavigate();
  const { mutateAsync, isPending } = useResetPassword();

  useEffect(() => {
    if (!token) {
      navigate(routes.initial);
    }
  }, [token]);

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
          <LockOutlined />
        </Avatar>
        <Typography component="h1" variant="h5">
          Digite a sua nova senha
        </Typography>

        <Box className="mt-4 w-full">
          <FormValidateProvider
            schema={schema}
            values={{ confirmPassword: "", password: "" }}
            onSubmit={async (data) => {
              await mutateAsync({ token, newPassword: data.password });
              confirmMessage(() => navigate(routes.initial), {
                title: "A sua senha foi alterada com sucesso",
                text: "Você será redirecionado para a página inicial para entrar no sistema novamente.",
              });
            }}
          >
            <Grid container spacing={1}>
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
              Alterar senha
            </Button>
          </FormValidateProvider>
        </Box>
      </Box>
    </Container>
  );
}
