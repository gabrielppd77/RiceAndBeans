import { Container } from "@mui/material";

import { PageHeader } from "@modules/core/@components/PageHeader";

export function Main() {
  return (
    <Container maxWidth="xl" className="p-2">
      <PageHeader title="Bem vindo" />
    </Container>
  );
}
