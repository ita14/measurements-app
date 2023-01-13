import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import UserMenu from './UserMenu';
import { AppBar, IconButton, Link, Toolbar, Typography } from '@mui/material';
import { InvertColors } from '@mui/icons-material';
import { useApplicationStore } from '../../stores/applicationStore';
import { useKeycloak } from '@react-keycloak/web';

function SensorAppBar() {
  const { theme, setTheme } = useApplicationStore();
  const { keycloak } = useKeycloak();

  const handleToggleTheme = () => {
    setTheme(theme === 'light' ? 'dark' : 'light');
  };

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography sx={{ mr: 2 }}>
          <Link to="/" color="inherit" component={RouterLink}>
            Home
          </Link>
        </Typography>

        {keycloak.authenticated && (
          <Link to="/settings" color="inherit" component={RouterLink}>
            Sensors
          </Link>
        )}

        <IconButton sx={{ marginLeft: 'auto' }} onClick={handleToggleTheme}>
          <InvertColors sx={{ color: 'white' }} />
        </IconButton>

        <UserMenu />
      </Toolbar>
    </AppBar>
  );
}

export default SensorAppBar;
