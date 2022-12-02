import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Button, Menu, MenuItem } from '@mui/material';

function UserMenu() {
  const [anchorEl, setAnchorEl] = useState<Element | null>(null);

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogin = () => {};

  const handleLogout = () => {};

  const user = {};

  return (
    <>
      {user ? (
        <>
          <Button color="inherit" onClick={handleClick}>
            Username
          </Button>

          <Menu
            anchorEl={anchorEl}
            keepMounted={true}
            open={Boolean(anchorEl)}
            onClose={handleClose}
          >
            <MenuItem to="/settings" component={Link}>
              Settings
            </MenuItem>
            <MenuItem onClick={handleLogout}>Logout</MenuItem>
          </Menu>
        </>
      ) : (
        <Button color="inherit" onClick={handleLogin}>
          Admin Login
        </Button>
      )}
    </>
  );
}

export default UserMenu;
