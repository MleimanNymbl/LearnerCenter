import React, { useEffect, useState } from 'react';
import {
  Container,
  Paper,
  Typography,
  Card,
  CardContent,
  Box,
  CircularProgress,
  Alert,
} from '@mui/material';
import {
  School,
  People,
  Assignment,
  TrendingUp,
} from '@mui/icons-material';
import { useAuth } from '../contexts/AuthContext';
import { UserRole } from '../types';

interface DashboardStats {
  totalCourses: number;
  totalUsers: number;
  totalEnrollments: number;
  totalAssignments: number;
  userEnrollments?: number;
}

const Dashboard: React.FC = () => {
  const { user } = useAuth();
  const [stats, setStats] = useState<DashboardStats>({
    totalCourses: 0,
    totalUsers: 0,
    totalEnrollments: 0,
    totalAssignments: 0,
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchDashboardData = async () => {
      try {
        setLoading(true);
        
        // For now, use mock data since we don't have a backend yet
        setStats({
          totalCourses: user?.role === UserRole.Student ? 3 : 25,
          totalUsers: user?.role === UserRole.Admin ? 150 : 0,
          totalEnrollments: user?.role === UserRole.Admin ? 75 : 0,
          totalAssignments: 12,
          userEnrollments: user?.role === UserRole.Student ? 3 : undefined,
        });
      } catch (err: any) {
        setError('Failed to load dashboard data');
        console.error('Dashboard error:', err);
      } finally {
        setLoading(false);
      }
    };

    if (user) {
      fetchDashboardData();
    }
  }, [user]);

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
        <CircularProgress />
      </Box>
    );
  }

  const StatCard: React.FC<{
    title: string;
    value: number | undefined;
    subtitle: string;
    icon: React.ReactNode;
  }> = ({ title, value, subtitle, icon }) => (
    <Card sx={{ minWidth: 275, margin: 1, flex: '1 1 auto' }}>
      <CardContent>
        <Box display="flex" alignItems="center">
          {icon}
          <Box sx={{ ml: 2 }}>
            <Typography color="textSecondary" gutterBottom>
              {title}
            </Typography>
            <Typography variant="h4">
              {value || 0}
            </Typography>
            <Typography variant="body2" color="textSecondary">
              {subtitle}
            </Typography>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Typography variant="h4" gutterBottom>
        Welcome back, {user?.firstName}!
      </Typography>

      {error && (
        <Alert severity="error" sx={{ mb: 3 }}>
          {error}
        </Alert>
      )}

      <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 2, mb: 4 }}>
        <StatCard
          title="Courses"
          value={user?.role === UserRole.Student ? stats.userEnrollments : stats.totalCourses}
          subtitle={user?.role === UserRole.Student ? 'Enrolled' : 'Total Available'}
          icon={<School color="primary" sx={{ fontSize: 40 }} />}
        />

        {user?.role === UserRole.Admin && (
          <>
            <StatCard
              title="Users"
              value={stats.totalUsers}
              subtitle="Total Registered"
              icon={<People color="primary" sx={{ fontSize: 40 }} />}
            />
            <StatCard
              title="Enrollments"
              value={stats.totalEnrollments}
              subtitle="Active Enrollments"
              icon={<TrendingUp color="primary" sx={{ fontSize: 40 }} />}
            />
          </>
        )}

        <StatCard
          title="Assignments"
          value={stats.totalAssignments}
          subtitle="Total Available"
          icon={<Assignment color="primary" sx={{ fontSize: 40 }} />}
        />
      </Box>

      <Paper sx={{ p: 3 }}>
        <Typography variant="h6" gutterBottom>
          Quick Actions
        </Typography>
        <Typography variant="body1" color="textSecondary">
          {user?.role === UserRole.Student && 
            "Browse available courses, check your assignments, and track your progress."}
          {user?.role === UserRole.Instructor && 
            "Manage your courses, create assignments, and review student submissions."}
          {user?.role === UserRole.Admin && 
            "Oversee all courses, manage users, and monitor system-wide activity."}
        </Typography>
      </Paper>
    </Container>
  );
};

export default Dashboard;