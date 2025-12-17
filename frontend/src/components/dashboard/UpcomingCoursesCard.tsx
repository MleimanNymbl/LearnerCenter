import React from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Divider,
  Button,
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
import { School } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import StatusChip, { StatusType } from '../common/StatusChip';

interface Course {
  courseId: string;
  courseCode: string;
  courseName: string;
  isActive: boolean;
  status?: StatusType;
}

interface UpcomingCoursesCardProps {
  courses: Course[];
  loading: boolean;
  error: string | null;
}

const UpcomingCoursesCard: React.FC<UpcomingCoursesCardProps> = ({ courses, loading, error }) => {
  const navigate = useNavigate();

  return (
    <Card sx={{ flex: 1, minHeight: 413 }}>
      <CardContent sx={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
        <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
          <School color="primary" sx={{ mr: 1 }} />
          <Typography variant="h6">Upcoming Courses</Typography>
        </Box>
        <Divider sx={{ mb: 2 }} />

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

        <Box sx={{ mt: 7, display: 'flex', justifyContent: 'flex-end' }}>
          <Button variant="outlined" color="primary" onClick={() => navigate('/courses')}>
            View All Courses
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default UpcomingCoursesCard;
