import React from 'react';
import { Box, FormControl, InputLabel, Select, MenuItem, Typography, FormHelperText } from '@mui/material';
import { Phone, Home, ContactPhone } from '@mui/icons-material';
import { UserRegistrationData } from '../types/user';
import ValidatedTextField from './ValidatedTextField';
import { formatPhoneNumber } from '../utils/validation';

interface ContactDetailsStepProps {
  formData: UserRegistrationData;
  formErrors: Record<string, string>;
  onInputChange: (field: keyof UserRegistrationData, value: string) => void;
}

const ContactDetailsStep: React.FC<ContactDetailsStepProps> = ({
  formData,
  formErrors,
  onInputChange
}) => {
  const handlePhoneChange = (field: 'phoneNumber' | 'emergencyContactPhone', value: string) => {
    const formatted = formatPhoneNumber(value);
    onInputChange(field, formatted);
  };

  const states = [
    'AL', 'AK', 'AZ', 'AR', 'CA', 'CO', 'CT', 'DE', 'FL', 'GA',
    'HI', 'ID', 'IL', 'IN', 'IA', 'KS', 'KY', 'LA', 'ME', 'MD',
    'MA', 'MI', 'MN', 'MS', 'MO', 'MT', 'NE', 'NV', 'NH', 'NJ',
    'NM', 'NY', 'NC', 'ND', 'OH', 'OK', 'OR', 'PA', 'RI', 'SC',
    'SD', 'TN', 'TX', 'UT', 'VT', 'VA', 'WA', 'WV', 'WI', 'WY'
  ];

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
      <Box sx={{ mb: 1 }}>
        <Typography variant="h6" gutterBottom color="primary">
          Contact Information
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
          All contact information fields are optional, but providing them helps us serve you better.
        </Typography>
      </Box>
      
      <ValidatedTextField
        label="Phone Number"
        value={formData.phoneNumber || ''}
        onChange={(value) => handlePhoneChange('phoneNumber', value)}
        error={formErrors.phoneNumber}
        helperText={formErrors.phoneNumber || "Format: (xxx) xxx-xxxx"}
        startIcon={<Phone />}
      />

      <ValidatedTextField
        label="Address"
        value={formData.address || ''}
        onChange={(value) => onInputChange('address', value)}
        helperText={formErrors.address}
        startIcon={<Home />}
        placeholder="123 Main Street, Apt 4B"
      />

      <Box sx={{ display: 'flex', gap: 2, flexDirection: { xs: 'column', sm: 'row' } }}>
        <ValidatedTextField
          label="City"
          value={formData.city || ''}
          onChange={(value) => onInputChange('city', value)}
          error={formErrors.city}
          placeholder="Chicago"
        />

        <FormControl fullWidth error={!!formErrors.state}>
          <InputLabel>State</InputLabel>
          <Select
            value={formData.state || ''}
            label="State"
            onChange={(e) => onInputChange('state', e.target.value)}
          >
            <MenuItem value="">
              <em>Select a state</em>
            </MenuItem>
            {states.map((state) => (
              <MenuItem key={state} value={state}>
                {state}
              </MenuItem>
            ))}
          </Select>
          {formErrors.state && (
            <FormHelperText>{formErrors.state}</FormHelperText>
          )}
        </FormControl>

        <ValidatedTextField
          label="ZIP Code"
          value={formData.zipCode || ''}
          onChange={(value) => onInputChange('zipCode', value)}
          error={formErrors.zipCode}
          helperText={formErrors.zipCode || "Format: 12345 or 12345-6789"}
          placeholder="60601"
        />
      </Box>

      <Box sx={{ mt: 3 }}>
        <Typography variant="subtitle1" gutterBottom>
          Emergency Contact
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
          Someone we can contact in case of emergency. If you provide a name, please also provide a phone number.
        </Typography>
        
        <Box sx={{ display: 'flex', gap: 2, flexDirection: { xs: 'column', sm: 'row' } }}>
          <ValidatedTextField
            label="Emergency Contact Name"
            value={formData.emergencyContactName || ''}
            onChange={(value) => onInputChange('emergencyContactName', value)}
            error={formErrors.emergencyContactName}
            startIcon={<ContactPhone />}
            placeholder="John Smith"
          />

          <ValidatedTextField
            label="Emergency Contact Phone"
            value={formData.emergencyContactPhone || ''}
            onChange={(value) => handlePhoneChange('emergencyContactPhone', value)}
            error={formErrors.emergencyContactPhone}
            helperText={formErrors.emergencyContactPhone || "Format: (xxx) xxx-xxxx"}
            startIcon={<Phone />}
          />
        </Box>
      </Box>
    </Box>
  );
};

export default ContactDetailsStep;