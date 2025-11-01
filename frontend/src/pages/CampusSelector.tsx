import React, { useState, useEffect } from 'react';
import {
  Container,
  Paper,
  Typography,
  Box,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  Divider,
  Alert,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Person, LocationOn } from '@mui/icons-material';
import { campusApi } from '../services/apiService';
import { Campus } from '../types';
import HeroBanner from '../components/HeroBanner';
import LoadingSpinner from '../components/LoadingSpinner';

const CampusSelector: React.FC = () => {
  const [campuses, setCampuses] = useState<Campus[]>([]);
  const [selectedCampus, setSelectedCampus] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  
  const navigate = useNavigate();

  useEffect(() => {
    const loadCampuses = async () => {
      try {
        setLoading(true);
        setError(null);
        
        const response = await campusApi.getAllCampuses();
        console.log('Campus API response:', response);
        
        // Handle the response - it might be directly the array or wrapped in .data
        const campusData = Array.isArray(response) ? response : response.data;
        console.log('Campus data:', campusData);
        setCampuses(campusData);
      } catch (err: any) {
        console.error('Error loading campuses:', err);
        console.error('Error response:', err.response);
        setError(`Failed to load campuses: ${err.message || 'Please try again.'}`);
      } finally {
        setLoading(false);
      }
    };

    loadCampuses();
  }, []);

  const handleCampusChange = (event: any) => {
    setSelectedCampus(event.target.value);
  };

  const handleContinue = () => {
    if (selectedCampus) {
      // Store selected campus in localStorage for later use
      const selectedCampusData = campuses.find(c => c.campusId === selectedCampus);
      if (selectedCampusData) {
        localStorage.setItem('selectedCampus', JSON.stringify(selectedCampusData));
        // Navigate to enrollment registration with campus ID
        navigate(`/enrollment-registration/${selectedCampusData.campusId}`);
      }
    }
  };

  const handleSignIn = () => {
    navigate('/login');
  };

  if (loading) {
    return <LoadingSpinner size={60} />;
  }

  return (
    <>
      <HeroBanner 
        title="Welcome to Learner Center"
        subtitle="Your educational journey starts here"
      />

      <Container component="main" maxWidth="md">
        <Box
          sx={{
            marginTop: 6,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            minHeight: '60vh',
            paddingBottom: 4,
          }}
        >
          {/* Section Header */}
          <Box sx={{ textAlign: 'center', mb: 4 }}>
            <LocationOn color="primary" sx={{ fontSize: 50, mb: 2 }} />
            <Typography component="h2" variant="h4" gutterBottom>
              Choose Your Campus
            </Typography>
            <Typography variant="h6" color="textSecondary">
              Select the campus where you would like to begin your educational journey
            </Typography>
          </Box>

        <Paper elevation={3} sx={{ padding: 4, width: '100%', maxWidth: 600 }}>

          {error && (
            <Alert severity="error" sx={{ mb: 3 }}>
              {error}
            </Alert>
          )}

          <Box sx={{ mt: 2 }}>
            {/* Campus Selection */}
            <FormControl fullWidth margin="normal" required>
              <InputLabel>Select Your Campus</InputLabel>
              <Select
                value={selectedCampus}
                label="Select Your Campus"
                onChange={handleCampusChange}
                disabled={loading || campuses.length === 0}
                MenuProps={{
                  PaperProps: {
                    style: {
                      maxHeight: 300,
                      overflow: 'auto',
                    },
                  },
                }}
              >
                {campuses.map((campus) => (
                  <MenuItem key={campus.campusId} value={campus.campusId}>
                    <Box>
                      <Typography variant="body1" fontWeight="medium">
                        {campus.campusName}
                      </Typography>
                      {campus.city && campus.state && (
                        <Typography variant="body2" color="textSecondary">
                          {campus.city}, {campus.state}
                        </Typography>
                      )}
                    </Box>
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            {/* Selected Campus Details */}
            {selectedCampus && (
              <Paper 
                variant="outlined" 
                sx={{ p: 2, mt: 3, backgroundColor: 'grey.50' }}
              >
                {(() => {
                  const campus = campuses.find(c => c.campusId === selectedCampus);
                  return campus ? (
                    <Box>
                      <Typography variant="h6" gutterBottom>
                        {campus.campusName}
                      </Typography>
                      {campus.address && (
                        <Typography variant="body2" color="textSecondary">
                          📍 {campus.address}
                          {campus.city && campus.state && campus.zipCode && (
                            <>, {campus.city}, {campus.state} {campus.zipCode}</>
                          )}
                        </Typography>
                      )}
                      {campus.phoneNumber && (
                        <Typography variant="body2" color="textSecondary">
                          📞 {campus.phoneNumber}
                        </Typography>
                      )}
                      {campus.email && (
                        <Typography variant="body2" color="textSecondary">
                          ✉️ {campus.email}
                        </Typography>
                      )}
                    </Box>
                  ) : null;
                })()}
              </Paper>
            )}

            <Button
              fullWidth
              variant="contained"
              onClick={handleContinue}
              sx={{ mt: 3, mb: 2, py: 1.5 }}
              disabled={!selectedCampus}
            >
              Continue to Enrollment
            </Button>

            <Divider sx={{ my: 3 }}>
              <Typography variant="body2" color="textSecondary">
                OR
              </Typography>
            </Divider>

            {/* Existing User Login */}
            <Box textAlign="center">
              <Typography variant="body1" sx={{ mb: 2 }}>
                Already have an account?
              </Typography>
              <Button
                variant="outlined"
                startIcon={<Person />}
                onClick={handleSignIn}
                fullWidth
                sx={{ py: 1.5 }}
              >
                Sign In to Your Account
              </Button>
            </Box>
          </Box>
        </Paper>

        {/* Footer */}
        <Box sx={{ mt: 4, textAlign: 'center' }}>
          <Typography variant="body2" color="textSecondary">
            Need help choosing a campus? Contact our admissions team
          </Typography>
        </Box>
      </Box>
    </Container>
    </>
  );
};

export default CampusSelector;