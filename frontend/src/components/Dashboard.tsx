import React, { useState, useEffect } from 'react';
import {
  Container,
  Typography,
  Box,
  Card,
  CardContent,
  Button,
  Divider,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  CircularProgress,
  Alert,
} from '@mui/material';
import { School, Assignment, CreditCard, CheckCircle } from '@mui/icons-material';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import { coursesApi } from '../services/apiService';
import StatusChip, { StatusType } from './common/StatusChip';
import { useEnrollment } from '../hooks/useEnrollment';

interface Course {
  courseId: string;
  courseCode: string;
  courseName: string;
  description: string;
  creditHours: number;
  isActive: boolean;
  enrollmentId: string;
  status?: StatusType;
}

const Dashboard: React.FC = () => {
  const { user } = useAuth();
  const enrollment = useEnrollment();
  const navigate = useNavigate();
  const [courses, setCourses] = useState<Course[]>([]);
  const [currentCourse, setCurrentCourse] = useState<Course | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCourses = async () => {
      if (!user?.enrollmentId) {
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        setError(null);
        const response = await coursesApi.getCoursesByEnrollment(user.enrollmentId);
        const coursesData = Array.isArray(response) ? response : response.data;
        const topThree = coursesData.slice(0, 3);

        // Assign showcase statuses: first is Active, others are random
        const showcaseStatuses: StatusType[] = ['In Progress', 'Blocked', 'Done'];
        const coursesWithStatus = topThree.map((course: Course, index: number) => ({
          ...course,
          status: index === 0 ? 'Active' : showcaseStatuses[index - 1]
        }));

        // Set the first course as the current/active course
        if (coursesWithStatus.length > 0) {
          setCurrentCourse(coursesWithStatus[0]);
        }

        setCourses(coursesWithStatus);
      } catch (err: any) {
        console.error('Failed to fetch courses:', err);
        setError('Failed to load courses. Please try again later.');
      } finally {
        setLoading(false);
      }
    };

    fetchCourses();
  }, [user?.enrollmentId]);

  return (
    <Container maxWidth="xl" sx={{ mt: 4, mb: 4 }}>
      {/* Header */}
      <Box sx={{ mb: 4 }}>
        <Typography variant="h4" gutterBottom>
          {user?.role || 'Student'} Dashboard
        </Typography>
        <Typography variant="body1" color="textSecondary">
          Welcome back, {user?.firstName || user?.username || 'Student'}! Here's an overview of your academic progress.
        </Typography>
      </Box>

      {/* Dashboard Grid */}
      <Box sx={{ display: 'flex', gap: 3, maxWidth: '1400px', mx: 'auto' }}>
        {/* Left Column - Larger Cards */}
        <Box sx={{ flex: '0 0 66%', display: 'flex', flexDirection: 'column', gap: 3 }}>
          {/* Current Course Card */}
          <Card sx={{ minHeight: 400, display: 'flex', flexDirection: 'column' }}>
            <CardContent sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
              <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 2 }}>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <School color="primary" sx={{ mr: 1 }} />
                  <Typography variant="h6">Current Course</Typography>
                </Box>
                {currentCourse && <StatusChip status={currentCourse.status || 'Active'} />}
              </Box>
              <Divider sx={{ mb: 2 }} />

              {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: 300 }}>
                  <CircularProgress />
                </Box>
              ) : error ? (
                <Alert severity="error">{error}</Alert>
              ) : currentCourse ? (
                <>
                  {/* Course Title */}
                  <Typography variant="h4" sx={{ fontWeight: 600, mb: 1, color: '#1976d2' }}>
                    {currentCourse.courseName}
                  </Typography>

                  {/* Course Metadata */}
                  <Box sx={{ display: 'flex', gap: 3, mb: 3 }}>
                    <Box>
                      <Typography variant="caption" color="textSecondary" display="block">
                        COURSE CODE
                      </Typography>
                      <Typography variant="body1" sx={{ fontWeight: 500 }}>
                        {currentCourse.courseCode}
                      </Typography>
                    </Box>
                    <Box>
                      <Typography variant="caption" color="textSecondary" display="block">
                        CREDIT HOURS
                      </Typography>
                      <Typography variant="body1" sx={{ fontWeight: 500 }}>
                        {currentCourse.creditHours}
                      </Typography>
                    </Box>
                  </Box>

                  {/* Course Description */}
                  {currentCourse.description && (
                    <Box sx={{ mb: 3 }}>
                      <Typography variant="caption" color="textSecondary" display="block" gutterBottom>
                        DESCRIPTION
                      </Typography>
                      <Typography variant="body1" sx={{ lineHeight: 1.7, color: '#555' }}>
                        {currentCourse.description}
                      </Typography>
                    </Box>
                  )}

                  <Box sx={{ display: 'flex', justifyContent: 'flex-end', mt: 'auto', pt: 3 }}>
                    <Button
                      variant="contained"
                      color="primary"
                      size="large"
                      component="a"
                      href="https://www.youtube.com/watch?v=ub82Xb1C8os"
                      target="_blank"
                      rel="noopener noreferrer"
                    >
                      Continue Learning
                    </Button>
                  </Box>

                </>
              ) : (
                <Typography color="textSecondary" sx={{ textAlign: 'center', mt: 4 }}>
                  No active course at the moment.
                </Typography>
              )}
            </CardContent>
          </Card>

          {/* Upcoming Courses Card */}
          <Card sx={{ minHeight: 400 }}>
            <CardContent sx={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <School color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">Upcoming Courses</Typography>
              </Box>
              <Divider sx={{ mb: 2 }} />

              {/* Course Table - 75% height and width */}
              <Box sx={{ flexGrow: 1, display: 'flex', justifyContent: 'center', alignItems: 'flex-start' }}>
                {loading ? (
                  <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', width: '100%', height: '75%' }}>
                    <CircularProgress />
                  </Box>
                ) : error ? (
                  <Alert severity="error" sx={{ width: '75%' }}>{error}</Alert>
                ) : courses.length === 0 ? (
                  <Typography color="textSecondary" sx={{ width: '75%', textAlign: 'center' }}>
                    No courses found for your enrollment.
                  </Typography>
                ) : (
                  <TableContainer component={Paper} sx={{ width: '75%', boxShadow: 'none', border: 'none' }}>
                    <Table sx={{ '& .MuiTableCell-root': { border: 'none' } }}>
                      <TableHead>
                        <TableRow>
                          <TableCell sx={{ backgroundColor: '#f5f5f5' }}><strong>Course Title</strong></TableCell>
                          <TableCell align="center" sx={{ backgroundColor: '#f5f5f5' }}><strong>Status</strong></TableCell>
                        </TableRow>
                      </TableHead>
                      <TableBody>
                        {courses.map((course) => (
                          <TableRow key={course.courseId} hover>
                            <TableCell>{course.courseName}</TableCell>
                            <TableCell align="center">
                              <StatusChip status={course.status || (course.isActive ? 'Active' : 'Inactive')} />
                            </TableCell>
                          </TableRow>
                        ))}
                      </TableBody>
                    </Table>
                  </TableContainer>
                )}
              </Box>

              <Box sx={{ mt: 2, display: 'flex', justifyContent: 'flex-end' }}>
                <Button variant="outlined" color="primary" onClick={() => navigate('/courses')}>
                  View All Courses
                </Button>
              </Box>
            </CardContent>
          </Card>
        </Box>

        {/* Right Column - Smaller Cards */}
        <Box sx={{ flex: '0 0 33%', display: 'flex', flexDirection: 'column', justifyContent: 'space-between' }}>
          {/* My Programs Card */}
          <Card sx={{ flex: 1 }}>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 2 }}>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <Assignment color="primary" sx={{ mr: 1 }} />
                  <Typography variant="h6">My Programs</Typography>
                </Box>
                {enrollment && <StatusChip status="Active" />}
              </Box>
              <Divider sx={{ mb: 2 }} />
              {enrollment ? (
                <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}>
                  <Box>
                    <Typography variant="caption" color="textSecondary" display="block">
                      PROGRAM NAME
                    </Typography>
                    <Typography variant="body1" sx={{ fontWeight: 500 }}>
                      {enrollment.programName}
                    </Typography>
                  </Box>
                  <Box>
                    <Typography variant="caption" color="textSecondary" display="block">
                      DEGREE
                    </Typography>
                    <Typography variant="body1" sx={{ fontWeight: 500 }}>
                      {enrollment.degree || 'N/A'}
                    </Typography>
                  </Box>
                  <Box>
                    <Typography variant="caption" color="textSecondary" display="block">
                      ENROLLMENT DATE
                    </Typography>
                    <Typography variant="body1" sx={{ fontWeight: 500 }}>
                      {enrollment.createdDate
                        ? new Date(enrollment.createdDate).toLocaleDateString()
                        : 'N/A'}
                    </Typography>
                  </Box>
                </Box>
              ) : (
                <Typography color="textSecondary">No enrollment information available.</Typography>
              )}
            </CardContent>
          </Card>

          {/* Make a Payment Card */}
          <Card sx={{ flex: 1, my: 3 }}>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <CreditCard color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">Make a Payment</Typography>
              </Box>
              <Divider sx={{ mb: 2 }} />
              {/* Content will be added later */}
            </CardContent>
          </Card>

          {/* My Tasks Card */}
          <Card sx={{ flex: 1 }}>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <CheckCircle color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">My Tasks</Typography>
              </Box>
              <Divider sx={{ mb: 2 }} />
              {/* Content will be added later */}
            </CardContent>
          </Card>
        </Box>
      </Box>
    </Container>
  );
};

export default Dashboard;