import React from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Button,
  Divider,
} from '@mui/material';
import { Payment } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';

interface MakePaymentCardProps {
  cost: number;
  loading: boolean;
}

const MakePaymentCard: React.FC<MakePaymentCardProps> = ({ cost, loading }) => {
  const navigate = useNavigate();

  const monthlyPayment = 416.67;
  
  const getDueDate = () => {
    const now = new Date();
    const lastDay = new Date(now.getFullYear(), now.getMonth() + 1, 0);
    return lastDay.toLocaleDateString('en-US', { month: 'long', day: 'numeric', year: 'numeric' });
  };

  return (
    <Card sx={{ flex: 1 }}>
      <CardContent>
        <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 2 }}>
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <Payment color="primary" sx={{ mr: 1 }} />
            <Typography variant="h6">Make a Payment</Typography>
          </Box>
          <Box sx={{ textAlign: 'right' }}>
            <Typography variant="caption" color="textSecondary" display="block">
              DUE DATE
            </Typography>
            <Typography variant="body2" sx={{ fontWeight: 600, color: '#d32f2f' }}>
              {new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0).toLocaleDateString('en-US', { month: 'short', day: 'numeric' })}
            </Typography>
          </Box>
        </Box>
        <Divider sx={{ mb: 2 }} />
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 0.5 }}>
          <Box>
            <Typography variant="caption" color="textSecondary" display="block">
              TOTAL PROGRAM COST
            </Typography>
            <Typography variant="h5" sx={{ fontWeight: 600, color: '#1976d2' }}>
              ${loading ? '...' : cost.toFixed(2)}
            </Typography>
          </Box>
          <Box>
            <Typography variant="caption" color="textSecondary" display="block">
              MONTHLY PAYMENT (6 MONTHS)
            </Typography>
            <Typography variant="h4" sx={{ fontWeight: 700, color: '#2e7d32' }}>
              ${loading ? '...' : (cost / 6).toFixed(2)}
            </Typography>
          </Box>
          <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
            <Button
              variant="contained"
              color="primary"
              size="large"
              onClick={() => navigate('/payment')}
            >
              Make Payment
            </Button>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};

export default MakePaymentCard;
