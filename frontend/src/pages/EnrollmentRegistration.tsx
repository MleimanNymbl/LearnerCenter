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
  Card,
  CardContent,
  Chip,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { School, AccessTime, AttachMoney, ArrowBack, Groups, Visibility } from '@mui/icons-material';
import { enrollmentApi, coursesApi } from '../services/apiService';
import { Enrollment, Campus } from '../types';
import HeroBanner from '../components/HeroBanner';
import CampusInfoBanner from '../components/CampusInfoBanner';
import LoadingSpinner from '../components/LoadingSpinner';
import CourseDetailsModal from '../components/CourseDetailsModal';

interface Course {
  courseId: string;
  courseCode: string;
  courseName: string;
  description: string;
  creditHours: number;
  isActive: boolean;
}

const EnrollmentRegistration: React.FC = () => {
  const [enrollments, setEnrollments] = useState<Enrollment[]>([]);
  const [selectedEnrollment, setSelectedEnrollment] = useState('');
  const [selectedCampus, setSelectedCampus] = useState<Campus | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [courseModalOpen, setCourseModalOpen] = useState(false);
  const [courses, setCourses] = useState<Course[]>([]);
  const [courseLoading, setCourseLoading] = useState(false);
  const [courseError, setCourseError] = useState<string | null>(null);
  
  const navigate = useNavigate();

  useEffect(() => {
    const campusData = localStorage.getItem('selectedCampus');
    if (campusData) {
      const campus: Campus = JSON.parse(campusData);
      setSelectedCampus(campus);
      loadEnrollmentsByCampus(campus.campusId);
    } else {
      // If no campus selected, redirect back to campus selector
      navigate('/');
    }
  }, [navigate]);

  const loadEnrollmentsByCampus = async (campusId: string) => {
    try {
      setLoading(true);
      setError(null);
      console.log('Attempting to fetch enrollments for campus:', campusId);
      
      const response = await enrollmentApi.getEnrollmentsByCampus(campusId);
      console.log('Enrollment API response:', response);
      
      // Handle the response - it might be directly the array or wrapped in .data
      const enrollmentData = Array.isArray(response) ? response : response.data;
      
      if (Array.isArray(enrollmentData)) {
        setEnrollments(enrollmentData);
        console.log('Loaded enrollments:', enrollmentData);
      } else {
        console.error('Unexpected response format:', response);
        setError('Unexpected response format from server');
      }
    } catch (error) {
      console.error('Error loading enrollments:', error);
      setError('Failed to load enrollment programs. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  const handleEnrollmentChange = (event: any) => {
    setSelectedEnrollment(event.target.value);
  };

  const handleContinue = () => {
    if (selectedEnrollment) {
      // Store selected enrollment for next step
      const selectedEnrollmentData = enrollments.find(e => e.enrollmentId === selectedEnrollment);
      if (selectedEnrollmentData) {
        localStorage.setItem('selectedEnrollment', JSON.stringify(selectedEnrollmentData));
        console.log('Selected enrollment data:', selectedEnrollmentData);
        console.log('Tuition cost being passed:', selectedEnrollmentData.cost);
        // Navigate to user registration form
        navigate('/user-registration', {
          state: {
            enrollmentId: selectedEnrollmentData.enrollmentId,
            programName: selectedEnrollmentData.name,
            tuitionCost: selectedEnrollmentData.cost
          }
        });
      }
    }
  };

  const handleBackToCampus = () => {
    navigate('/');
  };

  const handleOpenCourseModal = async () => {
    if (selectedEnrollment) {
      setCourseModalOpen(true);
      setCourseLoading(true);
      setCourseError(null);
      
      try {
        console.log('Fetching courses for enrollment:', selectedEnrollment);
        const response = await coursesApi.getCoursesByEnrollment(selectedEnrollment);
        console.log('Courses API response:', response);
        
        // Handle the response - it might be directly the array or wrapped in .data
        const courseData = Array.isArray(response) ? response : response.data;
        
        if (Array.isArray(courseData)) {
          setCourses(courseData as Course[]);
          console.log('Loaded courses:', courseData);
        } else {
          console.error('Unexpected response format:', response);
          setCourseError('Unexpected response format from server');
          setCourses([]);
        }
      } catch (error) {
        console.error('Error loading courses:', error);
        setCourseError('Failed to load courses. Please try again.');
        setCourses([]);
      } finally {
        setCourseLoading(false);
      }
    }
  };

  const handleCloseCourseModal = () => {
    setCourseModalOpen(false);
    setCourses([]);
    setCourseError(null);
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  const selectedEnrollmentData = enrollments.find(e => e.enrollmentId === selectedEnrollment);

  return (
    <>
      <HeroBanner 
        title="Select Your Program"
        subtitle={selectedCampus ? `Programs at ${selectedCampus.campusName}` : 'Choose your educational path'}
      />

      <Container maxWidth="md" sx={{ mt: 4, mb: 4 }}>
      {/* Header */}
      <Box sx={{ mb: 4, textAlign: 'center' }}>
        {selectedCampus && (
          <Typography variant="h6" color="text.secondary">
            Available programs at {selectedCampus.campusName}
          </Typography>
        )}
      </Box>

      {/* Campus Info Banner */}
      {selectedCampus && (
        <CampusInfoBanner campus={selectedCampus} showIcon />
      )}

      <Paper elevation={3} sx={{ p: 4 }}>
        {error && (
          <Alert severity="error" sx={{ mb: 3 }}>
            {error}
          </Alert>
        )}

        {/* Enrollment Selector */}
        <Box sx={{ mb: 4 }}>
          <FormControl fullWidth>
            <InputLabel>Select a Program</InputLabel>
            <Select
              value={selectedEnrollment}
              label="Select a Program"
              onChange={handleEnrollmentChange}
              MenuProps={{
                PaperProps: {
                  style: {
                    maxHeight: 300,
                  },
                },
              }}
            >
              {enrollments.map((enrollment) => (
                <MenuItem key={enrollment.enrollmentId} value={enrollment.enrollmentId}>
                  <Box sx={{ width: '100%' }}>
                    <Typography variant="subtitle1" sx={{ fontWeight: 'bold' }}>
                      {enrollment.name}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {enrollment.programType} • {enrollment.durationWeeks} weeks • ${enrollment.cost.toLocaleString()}
                    </Typography>
                  </Box>
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Box>

        {/* Selected Enrollment Details */}
        {selectedEnrollmentData && (
          <Box sx={{ mb: 4 }}>
            <Divider sx={{ mb: 3 }}>
              <Typography variant="h6" color="primary">
                Program Details
              </Typography>
            </Divider>
            
            <Card elevation={1} sx={{ mb: 3 }}>
              <CardContent>
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mb: 2 }}>
                  <Typography variant="h5" sx={{ fontWeight: 'bold', color: 'primary.main' }}>
                    {selectedEnrollmentData.name}
                  </Typography>
                  <Button
                    variant="outlined"
                    startIcon={<Visibility />}
                    onClick={handleOpenCourseModal}
                    sx={{ minWidth: 'auto' }}
                  >
                    View Courses
                  </Button>
                </Box>
                
                <Typography variant="body1" sx={{ mb: 3, lineHeight: 1.6 }}>
                  {selectedEnrollmentData.description}
                </Typography>

                <Box sx={{ 
                  display: 'grid', 
                  gridTemplateColumns: { xs: '1fr', sm: '1fr 1fr', md: '1fr 1fr 1fr 1fr' }, 
                  gap: 3 
                }}>
                  <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                    <School color="primary" />
                    <Box>
                      <Typography variant="caption" color="text.secondary">
                        Program Type
                      </Typography>
                      <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
                        {selectedEnrollmentData.programType}
                      </Typography>
                    </Box>
                  </Box>
                  
                  <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                    <AccessTime color="primary" />
                    <Box>
                      <Typography variant="caption" color="text.secondary">
                        Duration
                      </Typography>
                      <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
                        {selectedEnrollmentData.durationWeeks} weeks
                      </Typography>
                    </Box>
                  </Box>
                  
                  <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                    <AttachMoney color="primary" />
                    <Box>
                      <Typography variant="caption" color="text.secondary">
                        Cost
                      </Typography>
                      <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
                        ${selectedEnrollmentData.cost.toLocaleString()}
                      </Typography>
                    </Box>
                  </Box>
                  
                  <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                    <Groups color="primary" />
                    <Box>
                      <Typography variant="caption" color="text.secondary">
                        Courses
                      </Typography>
                      <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
                        {selectedEnrollmentData.courseCount} courses
                      </Typography>
                    </Box>
                  </Box>
                </Box>

                {selectedEnrollmentData.isActive && (
                  <Box sx={{ mt: 2 }}>
                    <Chip 
                      label="Currently Accepting Enrollments" 
                      color="success" 
                      variant="filled" 
                      sx={{ fontWeight: 'bold' }}
                    />
                  </Box>
                )}
              </CardContent>
            </Card>
          </Box>
        )}

        {/* Action Buttons */}
        <Box sx={{ display: 'flex', gap: 2, justifyContent: 'space-between', mt: 4 }}>
          <Button
            variant="outlined"
            startIcon={<ArrowBack />}
            onClick={handleBackToCampus}
            size="large"
          >
            Back to Campus Selection
          </Button>
          
          <Button
            variant="contained"
            size="large"
            onClick={handleContinue}
            disabled={!selectedEnrollment}
            sx={{ px: 4 }}
          >
            Continue to Registration
          </Button>
        </Box>

        {enrollments.length === 0 && !loading && !error && (
          <Alert severity="info" sx={{ mt: 3 }}>
            No enrollment programs are currently available for this campus.
          </Alert>
        )}
      </Paper>
    </Container>

    {/* Course Details Modal */}
    {selectedEnrollmentData && (
      <CourseDetailsModal
        open={courseModalOpen}
        onClose={handleCloseCourseModal}
        enrollmentName={selectedEnrollmentData.name}
        enrollmentDescription={selectedEnrollmentData.description}
        courses={courses}
        loading={courseLoading}
        error={courseError}
      />
    )}
    </>
  );
};

export default EnrollmentRegistration;