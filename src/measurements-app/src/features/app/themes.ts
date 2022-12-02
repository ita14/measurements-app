import { createTheme } from '@mui/material/styles';
import orange from '@mui/material/colors/orange';

export function CreateMyTheme(theme: string | undefined) {
  return createTheme({
    palette: {
      mode: theme === 'light' ? 'light' : 'dark',
      primary: orange
    }
  });
}
