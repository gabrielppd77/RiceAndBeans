import { Box, Divider, Typography } from "@mui/material";

interface PageHeaderProps {
  title: string;
  renderRight?: React.ReactNode;
}

export function PageHeader({ title, renderRight }: PageHeaderProps) {
  return (
    <Box>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 1,
        }}
      >
        <Typography variant="h6">{title}</Typography>
        <Box>{renderRight}</Box>
      </Box>
      <Divider />
    </Box>
  );
}
