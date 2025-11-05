import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  Alert,
  Card,
  CardContent,
  Divider
} from '@mui/material';
import { CreditCard } from '@mui/icons-material';
import { PaymentData } from '../../types/user';

import BillingAddressSection from './BillingAddressSection';
import PaymentValidationStatus from './PaymentValidationStatus';
import CardDetailsSection from './CardDetailsSection';
import { UserProfile } from '../../types/user';

interface PaymentInformationStepProps {
  paymentData: PaymentData;
  paymentErrors: Record<string, string>;
  onInputChange: (field: keyof PaymentData, value: string) => void;
  isValidating: boolean;
  validationSuccess: boolean;
  userProfile?: UserProfile;
  tuitionCost?: number;
  programName?: string;
}

const PaymentInformationStep: React.FC<PaymentInformationStepProps> = ({
  paymentData,
  paymentErrors,
  onInputChange,
  isValidating,
  validationSuccess,
  userProfile,
  tuitionCost = 0,
  programName = 'Program'
}) => {
  const [cardType, setCardType] = useState<string>('');

  // Format credit card number with spaces
  const formatCardNumber = (value: string): string => {
    const numbers = value.replace(/\D/g, '');
    
    // American Express format: 4-6-5
    if (/^3[47]/.test(numbers)) {
      if (numbers.length <= 4) return numbers;
      if (numbers.length <= 10) return `${numbers.slice(0, 4)} ${numbers.slice(4)}`;
      return `${numbers.slice(0, 4)} ${numbers.slice(4, 10)} ${numbers.slice(10, 15)}`;
    }
    
    // Other cards format: 4-4-4-4
    const formatted = numbers.replace(/(\d{4})(?=\d)/g, '$1 ');
    return formatted.substring(0, 19); // Max 16 digits + 3 spaces
  };

  // Detect card type based on number
  const detectCardType = (cardNumber: string): string => {
    const number = cardNumber.replace(/\s/g, '');
    
    if (/^4/.test(number)) return 'Visa';
    if (/^5[1-5]/.test(number)) return 'Mastercard';
    if (/^3[47]/.test(number)) return 'American Express';
    if (/^6011/.test(number)) return 'Discover';
    
    return '';
  };

  // Update card type when card number changes
  useEffect(() => {
    setCardType(detectCardType(paymentData.cardNumber));
  }, [paymentData.cardNumber]);

  const handleCardNumberChange = (value: string) => {
    const formatted = formatCardNumber(value);
    onInputChange('cardNumber', formatted);
  };

  const handleCvvChange = (value: string) => {
    const numbers = value.replace(/\D/g, '');
    const maxLength = cardType === 'American Express' ? 4 : 3;
    onInputChange('cvv', numbers.slice(0, maxLength));
  };

  // Enhanced input change handler that formats card numbers
  const handleInputChange = (field: keyof PaymentData, value: string) => {
    if (field === 'cardNumber') {
      handleCardNumberChange(value);
    } else if (field === 'cvv') {
      handleCvvChange(value);
    } else {
      onInputChange(field, value);
    }
  };

  return (
    <Box>
      <Typography variant="h6" gutterBottom sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
        <CreditCard color="primary" />
        Payment Information
      </Typography>
      
      <Box sx={{ mb: 3 }}>
        <Alert severity="info" sx={{ mb: 2 }}>
          <strong>Demo Payment Form</strong> - No actual charges will be made.
        </Alert>
        <Typography variant="body2" color="text.secondary">
          Test with these sample cards: <br />
          • Visa: <strong>4532 1234 5678 9012</strong> <br />
          • Mastercard: <strong>5555 5555 5555 4444</strong> <br />
          • American Express: <strong>3782 8224 6310 005</strong> <br />
          Use any future expiry date and any 3-4 digit CVV.
        </Typography>
      </Box>

      {/* Payment Summary - Due Today */}
      {tuitionCost >= 0 && (
        <Card 
          elevation={2} 
          sx={{ 
            mb: 3, 
            bgcolor: 'success.50',
            border: '2px solid',
            borderColor: 'success.main',
            borderRadius: 2
          }}
        >
          <CardContent sx={{ textAlign: 'center' }}>
            <Typography variant="h5" gutterBottom sx={{ color: 'success.dark', fontWeight: 'bold' }}>
              💳 Due Today
            </Typography>
            
            <Typography variant="h3" sx={{ color: 'success.main', fontWeight: 'bold', mb: 1 }}>
              ${(tuitionCost * 0.05).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
            </Typography>
            
            <Typography variant="h6" sx={{ color: 'text.primary', mb: 2 }}>
              Registration Fee for {programName}
            </Typography>
            
            <Box sx={{ 
              display: 'flex', 
              justifyContent: 'space-between', 
              alignItems: 'center',
              bgcolor: 'white',
              p: 2,
              borderRadius: 1,
              border: '1px solid',
              borderColor: 'grey.200'
            }}>
              <Box sx={{ textAlign: 'left' }}>
                <Typography variant="body2" color="text.secondary">
                  Total Program Cost:
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Registration Fee (5%):
                </Typography>
              </Box>
              <Box sx={{ textAlign: 'right' }}>
                <Typography variant="body2" sx={{ fontWeight: 'medium' }}>
                  ${tuitionCost.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                </Typography>
                <Typography variant="body2" sx={{ fontWeight: 'bold', color: 'success.main' }}>
                  ${(tuitionCost * 0.05).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                </Typography>
              </Box>
            </Box>
            
            <Typography variant="caption" color="text.secondary" sx={{ mt: 2, display: 'block' }}>
              This secures your enrollment. Remaining balance will be covered by your payment plan.
            </Typography>
          </CardContent>
        </Card>
      )}

      <PaymentValidationStatus 
        isValidating={isValidating} 
        validationSuccess={validationSuccess} 
      />

      <CardDetailsSection
        paymentData={paymentData}
        paymentErrors={paymentErrors}
        onInputChange={handleInputChange}
        isValidating={isValidating}
        cardType={cardType}
      />

      <BillingAddressSection
        paymentData={paymentData}
        paymentErrors={paymentErrors}
        onInputChange={onInputChange}
        isValidating={isValidating}
        userProfile={userProfile}
      />
    </Box>
  );
};

export default PaymentInformationStep;