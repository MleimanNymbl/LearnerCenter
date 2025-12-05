import React, { useState, useEffect } from 'react';
import {
  Container,
  Box,
  Card,
  CardContent,
  Typography,
  TextField,
  Button,
  Stack,
  CircularProgress,
  Alert,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
  Divider,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
} from '@mui/material';
import { Person, Edit, Save, Cancel } from '@mui/icons-material';
import { useAuth } from '../contexts/AuthContext';
import { authApi } from '../services/apiService';
import { formatPhoneNumber } from '../utils/validation';

interface ProfileData {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  address: string;
  city: string;
  state: string;
  zipCode: string;
  dateOfBirth: string;
  gender: string;
  emergencyContactName: string;
  emergencyContactPhone: string;
}

const Profile: React.FC = () => {
  const { user } = useAuth();
  const [profileData, setProfileData] = useState<ProfileData>({
    username: '',
    email: '',
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
    emergencyContactPhone: '',
  });
  const [originalData, setOriginalData] = useState<ProfileData | null>(null);
  const [isEditing, setIsEditing] = useState(false);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [confirmDialogOpen, setConfirmDialogOpen] = useState(false);

  useEffect(() => {
    fetchProfile();
  }, []);

  const fetchProfile = async () => {
    try {
      setLoading(true);
      setError(null);
      const response = await authApi.getProfile();
      const userData = response.data.data;

      const profile: ProfileData = {
        username: userData.username || '',
        email: userData.email || '',
        firstName: userData.profile?.firstName || '',
        lastName: userData.profile?.lastName || '',
        phoneNumber: userData.profile?.phoneNumber || '',
        address: userData.profile?.address || '',
        city: userData.profile?.city || '',
        state: userData.profile?.state || '',
        zipCode: userData.profile?.zipCode || '',
        dateOfBirth: userData.profile?.dateOfBirth || '',
        gender: userData.profile?.gender || '',
        emergencyContactName: userData.profile?.emergencyContactName || '',
        emergencyContactPhone: userData.profile?.emergencyContactPhone || '',
      };

      setProfileData(profile);
      setOriginalData(profile);
    } catch (err: any) {
      console.error('Failed to fetch profile:', err);
      setError('Failed to load profile. Please try again later.');
    } finally {
      setLoading(false);
    }
  };

  const handleInputChange = (field: keyof ProfileData, value: string) => {
    setProfileData((prev) => ({ ...prev, [field]: value }));
  };

  const handlePhoneChange = (field: 'phoneNumber' | 'emergencyContactPhone', value: string) => {
    const formatted = formatPhoneNumber(value);
    setProfileData((prev) => ({ ...prev, [field]: formatted }));
  };

  const states = [
    'AL', 'AK', 'AZ', 'AR', 'CA', 'CO', 'CT', 'DE', 'FL', 'GA',
    'HI', 'ID', 'IL', 'IN', 'IA', 'KS', 'KY', 'LA', 'ME', 'MD',
    'MA', 'MI', 'MN', 'MS', 'MO', 'MT', 'NE', 'NV', 'NH', 'NJ',
    'NM', 'NY', 'NC', 'ND', 'OH', 'OK', 'OR', 'PA', 'RI', 'SC',
    'SD', 'TN', 'TX', 'UT', 'VT', 'VA', 'WA', 'WV', 'WI', 'WY'
  ];

  const handleEdit = () => {
    setIsEditing(true);
    setSuccess(null);
    setError(null);
  };

  const handleCancel = () => {
    if (originalData) {
      setProfileData(originalData);
    }
    setIsEditing(false);
    setSuccess(null);
    setError(null);
  };

  const handleSaveClick = () => {
    setConfirmDialogOpen(true);
  };

  const handleConfirmSave = async () => {
    setConfirmDialogOpen(false);
    
    try {
      setSaving(true);
      setError(null);
      setSuccess(null);

      // Call backend update endpoint
      await authApi.updateProfile({
        phoneNumber: profileData.phoneNumber,
        address: profileData.address,
        city: profileData.city,
        state: profileData.state,
        zipCode: profileData.zipCode,
        emergencyContactName: profileData.emergencyContactName,
        emergencyContactPhone: profileData.emergencyContactPhone,
      });

      setOriginalData(profileData);
      setIsEditing(false);
      setSuccess('Profile updated successfully!');
    } catch (err: any) {
      console.error('Failed to update profile:', err);
      setError(err.response?.data?.message || 'Failed to update profile. Please try again.');
    } finally {
      setSaving(false);
    }
  };

  const handleCancelDialog = () => {
    setConfirmDialogOpen(false);
  };

  if (loading) {
    return (
      <Container maxWidth="md" sx={{ mt: 8, display: 'flex', justifyContent: 'center' }}>
        <CircularProgress />
      </Container>
    );
  }

  return (
    <Container maxWidth="md" sx={{ mt: 6, mb: 6 }}>
      <Card
        sx={{
          transform: 'perspective(1000px) rotateX(2deg)',
          boxShadow: '0 20px 60px rgba(0,0,0,0.15)',
          borderRadius: 3,
        }}
      >
        <CardContent sx={{ p: 4 }}>
          {/* Header */}
          <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 3 }}>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              <Person color="primary" sx={{ fontSize: 40, mr: 2 }} />
              <Typography variant="h4" fontWeight={600}>
                My Profile
              </Typography>
            </Box>
            {!isEditing ? (
              <Button
                variant="contained"
                startIcon={<Edit />}
                onClick={handleEdit}
                disabled={loading}
              >
                Edit Profile
              </Button>
            ) : (
              <Box sx={{ display: 'flex', gap: 2 }}>
                <Button
                  variant="outlined"
                  startIcon={<Cancel />}
                  onClick={handleCancel}
                  disabled={saving}
                >
                  Cancel
                </Button>
                <Button
                  variant="contained"
                  startIcon={<Save />}
                  onClick={handleSaveClick}
                  disabled={saving}
                >
                  {saving ? 'Saving...' : 'Save Changes'}
                </Button>
              </Box>
            )}
          </Box>

          <Divider sx={{ mb: 3 }} />

          {/* Success/Error Messages */}
          {success && (
            <Alert severity="success" sx={{ mb: 3 }} onClose={() => setSuccess(null)}>
              {success}
            </Alert>
          )}
          {error && (
            <Alert severity="error" sx={{ mb: 3 }} onClose={() => setError(null)}>
              {error}
            </Alert>
          )}

          {/* Profile Form */}
          <Stack spacing={3}>
            {/* Personal Information (Read-only) */}
            <Box sx={{ mt: 2 }}>
              <Typography variant="h6" gutterBottom sx={{ fontWeight: 600, color: '#1976d2' }}>
                Personal Information
              </Typography>
            </Box>
            <Box sx={{ display: 'flex', gap: 2, flexWrap: 'wrap' }}>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Username"
                  value={profileData.username}
                  disabled
                  InputProps={{ readOnly: true }}
                  helperText="Username cannot be changed"
                />
              </Box>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Email"
                  value={profileData.email}
                  disabled
                  InputProps={{ readOnly: true }}
                  helperText="Email cannot be changed"
                />
              </Box>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="First Name"
                  value={profileData.firstName}
                  disabled
                  InputProps={{ readOnly: true }}
                  helperText="First name cannot be changed"
                />
              </Box>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Last Name"
                  value={profileData.lastName}
                  disabled
                  InputProps={{ readOnly: true }}
                  helperText="Last name cannot be changed"
                />
              </Box>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Date of Birth"
                  type="date"
                  value={profileData.dateOfBirth}
                  disabled
                  InputProps={{ readOnly: true }}
                  InputLabelProps={{ shrink: true }}
                  helperText="Date of birth cannot be changed"
                />
              </Box>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Gender"
                  value={profileData.gender}
                  disabled
                  InputProps={{ readOnly: true }}
                  helperText="Gender cannot be changed"
                />
              </Box>
            </Box>

            {/* Contact Information */}
            <Box sx={{ mt: 2 }}>
              <Typography variant="h6" gutterBottom sx={{ fontWeight: 600, color: '#1976d2' }}>
                Contact Information
              </Typography>
            </Box>
            <Box sx={{ display: 'flex', gap: 2, flexWrap: 'wrap' }}>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Phone Number"
                  value={profileData.phoneNumber}
                  onChange={(e) => handlePhoneChange('phoneNumber', e.target.value)}
                  disabled={!isEditing}
                  InputProps={{ readOnly: !isEditing }}
                  helperText="Format: (xxx) xxx-xxxx"
                  inputProps={{ maxLength: 14 }}
                />
              </Box>
              <Box sx={{ flex: '1 1 100%' }}>
                <TextField
                  fullWidth
                  label="Address"
                  value={profileData.address}
                  onChange={(e) => handleInputChange('address', e.target.value)}
                  disabled={!isEditing}
                  InputProps={{ readOnly: !isEditing }}
                />
              </Box>
              <Box sx={{ flex: '1 1 30%', minWidth: '200px' }}>
                <TextField
                  fullWidth
                  label="City"
                  value={profileData.city}
                  onChange={(e) => handleInputChange('city', e.target.value)}
                  disabled={!isEditing}
                  InputProps={{ readOnly: !isEditing }}
                />
              </Box>
              <Box sx={{ flex: '1 1 30%', minWidth: '200px' }}>
                <FormControl fullWidth disabled={!isEditing}>
                  <InputLabel>State</InputLabel>
                  <Select
                    value={profileData.state}
                    onChange={(e) => handleInputChange('state', e.target.value)}
                    label="State"
                  >
                    {states.map((state) => (
                      <MenuItem key={state} value={state}>
                        {state}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Box>
              <Box sx={{ flex: '1 1 30%', minWidth: '200px' }}>
                <TextField
                  fullWidth
                  label="Zip Code"
                  value={profileData.zipCode}
                  onChange={(e) => handleInputChange('zipCode', e.target.value)}
                  disabled={!isEditing}
                  InputProps={{ readOnly: !isEditing }}
                  inputProps={{ maxLength: 10 }}
                />
              </Box>
            </Box>

            {/* Emergency Contact */}
            <Box sx={{ mt: 2 }}>
              <Typography variant="h6" gutterBottom sx={{ fontWeight: 600, color: '#1976d2' }}>
                Emergency Contact
              </Typography>
            </Box>
            <Box sx={{ display: 'flex', gap: 2, flexWrap: 'wrap' }}>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Emergency Contact Name"
                  value={profileData.emergencyContactName}
                  onChange={(e) => handleInputChange('emergencyContactName', e.target.value)}
                  disabled={!isEditing}
                  InputProps={{ readOnly: !isEditing }}
                />
              </Box>
              <Box sx={{ flex: '1 1 45%', minWidth: '250px' }}>
                <TextField
                  fullWidth
                  label="Emergency Contact Phone"
                  value={profileData.emergencyContactPhone}
                  onChange={(e) => handlePhoneChange('emergencyContactPhone', e.target.value)}
                  disabled={!isEditing}
                  InputProps={{ readOnly: !isEditing }}
                  helperText="Format: (xxx) xxx-xxxx"
                  inputProps={{ maxLength: 14 }}
                />
              </Box>
            </Box>
          </Stack>
        </CardContent>
      </Card>

      {/* Confirmation Dialog */}
      <Dialog open={confirmDialogOpen} onClose={handleCancelDialog}>
        <DialogTitle>Confirm Profile Update</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are you sure you want to save these changes to your profile? This action will update your information in the system.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCancelDialog} disabled={saving}>
            Cancel
          </Button>
          <Button onClick={handleConfirmSave} variant="contained" autoFocus disabled={saving}>
            {saving ? 'Saving...' : 'Confirm'}
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default Profile;
