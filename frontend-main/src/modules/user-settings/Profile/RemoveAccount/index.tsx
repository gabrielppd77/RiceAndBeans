import { Delete } from "@mui/icons-material";
import { Button, Divider } from "@mui/material";

import { confirmMessage, confirmPassword } from "@libs/alert";

import { useAuth } from "@modules/core/@hooks/useAuth";
import { useRemoveAccount } from "@modules/user-settings/@hooks/useRemoveAccount";
import { useNavigate } from "react-router-dom";
import { routes } from "@modules/routing/@consts/routes";

export function RemoveAccount() {
  const { isPending, mutateAsync } = useRemoveAccount();
  const { setToken } = useAuth();
  const navigate = useNavigate();

  function handleRemoveAccount() {
    confirmPassword(async (password) => {
      await mutateAsync({
        params: {
          password,
        },
      });
      confirmMessage(
        () => {
          setToken("");
          navigate(routes.initial);
        },
        {
          title: "Conta removida com sucesso!",
          text: "Você será redirecionado para a página inicial.",
        },
      );
    });
  }

  return (
    <div className="flex flex-col gap-2">
      <h2 className="text-2xl font-medium text-red-600">Remover conta</h2>
      <Divider />
      <p>
        Atenção: no momento, não estamos realizando backup das contas. Caso você
        remova sua conta, ela será excluída permanentemente.
      </p>
      <div>
        <Button
          variant="contained"
          color="error"
          startIcon={<Delete />}
          fullWidth={false}
          loading={isPending}
          onClick={handleRemoveAccount}
        >
          Remover conta
        </Button>
      </div>
    </div>
  );
}
