import React from 'react';
import { Container, CircularProgress } from '@mui/material';

interface LoadingSpinnerProps {
  size?: number;
}

const LoadingSpinner: React.FC<LoadingSpinnerProps> = ({ size = 40 }) => {
  return (
    <Container maxWidth="md" sx={{ mt: 4, display: 'flex', justifyContent: 'center' }}>
      <CircularProgress size={size} />
    </Container>
  );
};

export default LoadingSpinner;