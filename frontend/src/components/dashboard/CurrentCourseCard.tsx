import React from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Divider,
  Button,
  CircularProgress,
  Alert,
} from '@mui/material';
import { School } from '@mui/icons-material';
import StatusChip, { StatusType } from '../common/StatusChip';

interface Course {
  courseId: string;
  courseCode: string;
  courseName: string;
  description: string;
  creditHours: number;
  isActive: boolean;
  status?: StatusType;
}

interface CurrentCourseCardProps {
  course: Course | null;
  loading: boolean;
  error: string | null;
}

const CurrentCourseCard: React.FC<CurrentCourseCardProps> = ({ course, loading, error }) => {
  return (
    <Card sx={{ display: 'flex', flexDirection: 'column', flex: 1, minHeight: 413 }}>
      <CardContent sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
        <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 2 }}>
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <School color="primary" sx={{ mr: 1 }} />
            <Typography variant="h6">Current Course</Typography>
          </Box>
          {course && <StatusChip status={course.status || 'Active'} />}
        </Box>
        <Divider sx={{ mb: 2 }} />

        {loading ? (
          <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: 300 }}>
            <CircularProgress />
          </Box>
        ) : error ? (
          <Alert severity="error">{error}</Alert>
        ) : course ? (
          <>
            <Typography variant="h4" sx={{ fontWeight: 600, mb: 1, color: '#1976d2' }}>
              {course.courseName}
            </Typography>

            <Box sx={{ display: 'flex', gap: 3, mb: 3 }}>
              <Box>
                <Typography variant="caption" color="textSecondary" display="block">
                  COURSE CODE
                </Typography>
                <Typography variant="body1" sx={{ fontWeight: 500 }}>
                  {course.courseCode}
                </Typography>
              </Box>
              <Box>
                <Typography variant="caption" color="textSecondary" display="block">
                  CREDIT HOURS
                </Typography>
                <Typography variant="body1" sx={{ fontWeight: 500 }}>
                  {course.creditHours}
                </Typography>
              </Box>
            </Box>

            {course.description && (
              <Box sx={{ mb: 3 }}>
                <Typography variant="caption" color="textSecondary" display="block" gutterBottom>
                  DESCRIPTION
                </Typography>
                <Typography variant="body1" sx={{ lineHeight: 1.7, color: '#555' }}>
                  {course.description}
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
  );
};

export default CurrentCourseCard;
