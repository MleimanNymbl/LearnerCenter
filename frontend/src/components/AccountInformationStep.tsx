import React, { useState } from 'react';
import { Box, IconButton, InputAdornment } from '@mui/material';
import {
  Person,
  Email,
  Lock,
  Visibility,
  VisibilityOff
} from '@mui/icons-material';
import { UserRegistrationData } from '../types/user';
import ValidatedTextField from './ValidatedTextField';

interface AccountInformationStepProps {
  formData: UserRegistrationData;
  formErrors: Record<string, string>;
  checkingAvailability: Record<string, boolean>;
  availabilityResults: Record<string, { available: boolean; checked: boolean }>;
  onInputChange: (field: keyof UserRegistrationData, value: string) => void;
}

const AccountInformationStep: React.FC<AccountInformationStepProps> = ({
  formData,
  formErrors,
  checkingAvailability,
  availabilityResults,
  onInputChange
}) => {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
      <Box sx={{ display: 'flex', gap: 2, flexDirection: { xs: 'column', sm: 'row' } }}>
        <ValidatedTextField
          label="Username"
          value={formData.username}
          onChange={(value) => onInputChange('username', value)}
          error={formErrors.username}
          helperText={
            checkingAvailability.username ? 'Checking availability...' :
            (availabilityResults.username?.checked && availabilityResults.username?.available && !formErrors.username ? 'Username available' : '')
          }
          startIcon={<Person />}
          isChecking={checkingAvailability.username}
          isAvailable={availabilityResults.username?.checked && availabilityResults.username?.available}
          showAvailabilityIcon={formData.username.length >= 3}
          required
        />

        <ValidatedTextField
          label="Email Address"
          type="email"
          value={formData.email}
          onChange={(value) => onInputChange('email', value)}
          error={formErrors.email}
          helperText={
            checkingAvailability.email ? 'Checking availability...' :
            (availabilityResults.email?.checked && availabilityResults.email?.available && !formErrors.email ? 'Email available' : '')
          }
          startIcon={<Email />}
          isChecking={checkingAvailability.email}
          isAvailable={availabilityResults.email?.checked && availabilityResults.email?.available}
          showAvailabilityIcon={formData.email.includes('@')}
          required
        />
      </Box>

      <Box sx={{ display: 'flex', gap: 2, flexDirection: { xs: 'column', sm: 'row' } }}>
        <ValidatedTextField
          label="Password"
          type={showPassword ? 'text' : 'password'}
          value={formData.password}
          onChange={(value) => onInputChange('password', value)}
          error={formErrors.password}
          helperText="Minimum 8 characters"
          startIcon={<Lock />}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <Lock />
              </InputAdornment>
            ),
            endAdornment: (
              <InputAdornment position="end">
                <IconButton
                  onClick={() => setShowPassword(!showPassword)}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            ),
          }}
          required
        />

        <ValidatedTextField
          label="Confirm Password"
          type={showConfirmPassword ? 'text' : 'password'}
          value={formData.confirmPassword}
          onChange={(value) => onInputChange('confirmPassword', value)}
          error={formErrors.confirmPassword}
          helperText="Must match password"
          startIcon={<Lock />}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <Lock />
              </InputAdornment>
            ),
            endAdornment: (
              <InputAdornment position="end">
                <IconButton
                  onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                  edge="end"
                >
                  {showConfirmPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            ),
          }}
          required
        />
      </Box>
    </Box>
  );
};

export default AccountInformationStep;