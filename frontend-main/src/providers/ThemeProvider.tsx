import { ThemeProvider as ThemeProviderMain, createTheme } from "@mui/material";

const theme = createTheme({
  palette: {
    primary: {
      main: "#e7000b",
      "500": "#fb2c36",
    },
    secondary: {
      main: "#733e0a",
    },
  },
  components: {
    MuiButton: {
      defaultProps: {
        color: "primary",
        variant: "contained",
        size: "small",
        style: { textTransform: "none" },
      },
    },
    MuiFab: {
      defaultProps: {
        style: { textTransform: "none" },
      },
    },
    MuiSvgIcon: {
      defaultProps: {
        fontSize: "small",
      },
    },
    MuiIcon: {
      defaultProps: {
        fontSize: "small",
      },
    },
    MuiIconButton: {
      defaultProps: {
        size: "small",
        color: "primary",
      },
    },
    MuiTextField: {
      defaultProps: {
        size: "small",
        fullWidth: true,
      },
    },
    MuiTooltip: {
      defaultProps: {
        arrow: true,
      },
    },
    MuiTab: {
      defaultProps: {
        style: { textTransform: "none" },
      },
    },
    MuiTableCell: {
      styleOverrides: {
        root: {
          padding: 8,
        },
      },
    },
  },
});

interface ThemeProviderProps {
  children: React.ReactNode;
}

export default function ThemeProvider({ children }: ThemeProviderProps) {
  return <ThemeProviderMain theme={theme}>{children}</ThemeProviderMain>;
}
