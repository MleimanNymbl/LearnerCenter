import React from 'react';
import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Box,
} from '@mui/material';
import { School, ExitToApp } from '@mui/icons-material';
import { useNavigate, useLocation } from 'react-router-dom';

const SimpleHeader: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  
  // Don't show header on the main enrollment page
  if (location.pathname === '/') {
    return null;
  }

  const handleLogout = () => {
    // TODO: Implement actual logout logic
    navigate('/');
  };

  const isLoggedIn = location.pathname === '/dashboard';

  return (
    <AppBar position="static">
      <Toolbar>
        <School sx={{ mr: 2 }} />
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          Learner Center
        </Typography>

        {isLoggedIn ? (
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
            <Typography variant="body2">
              Welcome, Student!
            </Typography>
            <Button
              color="inherit"
              startIcon={<ExitToApp />}
              onClick={handleLogout}
            >
              Sign Out
            </Button>
          </Box>
        ) : (
          <Button color="inherit" onClick={() => navigate('/')}>
            Home
          </Button>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default SimpleHeader;