import React from 'react';
import { CircularProgress, Box } from '@mui/material';

function Spinner() {
  return (
    <Box sx={{ display: 'flex', justifyContent: 'center', padding: 2 }}>
      <CircularProgress />
    </Box>
  );
}

export default Spinner;
