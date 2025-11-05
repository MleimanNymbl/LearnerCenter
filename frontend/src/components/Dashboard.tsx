import React from 'react';
import {
  Container,
  Paper,
  Typography,
  Box,
  Card,
  CardContent,
  Button,
} from '@mui/material';
import { School, Assignment, CalendarToday, Person } from '@mui/icons-material';
import { useAuth } from '../contexts/AuthContext';

const Dashboard: React.FC = () => {
  const { user } = useAuth();
  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      {/* Header */}
      <Box sx={{ mb: 4 }}>
        <Typography variant="h4" gutterBottom>
          {user?.role || 'Student'} Dashboard
        </Typography>
        <Typography variant="body1" color="textSecondary">
          Welcome back, {user?.firstName || user?.username || 'Student'}! Here's an overview of your academic progress.
        </Typography>
      </Box>

      {/* Quick Stats */}
      <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 3, mb: 4 }}>
        <Card sx={{ minWidth: 200, flex: 1 }}>
          <CardContent>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <School color="primary" sx={{ mr: 1 }} />
              <Typography variant="h6">Enrolled Courses</Typography>
            </Box>
            <Typography variant="h4">4</Typography>
            <Typography variant="body2" color="textSecondary">
              Active this semester
            </Typography>
          </CardContent>
        </Card>

        <Card sx={{ minWidth: 200, flex: 1 }}>
          <CardContent>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <Assignment color="primary" sx={{ mr: 1 }} />
              <Typography variant="h6">Assignments</Typography>
            </Box>
            <Typography variant="h4">12</Typography>
            <Typography variant="body2" color="textSecondary">
              Due this week
            </Typography>
          </CardContent>
        </Card>

        <Card sx={{ minWidth: 200, flex: 1 }}>
          <CardContent>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <CalendarToday color="primary" sx={{ mr: 1 }} />
              <Typography variant="h6">Next Class</Typography>
            </Box>
            <Typography variant="h6">Today</Typography>
            <Typography variant="body2" color="textSecondary">
              Mathematics 101 at 2:00 PM
            </Typography>
          </CardContent>
        </Card>
      </Box>

      {/* Recent Activity */}
      <Paper sx={{ p: 3, mb: 3 }}>
        <Typography variant="h5" gutterBottom>
          Recent Activity
        </Typography>
        <Box sx={{ mt: 2 }}>
          <Typography variant="body1" sx={{ mb: 1 }}>
            📚 New assignment posted in English Literature
          </Typography>
          <Typography variant="body1" sx={{ mb: 1 }}>
            ✅ Quiz completed in Biology 101 - Score: 95%
          </Typography>
          <Typography variant="body1" sx={{ mb: 1 }}>
            📅 Upcoming midterm in Mathematics 101 - March 15th
          </Typography>
        </Box>
      </Paper>

      {/* Quick Actions */}
      <Paper sx={{ p: 3 }}>
        <Typography variant="h5" gutterBottom>
          Quick Actions
        </Typography>
        <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 2, mt: 2 }}>
          <Button variant="contained" startIcon={<School />}>
            View All Courses
          </Button>
          <Button variant="outlined" startIcon={<Assignment />}>
            Check Assignments
          </Button>
          <Button variant="outlined" startIcon={<CalendarToday />}>
            View Schedule
          </Button>
          <Button variant="outlined" startIcon={<Person />}>
            Update Profile
          </Button>
        </Box>
      </Paper>
    </Container>
  );
};

export default Dashboard;