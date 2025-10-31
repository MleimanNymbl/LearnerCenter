import React from 'react';
import { Box, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import { Person, Cake } from '@mui/icons-material';
import { UserRegistrationData } from '../types/user';
import ValidatedTextField from './ValidatedTextField';

interface PersonalInformationStepProps {
  formData: UserRegistrationData;
  formErrors: Record<string, string>;
  onInputChange: (field: keyof UserRegistrationData, value: string) => void;
}

const PersonalInformationStep: React.FC<PersonalInformationStepProps> = ({
  formData,
  formErrors,
  onInputChange
}) => {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
      <Box sx={{ display: 'flex', gap: 2, flexDirection: { xs: 'column', sm: 'row' } }}>
        <ValidatedTextField
          label="First Name"
          value={formData.firstName}
          onChange={(value) => onInputChange('firstName', value)}
          error={formErrors.firstName}
          startIcon={<Person />}
          required
        />

        <ValidatedTextField
          label="Last Name"
          value={formData.lastName}
          onChange={(value) => onInputChange('lastName', value)}
          error={formErrors.lastName}
          startIcon={<Person />}
          required
        />
      </Box>

      <Box sx={{ display: 'flex', gap: 2, flexDirection: { xs: 'column', sm: 'row' } }}>
        <ValidatedTextField
          label="Date of Birth"
          type="date"
          value={formData.dateOfBirth || ''}
          onChange={(value) => onInputChange('dateOfBirth', value)}
          error={formErrors.dateOfBirth}
          startIcon={<Cake />}
          InputLabelProps={{
            shrink: true,
          }}
        />

        <FormControl fullWidth>
          <InputLabel>Gender</InputLabel>
          <Select
            value={formData.gender || ''}
            label="Gender"
            onChange={(e) => onInputChange('gender', e.target.value)}
          >
            <MenuItem value="">
              <em>Prefer not to say</em>
            </MenuItem>
            <MenuItem value="Male">Male</MenuItem>
            <MenuItem value="Female">Female</MenuItem>
            <MenuItem value="Other">Other</MenuItem>
          </Select>
        </FormControl>
      </Box>
    </Box>
  );
};

export default PersonalInformationStep;