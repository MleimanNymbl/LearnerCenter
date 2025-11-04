import React, { useState, useEffect } from 'react';
import {
  Box,
  TextField,
  Typography,
  Card,
  CardContent,
  Checkbox,
  FormControlLabel
} from '@mui/material';
import { PaymentData, UserProfile } from '../../types/user';

interface BillingAddressSectionProps {
  paymentData: PaymentData;
  paymentErrors: Record<string, string>;
  onInputChange: (field: keyof PaymentData, value: string) => void;
  isValidating: boolean;
  userProfile?: UserProfile;
}

const BillingAddressSection: React.FC<BillingAddressSectionProps> = ({
  paymentData,
  paymentErrors,
  onInputChange,
  isValidating,
  userProfile
}) => {
  const [isSameAsContact, setIsSameAsContact] = useState(false);
  
  // Check if billing address matches contact address
  useEffect(() => {
    if (userProfile && userProfile.address) {
      const matches = 
        paymentData.billingAddress === (userProfile.address || '') &&
        paymentData.billingCity === (userProfile.city || '') &&
        paymentData.billingState === (userProfile.state || '') &&
        paymentData.billingZipCode === (userProfile.zipCode || '');
      
      setIsSameAsContact(matches);
    }
  }, [paymentData.billingAddress, paymentData.billingCity, paymentData.billingState, paymentData.billingZipCode, userProfile]);
  
  const handleSameAsContact = (checked: boolean) => {
    setIsSameAsContact(checked);
    
    if (checked && userProfile) {
      onInputChange('billingAddress', userProfile.address || '');
      onInputChange('billingCity', userProfile.city || '');
      onInputChange('billingState', userProfile.state || '');
      onInputChange('billingZipCode', userProfile.zipCode || '');
    } else if (!checked) {
      // Clear fields when unchecked
      onInputChange('billingAddress', '');
      onInputChange('billingCity', '');
      onInputChange('billingState', '');
      onInputChange('billingZipCode', '');
    }
  };
  return (
    <Card variant="outlined" sx={{ mb: 3 }}>
      <CardContent>
        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
          <Typography variant="subtitle1">
            Billing Address
          </Typography>
          
          {userProfile && userProfile.address && (
            <FormControlLabel
              control={
                <Checkbox
                  size="small"
                  checked={isSameAsContact}
                  onChange={(e) => handleSameAsContact(e.target.checked)}
                  disabled={isValidating}
                />
              }
              label="Same as contact address"
              sx={{ 
                fontSize: '0.875rem',
                '& .MuiFormControlLabel-label': { fontSize: '0.875rem' }
              }}
            />
          )}
        </Box>

        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            fullWidth
            label="Address"
            value={paymentData.billingAddress}
            onChange={(e) => onInputChange('billingAddress', e.target.value)}
            error={!!paymentErrors.billingAddress}
            helperText={paymentErrors.billingAddress}
            placeholder="123 Main Street"
            disabled={isValidating}
          />

          <Box sx={{ display: 'flex', gap: 2 }}>
            <TextField
              fullWidth
              label="City"
              value={paymentData.billingCity}
              onChange={(e) => onInputChange('billingCity', e.target.value)}
              error={!!paymentErrors.billingCity}
              helperText={paymentErrors.billingCity}
              placeholder="New York"
              disabled={isValidating}
            />

            <TextField
              label="State"
              value={paymentData.billingState}
              onChange={(e) => onInputChange('billingState', e.target.value)}
              error={!!paymentErrors.billingState}
              helperText={paymentErrors.billingState}
              placeholder="NY"
              disabled={isValidating}
              sx={{ minWidth: 120 }}
            />

            <TextField
              label="ZIP Code"
              value={paymentData.billingZipCode}
              onChange={(e) => onInputChange('billingZipCode', e.target.value)}
              error={!!paymentErrors.billingZipCode}
              helperText={paymentErrors.billingZipCode}
              placeholder="10001"
              disabled={isValidating}
              sx={{ minWidth: 120 }}
            />
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};

export default BillingAddressSection;