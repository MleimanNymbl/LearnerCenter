import React, { useState, useEffect } from 'react';
import {
  Container,
  Typography,
  Box,
} from '@mui/material';
import { useAuth } from '../contexts/AuthContext';
import { coursesApi } from '../services/apiService';
import { StatusType } from './common/StatusChip';
import { useEnrollment } from '../hooks/useEnrollment';
import CurrentCourseCard from './dashboard/CurrentCourseCard';
import UpcomingCoursesCard from './dashboard/UpcomingCoursesCard';
import MyProgramsCard from './dashboard/MyProgramsCard';
import MakePaymentCard from './dashboard/MakePaymentCard';
import MyTasksCard from './dashboard/MyTasksCard';

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
      <Box sx={{ mb: 4 }}>
        <Typography variant="h4" gutterBottom>
          {user?.role || 'Student'} Dashboard
        </Typography>
        <Typography variant="body1" color="textSecondary">
          Welcome back, {user?.firstName || user?.username || 'Student'}! Here's an overview of your academic progress.
        </Typography>
      </Box>
      <Box sx={{ display: 'flex', flexDirection: { xs: 'column', md: 'row' }, gap: 3, maxWidth: '1400px', mx: 'auto', alignItems: 'flex-start' }}>
        <Box sx={{ flex: { xs: '1', md: '0 0 66%' }, display: 'flex', flexDirection: 'column', gap: 3 }}>
          <Box sx={{ minHeight: 416 }}>
            <CurrentCourseCard 
              course={currentCourse}
              loading={loading}
              error={error}
            />
          </Box>
          <Box sx={{ minHeight: 416 }}>
            <UpcomingCoursesCard 
              courses={courses}
              loading={loading}
              error={error}
            />
          </Box>
        </Box>
        <Box sx={{ flex: '0 0 33%', display: 'flex', flexDirection: 'column', gap: 3 }}>
          <MyProgramsCard enrollment={enrollment} />
          <MakePaymentCard cost={enrollment?.cost || 0} loading={!enrollment} />
          <MyTasksCard />
        </Box>
      </Box>
    </Container>
  );
};

export default Dashboard;