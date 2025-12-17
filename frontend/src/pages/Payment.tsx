import React, { useState } from 'react';
import {
  Container,
  Typography,
  Box,
  Card,
  CardContent,
  Button,
  TextField,
  Divider,
  Alert,
  CircularProgress,
  Radio,
  RadioGroup,
  FormControlLabel,
  FormControl,
} from '@mui/material';
import { CreditCard, CheckCircle, ArrowBack } from '@mui/icons-material';
import { Visa, Mastercard, Amex } from 'react-pay-icons';
import { useNavigate } from 'react-router-dom';
import { useEnrollment } from '../hooks/useEnrollment';
import { useAuth } from '../contexts/AuthContext';
import { api } from '../services/api';

interface SavedCard {
  id: string;
  cardNumber: string;
  cardName: string;
  expiryDate: string;
  cardType: string;
}

interface PaymentResponse {
  success: boolean;
  message: string;
}

const savedCards: SavedCard[] = [
  { id: '1', cardNumber: '4532 1234 5678 9010', cardName: 'John Smith', expiryDate: '12/25', cardType: 'Visa' },
  { id: '2', cardNumber: '5425 2334 3010 9903', cardName: 'John Smith', expiryDate: '08/26', cardType: 'Mastercard' },
  { id: '3', cardNumber: '3782 822463 10005', cardName: 'John Smith', expiryDate: '03/27', cardType: 'Amex' },
];

const Payment: React.FC = () => {
  const navigate = useNavigate();
  const { user, refreshUser } = useAuth();
  const enrollment = useEnrollment();
  const [selectedCard, setSelectedCard] = useState<string>('');
  const [useNewCard, setUseNewCard] = useState(false);
  const [cardNumber, setCardNumber] = useState('');
  const [cardName, setCardName] = useState('');
  const [expiryDate, setExpiryDate] = useState('');
  const [cvv, setCvv] = useState('');
  const [processing, setProcessing] = useState(false);
  const [paymentSuccess, setPaymentSuccess] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const monthlyPayment = enrollment?.cost ? (enrollment.cost / 6).toFixed(2) : '0.00';
  const amount = monthlyPayment;

  const handleCardNumberChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value.replace(/\s/g, '');
    if (value.length <= 16 && /^\d*$/.test(value)) {
      // Format with spaces every 4 digits
      const formatted = value.match(/.{1,4}/g)?.join(' ') || value;
      setCardNumber(formatted);
    }
  };

  const handleExpiryChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value.replace(/\D/g, '');
    if (value.length <= 4) {
      // Format as MM/YY
      const formatted = value.length >= 2 ? `${value.slice(0, 2)}/${value.slice(2)}` : value;
      setExpiryDate(formatted);
    }
  };

  const handleCvvChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    if (value.length <= 3 && /^\d*$/.test(value)) {
      setCvv(value);
    }
  };

  const getCardIcon = (cardType: string) => {
    const size = 28;
    switch (cardType.toLowerCase()) {
      case 'visa':
        return <Visa style={{ width: size, height: 'auto' }} />;
      case 'mastercard':
        return <Mastercard style={{ width: size, height: 'auto' }} />;
      case 'amex':
        return <Amex style={{ width: size, height: 'auto' }} />;
      default:
        return <CreditCard sx={{ fontSize: 24, color: '#666' }} />;
    }
  };

  const handleCardSelection = (cardId: string) => {
    if (cardId === 'new') {
      setUseNewCard(true);
      setSelectedCard('');
      setCardNumber('');
      setCardName('');
      setExpiryDate('');
      setCvv('');
    } else {
      setUseNewCard(false);
      setSelectedCard(cardId);
      const card = savedCards.find(c => c.id === cardId);
      if (card) {
        setCardNumber(card.cardNumber);
        setCardName(card.cardName);
        setExpiryDate(card.expiryDate);
        setCvv('123'); // Auto-fill CVV for saved cards
      }
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    // Basic validation
    if (!selectedCard && !useNewCard) {
      setError('Please select a payment method');
      return;
    }

    if (useNewCard) {
      if (cardNumber.replace(/\s/g, '').length !== 16) {
        setError('Card number must be 16 digits');
        return;
      }
      if (!cardName.trim()) {
        setError('Cardholder name is required');
        return;
      }
      if (expiryDate.length !== 5) {
        setError('Expiry date must be in MM/YY format');
        return;
      }
      if (cvv.length !== 3) {
        setError('CVV must be 3 digits');
        return;
      }
    }

    // Process payment
    setProcessing(true);
    try {
      const response = await api.post<PaymentResponse>(
        `/enrollment/${enrollment?.enrollmentId}/payment`,
        { amount: parseFloat(amount) }
      );

      if (response.data.success) {
        // Add artificial delay of 2-3 seconds
        await new Promise(resolve => setTimeout(resolve, 2500));
        setPaymentSuccess(true);
      } else {
        setError('Payment failed. Please try again.');
      }
    } catch (err: any) {
      console.error('Payment error:', err);
      setError(err.response?.data?.message || 'An error occurred while processing the payment.');
    } finally {
      setProcessing(false);
    }
  };

  if (paymentSuccess) {
    return (
      <Container maxWidth="md" sx={{ mt: 8, mb: 4 }}>
        <Card sx={{ textAlign: 'center', py: 6 }}>
          <CardContent>
            <CheckCircle sx={{ fontSize: 80, color: 'success.main', mb: 2 }} />
            <Typography variant="h4" gutterBottom sx={{ fontWeight: 600 }}>
              Payment Successful!
            </Typography>
            <Typography variant="body1" color="textSecondary" sx={{ mb: 1 }}>
              Your payment of ${monthlyPayment} has been processed successfully.
            </Typography>
            <Typography variant="body2" color="textSecondary" sx={{ mb: 4 }}>
              Transaction ID: {Math.random().toString(36).substring(2, 15).toUpperCase()}
            </Typography>
            <Box sx={{ display: 'flex', gap: 2, justifyContent: 'center' }}>
              <Button
                variant="contained"
                color="primary"
                onClick={async () => {
                  await refreshUser();
                  navigate('/dashboard');
                }}
                size="large"
              >
                Return to Dashboard
              </Button>
            </Box>
          </CardContent>
        </Card>
      </Container>
    );
  }

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Button
        startIcon={<ArrowBack />}
        onClick={() => navigate('/dashboard')}
        sx={{ mb: 3 }}
      >
        Back to Dashboard
      </Button>

      <Typography variant="h4" gutterBottom sx={{ fontWeight: 600 }}>
        Make a Payment
      </Typography>
      <Typography variant="body1" color="textSecondary" sx={{ mb: 4 }}>
        Complete your monthly payment for your program enrollment.
      </Typography>

      <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: '2fr 1fr' }, gap: 3 }}>
        {/* Payment Form */}
        <Card>
          <CardContent>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <CreditCard color="primary" sx={{ mr: 1 }} />
              <Typography variant="h6">Payment Information</Typography>
            </Box>
            <Divider sx={{ mb: 3 }} />

            {error && (
              <Alert severity="error" sx={{ mb: 3 }} onClose={() => setError(null)}>
                {error}
              </Alert>
            )}

            <form onSubmit={handleSubmit}>
              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
                {/* Saved Cards Section */}
                <Box>
                  <Typography variant="subtitle1" sx={{ mb: 2, fontWeight: 600 }}>
                    Select Payment Method
                  </Typography>
                  <FormControl component="fieldset" fullWidth>
                    <RadioGroup value={selectedCard || (useNewCard ? 'new' : '')} onChange={(e) => handleCardSelection(e.target.value)}>
                      {savedCards.map((card) => (
                        <Card key={card.id} sx={{ mb: 2, border: selectedCard === card.id ? '2px solid #1976d2' : '1px solid #e0e0e0', borderRadius: 2 }}>
                          <CardContent sx={{ py: 2 }}>
                            <FormControlLabel
                              value={card.id}
                              control={<Radio />}
                              label={
                                <Box sx={{ display: 'flex', alignItems: 'center', width: '100%', ml: 1 }}>
                                  <Box sx={{ mr: 2 }}>{getCardIcon(card.cardType)}</Box>
                                  <Box sx={{ flexGrow: 1 }}>
                                    <Typography variant="body1" sx={{ fontWeight: 500 }}>
                                      {card.cardType} •••• {card.cardNumber.slice(-4)}
                                    </Typography>
                                    <Typography variant="caption" color="textSecondary">
                                      {card.cardName} | Expires {card.expiryDate}
                                    </Typography>
                                  </Box>
                                </Box>
                              }
                              sx={{ m: 0, width: '100%' }}
                            />
                          </CardContent>
                        </Card>
                      ))}
                      <Card sx={{ border: useNewCard ? '2px solid #1976d2' : '1px solid #e0e0e0', borderRadius: 2 }}>
                        <CardContent sx={{ py: 2 }}>
                          <FormControlLabel
                            value="new"
                            control={<Radio />}
                            label={
                              <Box sx={{ display: 'flex', alignItems: 'center', ml: 1 }}>
                                <CreditCard sx={{ mr: 2, color: '#666' }} />
                                <Typography variant="body1" sx={{ fontWeight: 500 }}>
                                  Use a new card
                                </Typography>
                              </Box>
                            }
                            sx={{ m: 0 }}
                          />
                        </CardContent>
                      </Card>
                    </RadioGroup>
                  </FormControl>
                </Box>

                {/* New Card Form - Only show if "Use a new card" is selected */}
                {useNewCard && (
                  <>
                    <Divider sx={{ my: 2 }} />
                    <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
                      Card Details
                    </Typography>
                    <TextField
                      label="Card Number"
                      value={cardNumber}
                      onChange={handleCardNumberChange}
                      placeholder="1234 5678 9012 3456"
                      fullWidth
                      required
                      inputProps={{ maxLength: 19 }}
                    />

                    <TextField
                      label="Cardholder Name"
                      value={cardName}
                      onChange={(e) => setCardName(e.target.value)}
                      placeholder="John Doe"
                      fullWidth
                      required
                    />

                    <Box sx={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 2 }}>
                      <TextField
                        label="Expiry Date"
                        value={expiryDate}
                        onChange={handleExpiryChange}
                        placeholder="MM/YY"
                        required
                        inputProps={{ maxLength: 5 }}
                      />

                      <TextField
                        label="CVV"
                        value={cvv}
                        onChange={handleCvvChange}
                        placeholder="123"
                        required
                        type="password"
                        inputProps={{ maxLength: 3 }}
                      />
                    </Box>
                  </>
                )}

                <Divider sx={{ my: 2 }} />
                <Box sx={{ p: 2, backgroundColor: '#f5f5f5', borderRadius: 1 }}>
                  <Typography variant="body2" color="textSecondary" gutterBottom>
                    Monthly Payment Amount
                  </Typography>
                  <Typography variant="h4" sx={{ fontWeight: 600, color: '#1976d2' }}>
                    ${monthlyPayment}
                  </Typography>
                  <Typography variant="caption" color="textSecondary">
                    Minimum monthly payment required
                  </Typography>
                </Box>

                <Button
                  type="submit"
                  variant="contained"
                  color="primary"
                  size="large"
                  fullWidth
                  disabled={processing}
                  sx={{ mt: 2 }}
                >
                  {processing ? (
                    <>
                      <CircularProgress size={24} sx={{ mr: 1 }} color="inherit" />
                      Processing...
                    </>
                  ) : (
                    `Pay $${monthlyPayment}`
                  )}
                </Button>
              </Box>
            </form>
          </CardContent>
        </Card>

        {/* Payment Summary */}
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Payment Summary
            </Typography>
            <Divider sx={{ mb: 3 }} />

            {enrollment ? (
              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                <Box>
                  <Typography variant="caption" color="textSecondary" display="block">
                    PROGRAM
                  </Typography>
                  <Typography variant="body1" sx={{ fontWeight: 500 }}>
                    {enrollment.programName}
                  </Typography>
                </Box>

                <Divider />

                <Box>
                  <Typography variant="caption" color="textSecondary" display="block">
                    TOTAL COST
                  </Typography>
                  <Typography variant="body1" sx={{ fontWeight: 500 }}>
                    ${enrollment.cost?.toFixed(2)}
                  </Typography>
                </Box>

                <Box>
                  <Typography variant="caption" color="textSecondary" display="block">
                    PAYMENT PLAN
                  </Typography>
                  <Typography variant="body1" sx={{ fontWeight: 500 }}>
                    6 Monthly Payments
                  </Typography>
                </Box>

                <Box>
                  <Typography variant="caption" color="textSecondary" display="block">
                    DUE DATE
                  </Typography>
                  <Typography variant="body1" sx={{ fontWeight: 500, color: '#d32f2f' }}>
                    {new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0).toLocaleDateString('en-US', { month: 'long', day: 'numeric', year: 'numeric' })}
                  </Typography>
                </Box>

                <Divider />

                <Box>
                  <Typography variant="caption" color="textSecondary" display="block">
                    AMOUNT DUE TODAY
                  </Typography>
                  <Typography variant="h5" sx={{ fontWeight: 700, color: '#2e7d32' }}>
                    ${monthlyPayment}
                  </Typography>
                </Box>

                <Alert severity="info" sx={{ mt: 2 }}>
                  This is a simulated payment. No actual charges will be made.
                </Alert>
              </Box>
            ) : (
              <Typography color="textSecondary">
                No enrollment information available.
              </Typography>
            )}
          </CardContent>
        </Card>
      </Box>
    </Container>
  );
};

export default Payment;
