import { useEffect, useState } from "react";

import { Box, Button, Container, Typography } from "@mui/material";

import { useNavigate, useSearchParams } from "react-router-dom";
import { useConfirmEmail } from "../@hooks/useConfirmEmail";
import { extractError } from "@libs/alert";
import { Warning } from "@mui/icons-material";
import { routes } from "@modules/routing/@consts/routes";
import { LinearProgress } from "@modules/core/@components/LinearProgress";

export function ConfirmEmail() {
  const [countdown, setCountdown] = useState(5);

  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");
  const navigate = useNavigate();
  const { mutateAsync, isPending, error, isError, isSuccess } =
    useConfirmEmail();

  const errorExtracted = error && isError ? extractError(error) : undefined;

  useEffect(() => {
    if (token) {
      mutateAsync(token);
    } else {
      navigate(routes.initial);
    }
  }, [token]);

  useEffect(() => {
    if (!isSuccess) return;

    const interval = setInterval(() => {
      setCountdown((prev) => {
        if (prev === 1) {
          clearInterval(interval);
          navigate(routes.initial);
        }
        return prev - 1;
      });
    }, 1000);

    return () => clearInterval(interval);
  }, [isSuccess]);

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
        <Typography component="h1" variant="h5">
          Confirmação de Email
        </Typography>

        <Box className="mt-8 w-full">
          <div>
            {isPending && (
              <div className="mb-4">
                <h3>Confirmando email...</h3>
                <LinearProgress isLoading />
              </div>
            )}
          </div>
          {errorExtracted && (
            <div className="flex flex-col gap-4 rounded border border-amber-900 bg-red-500 p-4 text-white">
              <div>
                <div className="flex w-full justify-center">
                  <Warning fontSize="large" />
                </div>
                <div>{errorExtracted.title}</div>
                <div>{errorExtracted.text}</div>
              </div>
              <Button
                color="secondary"
                onClick={() => navigate(routes.initial)}
              >
                Voltar para tela inicial
              </Button>
            </div>
          )}
          {isSuccess && (
            <p className="flex flex-col gap-4">
              <div>
                Email confirmado com sucesso! Redirecionando em {countdown}...
              </div>
              <Button onClick={() => navigate(routes.initial)}>
                Pressione aqui para redirecionar manualmente
              </Button>
            </p>
          )}
        </Box>
      </Box>
    </Container>
  );
}
