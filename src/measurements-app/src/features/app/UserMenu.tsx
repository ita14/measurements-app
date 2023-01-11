import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Button, Menu, MenuItem } from '@mui/material';
import { useKeycloak } from '@react-keycloak/web';

function UserMenu() {
  const [anchorEl, setAnchorEl] = useState<Element | null>(null);
  const { keycloak } = useKeycloak();

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogin = () => {
    keycloak?.login();
  };

  const handleLogout = () => {
    keycloak.logout();
  };

  return (
    <>
      {keycloak.authenticated ? (
        <>
          <Button color="inherit" onClick={handleClick}>
            {keycloak?.idTokenParsed?.preferred_username}
          </Button>

          <Menu
            anchorEl={anchorEl}
            open={Boolean(anchorEl)}
            onClose={handleClose}
            onClick={handleClose}
          >
            <MenuItem to="/settings" component={Link}>
              Settings
            </MenuItem>
            <MenuItem onClick={handleLogout}>Logout</MenuItem>
          </Menu>
        </>
      ) : (
        <Button color="inherit" onClick={handleLogin}>
          Login
        </Button>
      )}
    </>
  );
}

export default UserMenu;
