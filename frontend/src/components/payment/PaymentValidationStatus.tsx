import React from 'react';
import {
  Box,
  Typography,
  CircularProgress,
  Alert
} from '@mui/material';

interface PaymentValidationStatusProps {
  isValidating: boolean;
  validationSuccess: boolean;
}

const PaymentValidationStatus: React.FC<PaymentValidationStatusProps> = ({
  isValidating,
  validationSuccess
}) => {
  if (validationSuccess) {
    return (
      <Alert severity="success" sx={{ mb: 3 }}>
        <strong>Payment validation successful!</strong> Your card information has been verified.
      </Alert>
    );
  }

  if (isValidating) {
    return (
      <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'center', p: 3 }}>
        <CircularProgress size={24} sx={{ mr: 2 }} />
        <Typography variant="body2" color="text.secondary">
          Validating payment information...
        </Typography>
      </Box>
    );
  }

  return null;
};

export default PaymentValidationStatus;