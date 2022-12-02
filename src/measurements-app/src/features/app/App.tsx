import React from 'react';
import SensorAppBar from './SensorAppBar';
import { CreateMyTheme } from './themes';
import { Slide, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Navigate, Route, Routes } from 'react-router-dom';
import PrivateRoute from '../../components/auth/PrivateRoute';
import { Sensors } from '../sensor';
import { Settings } from '../settings';
import { CssBaseline } from '@mui/material';
import { ThemeProvider } from '@mui/material/styles';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { useApplicationStore } from '../../stores/applicationStore';

const queryClient = new QueryClient();

function App() {
  const theme = useApplicationStore((state) => state.theme);

  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={CreateMyTheme(theme)}>
        <CssBaseline />
        <SensorAppBar />
        <Routes>
          <Route path="/" element={<Sensors />} />
          <Route
            path="/settings"
            element={
              <PrivateRoute>
                <Settings />
              </PrivateRoute>
            }
          />
          <Route path="*" element={<Navigate to="/" />} />
        </Routes>

        <ToastContainer
          position="bottom-right"
          autoClose={3000}
          transition={Slide}
          hideProgressBar
          newestOnTop
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
      </ThemeProvider>
    </QueryClientProvider>
  );
}

export default App;
