import React, { useState } from 'react';
import {
  Container,
  Paper,
  Typography,
  Box,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  TextField,
  Button,
  Divider,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { School, Person } from '@mui/icons-material';

// Mock colleges data - replace with API call later
const colleges = [
  'Community College of Denver',
  'Front Range Community College',
  'Red Rocks Community College',
  'Arapahoe Community College',
  'Colorado Northwestern Community College',
  'Northeastern Junior College',
];

const EnrollmentRegistration: React.FC = () => {
  const [selectedCollege, setSelectedCollege] = useState('');
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    studentId: '',
  });
  
  const navigate = useNavigate();

  const handleCollegeChange = (event: any) => {
    setSelectedCollege(event.target.value);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // TODO: Handle enrollment registration
    console.log('Enrollment data:', { selectedCollege, ...formData });
    // For now, just show an alert
    alert('Enrollment registration submitted! (This is a demo)');
  };

  return (
    <Container component="main" maxWidth="md">
      <Box
        sx={{
          marginTop: 4,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          minHeight: '80vh',
        }}
      >
        {/* Header */}
        <Box sx={{ textAlign: 'center', mb: 4 }}>
          <School color="primary" sx={{ fontSize: 60, mb: 2 }} />
          <Typography component="h1" variant="h3" gutterBottom>
            Welcome to Learner Center
          </Typography>
          <Typography variant="h6" color="textSecondary">
            Start your educational journey today
          </Typography>
        </Box>

        <Paper elevation={3} sx={{ padding: 4, width: '100%', maxWidth: 600 }}>
          <Typography component="h2" variant="h4" align="center" gutterBottom>
            Student Enrollment
          </Typography>
          
          <Typography variant="body1" align="center" color="textSecondary" sx={{ mb: 3 }}>
            Select your college and provide your information to get started
          </Typography>

          <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
            {/* College Selection */}
            <FormControl fullWidth margin="normal" required>
              <InputLabel>Select Your College</InputLabel>
              <Select
                value={selectedCollege}
                label="Select Your College"
                onChange={handleCollegeChange}
              >
                {colleges.map((college) => (
                  <MenuItem key={college} value={college}>
                    {college}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            {/* Student Information */}
            <Box sx={{ display: 'flex', gap: 2, mt: 2 }}>
              <TextField
                margin="normal"
                required
                fullWidth
                name="firstName"
                label="First Name"
                value={formData.firstName}
                onChange={handleInputChange}
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="lastName"
                label="Last Name"
                value={formData.lastName}
                onChange={handleInputChange}
              />
            </Box>

            <TextField
              margin="normal"
              required
              fullWidth
              name="email"
              label="Email Address"
              type="email"
              value={formData.email}
              onChange={handleInputChange}
            />

            <TextField
              margin="normal"
              fullWidth
              name="phone"
              label="Phone Number"
              type="tel"
              value={formData.phone}
              onChange={handleInputChange}
            />

            <TextField
              margin="normal"
              fullWidth
              name="studentId"
              label="Student ID (if you have one)"
              value={formData.studentId}
              onChange={handleInputChange}
              helperText="Leave blank if you're a new student"
            />

            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2, py: 1.5 }}
              disabled={!selectedCollege || !formData.firstName || !formData.lastName || !formData.email}
            >
              Begin Enrollment Process
            </Button>

            <Divider sx={{ my: 3 }}>
              <Typography variant="body2" color="textSecondary">
                OR
              </Typography>
            </Divider>

            {/* Existing User Login */}
            <Box textAlign="center">
              <Typography variant="body1" sx={{ mb: 2 }}>
                Already a user?
              </Typography>
              <Button
                variant="outlined"
                startIcon={<Person />}
                onClick={() => navigate('/login')}
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
            Need help? Contact your college's admissions office
          </Typography>
        </Box>
      </Box>
    </Container>
  );
};

export default EnrollmentRegistration;