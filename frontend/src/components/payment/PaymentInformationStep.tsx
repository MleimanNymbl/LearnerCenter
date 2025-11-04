import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  Alert
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
}

const PaymentInformationStep: React.FC<PaymentInformationStepProps> = ({
  paymentData,
  paymentErrors,
  onInputChange,
  isValidating,
  validationSuccess,
  userProfile
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