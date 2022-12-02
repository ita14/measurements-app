import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import UserMenu from './UserMenu';
import { AppBar, IconButton, Link, Toolbar, Typography } from '@mui/material';
import { InvertColors } from '@mui/icons-material';
import { useApplicationStore } from '../../stores/applicationStore';

function SensorAppBar() {
  const { theme, setTheme } = useApplicationStore();

  const handleToggleTheme = () => {
    setTheme(theme === 'light' ? 'dark' : 'light');
  };

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography>
          <Link to="/" color="inherit" component={RouterLink}>
            Home
          </Link>
        </Typography>

        <IconButton sx={{ marginLeft: 'auto' }} onClick={handleToggleTheme}>
          <InvertColors sx={{ color: 'white' }} />
        </IconButton>

        <UserMenu />
      </Toolbar>
    </AppBar>
  );
}

export default SensorAppBar;
