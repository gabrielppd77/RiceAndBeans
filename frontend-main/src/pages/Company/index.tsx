import { Box, Button, CircularProgress, Stack } from "@mui/material";
import { QrCode2 } from "@mui/icons-material";

import { PageHeader } from "@modules/core/@components/PageHeader";
import { LinearProgress } from "@modules/core/@components/LinearProgress";

import { useCompanyGetCompany } from "@libs/queries/company/useCompanyGetCompany";
import { useCompanyGetQRCode } from "@libs/queries/company/useCompanyGetQRCode";

export default function Company() {
  const { isLoading, isFetching } = useCompanyGetCompany();

  const { mutateAsync: mutateAsyncGetQRCode, isPending: isPendingGetQRCode } =
    useCompanyGetQRCode();

  return (
    <Stack gap={1} p={2}>
      <PageHeader
        title="Loja"
        renderRight={isLoading && <CircularProgress size={25} />}
      />

      <LinearProgress isLoading={isFetching} />

      <Stack gap={1}>
        <Stack gap={1} flexDirection="row" alignItems="center">
          <Stack gap={1} width="100%"></Stack>
        </Stack>

        <Stack gap={1}>
          <Box>
            <Button
              loading={isPendingGetQRCode}
              onClick={async () => await mutateAsyncGetQRCode()}
              variant="outlined"
              startIcon={<QrCode2 />}
            >
              Gerar QR Code da Loja
            </Button>
          </Box>
          <Box></Box>
        </Stack>
      </Stack>
    </Stack>
  );
}
