import React from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import {
  Container,
  Card,
  CardContent,
  Typography,
  Button,
  Box,
  Alert
} from '@mui/material';
import { CheckCircle, Login as LoginIcon } from '@mui/icons-material';

interface LocationState {
  username: string;
  programName: string;
}

const RegistrationSuccess: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { username, programName } = location.state as LocationState || {};

  const handleGoToLogin = () => {
    navigate('/login');
  };

  const handleGoHome = () => {
    navigate('/');
  };

  return (
    <Container maxWidth="md" sx={{ py: 4 }}>
      <Card elevation={3}>
        <CardContent sx={{ p: 4, textAlign: 'center' }}>
          <Box sx={{ mb: 3 }}>
            <CheckCircle 
              sx={{ 
                fontSize: 80, 
                color: 'success.main',
                mb: 2 
              }} 
            />
            
            <Typography variant="h4" gutterBottom color="success.main">
              Registration Successful!
            </Typography>
            
            <Typography variant="h6" gutterBottom>
              Welcome, {username}!
            </Typography>
          </Box>

          <Alert severity="success" sx={{ mb: 3 }}>
            <Typography variant="body1">
              Your account has been created successfully for the <strong>{programName}</strong> program.
            </Typography>
          </Alert>

          <Typography variant="body1" color="text.secondary" sx={{ mb: 4 }}>
            You can now log in to access your learner dashboard and begin your educational journey.
          </Typography>

          <Box sx={{ display: 'flex', gap: 2, justifyContent: 'center', flexWrap: 'wrap' }}>
            <Button
              variant="contained"
              startIcon={<LoginIcon />}
              onClick={handleGoToLogin}
              size="large"
              sx={{ px: 4 }}
            >
              Go to Login
            </Button>
            
            <Button
              variant="outlined"
              onClick={handleGoHome}
              size="large"
              sx={{ px: 4 }}
            >
              Back to Home
            </Button>
          </Box>
        </CardContent>
      </Card>
    </Container>
  );
};

export default RegistrationSuccess;