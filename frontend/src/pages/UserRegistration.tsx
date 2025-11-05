import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import {
  Box,
  Container,
  Card,
  CardContent,
  Typography,
  Button,
  Alert,
  LinearProgress,
  Stepper,
  Step,
  StepLabel
} from '@mui/material';
import { ArrowBack } from '@mui/icons-material';
import { UserRegistrationData, PaymentData } from '../types/user';
import { usersApi } from '../services/apiService';
import { useUserAvailability } from '../hooks/useUserAvailability';
import { validateStep } from '../utils/validation';
import HeroBanner from '../components/HeroBanner';
import AccountInformationStep from '../components/AccountInformationStep';
import PersonalInformationStep from '../components/PersonalInformationStep';
import ContactDetailsStep from '../components/ContactDetailsStep';
import { PaymentInformationStep } from '../components/payment';

interface LocationState {
  enrollmentId: string;
  programName: string;
  tuitionCost?: number;
}

const UserRegistration: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { enrollmentId, programName, tuitionCost } = location.state as LocationState || {};

  // Debug what's coming through
  useEffect(() => {
    console.log('UserRegistration received:', { enrollmentId, programName, tuitionCost });
  }, [enrollmentId, programName, tuitionCost]);

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [isValidatingPayment, setIsValidatingPayment] = useState(false);
  const [paymentValidated, setPaymentValidated] = useState(false);
  
  const [formData, setFormData] = useState<UserRegistrationData>({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
    enrollmentId: enrollmentId || '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: '',
    city: '',
    state: '',
    zipCode: '',
    dateOfBirth: '',
    gender: '',
    emergencyContactName: '',
    emergencyContactPhone: ''
  });

  // Separate state for payment data (demo only, not persisted)
  const [paymentData, setPaymentData] = useState<PaymentData>({
    cardNumber: '',
    expiryMonth: '',
    expiryYear: '',
    cvv: '',
    cardHolderName: '',
    billingAddress: '',
    billingCity: '',
    billingState: '',
    billingZipCode: ''
  });

  const [formErrors, setFormErrors] = useState<Record<string, string>>({});
  const [activeStep, setActiveStep] = useState(0);

  // Use custom hook for availability checking
  const { checkingAvailability, availabilityResults, availabilityErrors } = useUserAvailability(
    formData.username,
    formData.email
  );

  const steps = ['Account Information', 'Personal Information', 'Contact Details', 'Payment Information'];

  useEffect(() => {
    if (!enrollmentId || !programName) {
      navigate('/enrollment-registration');
    }
  }, [enrollmentId, programName, navigate]);

  // Merge availability errors with form errors
  useEffect(() => {
    setFormErrors(prev => ({ ...prev, ...availabilityErrors }));
  }, [availabilityErrors]);

  const handleInputChange = (field: keyof UserRegistrationData, value: string) => {
    setFormData(prev => ({
      ...prev,
      [field]: value
    }));

    // Clear error for this field when user starts typing
    if (formErrors[field]) {
      setFormErrors(prev => ({
        ...prev,
        [field]: ''
      }));
    }
  };

  const handlePaymentInputChange = (field: keyof PaymentData, value: string) => {
    setPaymentData(prev => ({
      ...prev,
      [field]: value
    }));

    // Clear error for this field when user starts typing
    if (formErrors[field]) {
      setFormErrors(prev => ({
        ...prev,
        [field]: ''
      }));
    }
  };

  const handleNext = async () => {
    const stepErrors = validateStep(activeStep, formData, availabilityResults, paymentData);
    setFormErrors(stepErrors);
    
    if (Object.keys(stepErrors).length === 0) {
      if (activeStep === 3) {
        // Payment step - simulate payment validation
        setIsValidatingPayment(true);
        setError(null);
        
        try {
          // Simulate API delay for payment validation
          await new Promise(resolve => setTimeout(resolve, 2000));
          
          // Additional validation delay for "processing"
          await new Promise(resolve => setTimeout(resolve, 1500));
          
          setPaymentValidated(true);
          setIsValidatingPayment(false);
          
          // Short delay to show success message
          await new Promise(resolve => setTimeout(resolve, 1000));
          
          handleSubmit();
        } catch (error) {
          setIsValidatingPayment(false);
          setError('Payment validation failed. Please check your card information.');
        }
      } else if (activeStep < steps.length - 1) {
        setActiveStep(activeStep + 1);
      }
    }
  };

  const handleBack = () => {
    setActiveStep(activeStep - 1);
  };

  const handleSubmit = async () => {
    // Final validation (skip payment validation as it's already done)
    const finalErrors = validateStep(2, formData, availabilityResults);
    setFormErrors(finalErrors);
    
    if (Object.keys(finalErrors).length > 0) return;

    setLoading(true);
    setError(null);

    try {
      // NOTE: Payment data is NOT included in the API call - it's demo only
      // Only user registration and profile data is sent to the backend
      const registrationData = {
        username: formData.username,
        email: formData.email,
        password: formData.password,
        confirmPassword: formData.confirmPassword,
        enrollmentId: formData.enrollmentId,
        firstName: formData.firstName,
        lastName: formData.lastName,
        phoneNumber: formData.phoneNumber,
        address: formData.address,
        city: formData.city,
        state: formData.state,
        zipCode: formData.zipCode,
        dateOfBirth: formData.dateOfBirth ? new Date(formData.dateOfBirth).toISOString() : null,
        gender: formData.gender,
        emergencyContactName: formData.emergencyContactName,
        emergencyContactPhone: formData.emergencyContactPhone
      };

      const response = await usersApi.register(registrationData);
      
      if (response.status === 201 || response.status === 200) {
        navigate('/registration-success', {
          state: { 
            username: formData.username,
            programName 
          }
        });
      }
    } catch (error: any) {
      console.error('Registration error:', error);
      
      // Handle different types of errors
      if (error.response?.status === 400) {
        // Validation errors from backend
        if (error.response.data?.errors) {
          // Handle ModelState validation errors
          const backendErrors: Record<string, string> = {};
          Object.keys(error.response.data.errors).forEach(key => {
            const errorMessages = error.response.data.errors[key];
            backendErrors[key.toLowerCase()] = Array.isArray(errorMessages) 
              ? errorMessages[0] 
              : errorMessages;
          });
          setFormErrors(prev => ({ ...prev, ...backendErrors }));
          setError('Please fix the validation errors below.');
        } else {
          setError(error.response.data?.message || 'Invalid data provided. Please check your information.');
        }
      } else if (error.response?.status === 409) {
        setError('Username or email already exists. Please choose different credentials.');
      } else if (error.response?.status === 500) {
        setError('Server error occurred. Please try again later.');
      } else if (error.code === 'NETWORK_ERROR') {
        setError('Network connection error. Please check your internet connection.');
      } else {
        setError(error.response?.data?.message || 'Registration failed. Please try again.');
      }
    } finally {
      setLoading(false);
    }
  };

  const renderStepContent = (step: number) => {
    switch (step) {
      case 0:
        return (
          <AccountInformationStep
            formData={formData}
            formErrors={formErrors}
            checkingAvailability={checkingAvailability}
            availabilityResults={availabilityResults}
            onInputChange={handleInputChange}
          />
        );
      case 1:
        return (
          <PersonalInformationStep
            formData={formData}
            formErrors={formErrors}
            onInputChange={handleInputChange}
          />
        );
      case 2:
        return (
          <ContactDetailsStep
            formData={formData}
            formErrors={formErrors}
            onInputChange={handleInputChange}
          />
        );
      case 3:
        return (
          <PaymentInformationStep
            paymentData={paymentData}
            paymentErrors={formErrors}
            onInputChange={(field, value) => handlePaymentInputChange(field, value)}
            isValidating={isValidatingPayment}
            validationSuccess={paymentValidated}
            userProfile={{
              userProfileId: '',
              userId: '',
              firstName: formData.firstName,
              lastName: formData.lastName,
              address: formData.address,
              city: formData.city,
              state: formData.state,
              zipCode: formData.zipCode,
              phoneNumber: formData.phoneNumber,
              createdDate: '',
            }}
            tuitionCost={tuitionCost ?? 2500} // Default to $2,500 if not provided
            programName={programName || 'Selected Program'}
          />
        );
      default:
        return null;
    }
  };

  return (
    <Box sx={{ minHeight: '100vh', bgcolor: 'grey.50' }}>
      <HeroBanner 
        title="Create Your Account"
        subtitle={`Complete your registration for ${programName}`}
      />
      
      <Container maxWidth="md" sx={{ py: 4 }}>
        <Card elevation={3}>
          <CardContent sx={{ p: 4 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 3 }}>
              {activeStep === 0 && (
                <Button
                  startIcon={<ArrowBack />}
                  onClick={() => navigate('/enrollment-registration')}
                  sx={{ mr: 2 }}
                >
                  Back to Program Selection
                </Button>
              )}
              <Typography variant="h5" component="h1" sx={{ flexGrow: 1 }}>
                User Registration
              </Typography>
            </Box>

            {error && (
              <Alert severity="error" sx={{ mb: 3 }}>
                {error}
              </Alert>
            )}

            <Stepper activeStep={activeStep} sx={{ mb: 4 }}>
              {steps.map((label) => (
                <Step key={label}>
                  <StepLabel>{label}</StepLabel>
                </Step>
              ))}
            </Stepper>

            {loading && <LinearProgress sx={{ mb: 2 }} />}

            <Box sx={{ mt: 2 }}>
              {renderStepContent(activeStep)}
            </Box>

            <Box sx={{ display: 'flex', justifyContent: 'space-between', mt: 4 }}>
              <Button
                disabled={activeStep === 0 || loading || isValidatingPayment}
                onClick={handleBack}
                variant="outlined"
              >
                Back
              </Button>
              <Button
                onClick={handleNext}
                variant="contained"
                disabled={loading || isValidatingPayment}
                sx={{ minWidth: 120 }}
              >
                {isValidatingPayment ? 'Processing...' : activeStep === steps.length - 1 ? 'Complete Registration' : 'Next'}
              </Button>
            </Box>
          </CardContent>
        </Card>
      </Container>
    </Box>
  );
};

export default UserRegistration;