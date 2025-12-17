import React, { useState, useEffect } from 'react';
import {
  Container,
  Typography,
  Box,
  Card,
  CardContent,
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
import { School, Assignment, Grade } from '@mui/icons-material';
import { useAuth } from '../contexts/AuthContext';
import { coursesApi } from '../services/apiService';
import StatusChip, { StatusType } from '../components/common/StatusChip';
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
  grade?: string;
  completedDate?: string;
}

// Helper function to convert letter grade to GPA points
const gradeToGPA = (grade: string): number => {
  const gradeMap: { [key: string]: number } = {
    'A': 4.0,
    'A-': 3.7,
    'B+': 3.3,
    'B': 3.0,
    'B-': 2.7,
    'C+': 2.3,
    'C': 2.0,
    'C-': 1.7,
    'D+': 1.3,
    'D': 1.0,
    'F': 0.0,
  };
  return gradeMap[grade] || 0.0;
};

const CoursesAndGrades: React.FC = () => {
  const { user } = useAuth();
  const enrollment = useEnrollment();
  const [courses, setCourses] = useState<Course[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [currentGPA, setCurrentGPA] = useState<number>(0);
  const [totalCreditsEarned, setTotalCreditsEarned] = useState<number>(0);
  const [totalCreditsRequired, setTotalCreditsRequired] = useState<number>(0);

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
        
        // Assign showcase statuses and mock data for display
        const showcaseStatuses: StatusType[] = ['Active', 'In Progress', 'Done', 'Blocked', 'Inactive'];
        const mockGrades = ['A', 'B+', 'A-', 'B', 'C+', 'A', 'B-'];
        const mockDates = [
          '2024-12-15',
          '2024-11-20',
          '2024-10-05',
          '2024-09-12',
          '2024-08-25',
          '2024-07-18',
          '2024-06-30',
        ];
        
        const coursesWithStatus = coursesData.map((course: Course, index: number) => ({
          ...course,
          status: showcaseStatuses[index % showcaseStatuses.length],
          grade: mockGrades[index % mockGrades.length],
          completedDate: mockDates[index % mockDates.length],
        }));
        
        setCourses(coursesWithStatus);
        
        // Calculate GPA and credits
        let totalGradePoints = 0;
        let totalCreditHours = 0;
        let earnedCredits = 0;
        
        coursesWithStatus.forEach((course: Course) => {
          const creditHours = course.creditHours || 0;
          totalCreditHours += creditHours;
          
          // Only count completed courses (Done status) for earned credits
          if (course.status === 'Done' && course.grade) {
            const gpa = gradeToGPA(course.grade);
            totalGradePoints += gpa * creditHours;
            earnedCredits += creditHours;
          }
        });
        
        // Calculate GPA (only from completed courses)
        const gpa = earnedCredits > 0 ? totalGradePoints / earnedCredits : 0;
        setCurrentGPA(parseFloat(gpa.toFixed(2)));
        setTotalCreditsEarned(earnedCredits);
        setTotalCreditsRequired(totalCreditHours);
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
          Courses & Grades
        </Typography>
        <Typography variant="body1" color="textSecondary">
          View all your courses, grades, and academic performance.
        </Typography>
      </Box>
      {/* Page Grid */}
      <Box sx={{ display: 'flex', flexDirection: { xs: 'column', md: 'row' }, gap: 3, maxWidth: '1400px', mx: 'auto' }}>
        {/* Left Column - All Courses Card */}
        <Box sx={{ flex: { xs: '1', md: '0 0 66%' } }}>
          <Card sx={{ minHeight: 600 }}>
            <CardContent sx={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <School color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">All Courses</Typography>
              </Box>
              <Divider sx={{ mb: 2 }} />
              
              {/* Courses Table */}
              <Box sx={{ flexGrow: 1 }}>
                {loading ? (
                  <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: 400 }}>
                    <CircularProgress />
                  </Box>
                ) : error ? (
                  <Alert severity="error">{error}</Alert>
                ) : courses.length === 0 ? (
                  <Typography color="textSecondary" sx={{ textAlign: 'center', mt: 4 }}>
                    No courses found for your enrollment.
                  </Typography>
                ) : (
                  <TableContainer component={Paper} sx={{ boxShadow: 'none', border: 'none', overflowX: 'auto' }}>
                    <Table sx={{ '& .MuiTableCell-root': { border: 'none' } }}>
                      <TableHead>
                        <TableRow>
                          <TableCell sx={{ backgroundColor: '#f5f5f5' }}><strong>Course Title</strong></TableCell>
                          <TableCell align="center" sx={{ backgroundColor: '#f5f5f5' }}><strong>Status</strong></TableCell>
                          <TableCell align="center" sx={{ backgroundColor: '#f5f5f5' }}><strong>Grade</strong></TableCell>
                          <TableCell align="center" sx={{ backgroundColor: '#f5f5f5' }}><strong>Date</strong></TableCell>
                          <TableCell align="center" sx={{ backgroundColor: '#f5f5f5' }}><strong>Credits</strong></TableCell>
                        </TableRow>
                      </TableHead>
                      <TableBody>
                        {courses.map((course) => (
                          <TableRow key={course.courseId} hover>
                            <TableCell>{course.courseName}</TableCell>
                            <TableCell align="center">
                              <StatusChip status={course.status || (course.isActive ? 'Active' : 'Inactive')} />
                            </TableCell>
                            <TableCell align="center">
                              <Typography variant="body2" sx={{ fontWeight: 600 }}>
                                {course.grade || '-'}
                              </Typography>
                            </TableCell>
                            <TableCell align="center">
                              {course.completedDate 
                                ? new Date(course.completedDate).toLocaleDateString()
                                : '-'}
                            </TableCell>
                            <TableCell align="center">{course.creditHours}</TableCell>
                          </TableRow>
                        ))}
                      </TableBody>
                    </Table>
                  </TableContainer>
                )}
              </Box>
            </CardContent>
          </Card>
        </Box>

        {/* Right Column - My Programs & My Grades Cards */}
        <Box sx={{ flex: { xs: '1', md: '0 0 33%' }, display: 'flex', flexDirection: 'column', gap: 3 }}>
          {/* My Programs Card */}
          <Card sx={{ minHeight: 285 }}>
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
                        ? new Date(enrollment.createdDate.split(' ')[0]).toLocaleDateString()
                        : 'N/A'}
                    </Typography>
                  </Box>
                </Box>
              ) : (
                <Typography color="textSecondary">No enrollment information available.</Typography>
              )}
            </CardContent>
          </Card>

          {/* My Grades Card */}
          <Card sx={{ minHeight: 285 }}>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Grade color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">My Grades</Typography>
              </Box>
              <Divider sx={{ mb: 2 }} />
              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3, mt: 3 }}>
                <Box>
                  <Typography variant="caption" color="textSecondary" display="block">
                    CURRENT GPA
                  </Typography>
                  <Typography variant="h4" sx={{ fontWeight: 600, color: '#1976d2' }}>
                    {loading ? '-' : currentGPA.toFixed(2)}
                  </Typography>
                  <Typography variant="caption" color="textSecondary">
                    {!loading && `Based on ${totalCreditsEarned} completed credits`}
                  </Typography>
                </Box>
                <Box>
                  <Typography variant="caption" color="textSecondary" display="block">
                    TOTAL CREDITS
                  </Typography>
                  <Typography variant="h4" sx={{ fontWeight: 600, color: '#1976d2' }}>
                    {loading ? '-' : `${totalCreditsEarned} / ${totalCreditsRequired}`}
                  </Typography>
                  <Typography variant="caption" color="textSecondary">
                    {!loading && `${totalCreditsRequired - totalCreditsEarned} credits remaining`}
                  </Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Box>
      </Box>
    </Container>
  );
};

export default CoursesAndGrades;
