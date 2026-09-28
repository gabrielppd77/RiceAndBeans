import {
  Avatar,
  Link,
  Grid,
  Box,
  Typography,
  Container,
  Stack,
  Divider,
  Button,
} from "@mui/material";
import { LockOutlined as LockOutlinedIcon } from "@mui/icons-material";
import { TextField } from "@modules/core/@components/TextField";

import { FormValidateProvider, z } from "@modules/core/@validation";
import { useNavigate } from "react-router-dom";
import { useRecoverPassword } from "../@hooks/useRecoverPassword";

import { confirmMessage } from "@libs/alert";
import { routes } from "@modules/routing/@consts/routes";

const schema = z.object({
  email: z
    .string({ message: "Informe o Email" })
    .min(1)
    .email("Informe um Email válido"),
});

export function ForgotPassword() {
  const navigate = useNavigate();

  const { mutateAsync, isPending } = useRecoverPassword();

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
        className="flex gap-4"
      >
        <div className="flex flex-col items-center">
          <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Esqueceu a senha?
          </Typography>
        </div>

        <Divider className="w-full" />

        <p className="text-center">
          Informe seu email para receber as instruções de recuperação de senha
        </p>

        <Divider className="w-full" />

        <Box sx={{ mt: 1, width: "100%" }}>
          <FormValidateProvider
            schema={schema}
            values={{ email: "" }}
            onSubmit={async (data) => {
              await mutateAsync(data.email);
              confirmMessage(() => navigate(routes.initial), {
                title: "Email de recuperação enviado com sucesso",
                text: "Você deve receber as instruções para recuperação de senha em poucos instantes em seu email.",
              });
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
            </Stack>

            <Button
              loading={isPending}
              type="submit"
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              fullWidth
            >
              Enviar recuperação de senha
            </Button>
          </FormValidateProvider>

          <Grid container justifyContent="flex-end">
            <Grid>
              <Link
                className="hover:cursor-pointer"
                onClick={() => navigate(routes.initial)}
                variant="body2"
              >
                Voltar
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
