import { UserRegistrationData } from '../types/user';

export const formatPhoneNumber = (value: string): string => {
  const numbers = value.replace(/\D/g, '');
  if (numbers.length >= 6) {
    return `(${numbers.slice(0, 3)}) ${numbers.slice(3, 6)}-${numbers.slice(6, 10)}`;
  } else if (numbers.length >= 3) {
    return `(${numbers.slice(0, 3)}) ${numbers.slice(3)}`;
  } else {
    return numbers;
  }
};

export const validateStep = (
  step: number,
  formData: UserRegistrationData,
  availabilityResults: Record<string, { available: boolean; checked: boolean }>
): Record<string, string> => {
  const errors: Record<string, string> = {};

  if (step === 0) {
    // Account Information validation
    if (!formData.username.trim()) {
      errors.username = 'Username is required';
    } else if (formData.username.length < 3) {
      errors.username = 'Username must be at least 3 characters';
    } else if (availabilityResults.username?.checked && !availabilityResults.username?.available) {
      errors.username = 'Username is already taken';
    }

    if (!formData.email.trim()) {
      errors.email = 'Email is required';
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      errors.email = 'Please enter a valid email address';
    } else if (availabilityResults.email?.checked && !availabilityResults.email?.available) {
      errors.email = 'Email is already registered';
    }

    if (!formData.password) {
      errors.password = 'Password is required';
    } else if (formData.password.length < 8) {
      errors.password = 'Password must be at least 8 characters';
    }

    if (!formData.confirmPassword) {
      errors.confirmPassword = 'Please confirm your password';
    } else if (formData.password !== formData.confirmPassword) {
      errors.confirmPassword = 'Passwords do not match';
    }
  } else if (step === 1) {
    // Personal Information validation
    if (!formData.firstName.trim()) {
      errors.firstName = 'First name is required';
    } else if (formData.firstName.length > 50) {
      errors.firstName = 'First name is too long (max 50 characters)';
    } else if (!/^[a-zA-Z\s'-]+$/.test(formData.firstName.trim())) {
      errors.firstName = 'First name can only contain letters, spaces, apostrophes, and hyphens';
    }

    if (!formData.lastName.trim()) {
      errors.lastName = 'Last name is required';
    } else if (formData.lastName.length > 50) {
      errors.lastName = 'Last name is too long (max 50 characters)';
    } else if (!/^[a-zA-Z\s'-]+$/.test(formData.lastName.trim())) {
      errors.lastName = 'Last name can only contain letters, spaces, apostrophes, and hyphens';
    }

    if (formData.dateOfBirth) {
      const birthDate = new Date(formData.dateOfBirth);
      const today = new Date();
      const age = today.getFullYear() - birthDate.getFullYear();
      const monthDiff = today.getMonth() - birthDate.getMonth();
      
      if (birthDate >= today) {
        errors.dateOfBirth = 'Birth date cannot be in the future';
      } else if (age < 13 || (age === 13 && monthDiff < 0)) {
        errors.dateOfBirth = 'You must be at least 13 years old to register';
      } else if (age > 120) {
        errors.dateOfBirth = 'Please enter a valid birth date';
      }
    }
  } else if (step === 2) {
    // Contact Details validation
    if (formData.phoneNumber) {
      if (formData.phoneNumber.length > 20) {
        errors.phoneNumber = 'Phone number is too long (max 20 characters)';
      } else if (!/^\(\d{3}\)\s\d{3}-\d{4}$/.test(formData.phoneNumber)) {
        errors.phoneNumber = 'Please enter a valid phone number (xxx) xxx-xxxx';
      }
    }

    if (formData.address && formData.address.length > 100) {
      errors.address = 'Address is too long (max 100 characters)';
    }

    if (formData.city && formData.city.length > 50) {
      errors.city = 'City name is too long (max 50 characters)';
    }

    if (formData.state) {
      if (formData.state.length > 50) {
        errors.state = 'State is too long (max 50 characters)';
      } else if (formData.state.length > 0 && formData.state.length < 2) {
        errors.state = 'Please enter a valid state (at least 2 characters)';
      }
    }

    if (formData.zipCode) {
      if (formData.zipCode.length > 10) {
        errors.zipCode = 'ZIP code is too long (max 10 characters)';
      } else if (!/^\d{5}(-\d{4})?$/.test(formData.zipCode)) {
        errors.zipCode = 'Please enter a valid ZIP code (12345 or 12345-6789)';
      }
    }

    if (formData.emergencyContactName && formData.emergencyContactName.length > 100) {
      errors.emergencyContactName = 'Emergency contact name is too long (max 100 characters)';
    }

    if (formData.emergencyContactPhone) {
      if (formData.emergencyContactPhone.length > 20) {
        errors.emergencyContactPhone = 'Emergency contact phone is too long (max 20 characters)';
      } else if (!/^\(\d{3}\)\s\d{3}-\d{4}$/.test(formData.emergencyContactPhone)) {
        errors.emergencyContactPhone = 'Please enter a valid emergency contact phone number (xxx) xxx-xxxx';
      }
    }

    // Conditional validation: if emergency contact name is provided, phone should be provided too
    if (formData.emergencyContactName && !formData.emergencyContactPhone) {
      errors.emergencyContactPhone = 'Please provide emergency contact phone number';
    }

    if (formData.emergencyContactPhone && !formData.emergencyContactName) {
      errors.emergencyContactName = 'Please provide emergency contact name';
    }
  }

  return errors;
};