import React from 'react';
import {
  TextField,
  InputAdornment,
  CircularProgress
} from '@mui/material';
import {
  CheckCircle,
  Error
} from '@mui/icons-material';

interface ValidatedTextFieldProps {
  label: string;
  value: string;
  onChange: (value: string) => void;
  error?: string;
  helperText?: string;
  type?: string;
  required?: boolean;
  fullWidth?: boolean;
  startIcon?: React.ReactNode;
  isChecking?: boolean;
  isAvailable?: boolean;
  showAvailabilityIcon?: boolean;
  [key: string]: any; // For other TextField props
}

const ValidatedTextField: React.FC<ValidatedTextFieldProps> = ({
  label,
  value,
  onChange,
  error,
  helperText,
  type = 'text',
  required = false,
  fullWidth = true,
  startIcon,
  isChecking = false,
  isAvailable = false,
  showAvailabilityIcon = false,
  ...otherProps
}) => {
  const getEndAdornment = () => {
    if (!showAvailabilityIcon) return null;

    return (
      <InputAdornment position="end">
        {isChecking ? (
          <CircularProgress size={20} />
        ) : error ? (
          <Error color="error" />
        ) : isAvailable ? (
          <CheckCircle color="success" />
        ) : null}
      </InputAdornment>
    );
  };

  return (
    <TextField
      fullWidth={fullWidth}
      label={label}
      type={type}
      value={value}
      onChange={(e) => onChange(e.target.value)}
      error={!!error}
      helperText={error || helperText}
      required={required}
      InputProps={{
        startAdornment: startIcon ? (
          <InputAdornment position="start">
            {startIcon}
          </InputAdornment>
        ) : null,
        endAdornment: getEndAdornment(),
      }}
      {...otherProps}
    />
  );
};

export default ValidatedTextField;