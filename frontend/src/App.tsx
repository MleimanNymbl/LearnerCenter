import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline, Box } from '@mui/material';
import { AuthProvider } from './contexts/AuthContext';
import Header from './components/Header';
import SimpleHeader from './components/SimpleHeader';
import ProtectedRoute from './components/ProtectedRoute';
import CampusSelector from './pages/CampusSelector';
import EnrollmentRegistration from './pages/EnrollmentRegistration';
import UserRegistration from './pages/UserRegistration';
import RegistrationSuccess from './pages/RegistrationSuccess';
import Login from './components/Login';
import Dashboard from './components/Dashboard';

// Create Material-UI theme
const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
  },
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AuthProvider>
        <Router>
          <Box sx={{ flexGrow: 1 }}>
            <Routes>
              {/* Public pages without authentication - campus and enrollment flow */}
              <Route path="/" element={<CampusSelector />} />
              <Route path="/enrollment-registration" element={<EnrollmentRegistration />} />
              <Route path="/enrollment-registration/:campusId" element={<EnrollmentRegistration />} />
              <Route path="/user-registration" element={<UserRegistration />} />
              <Route path="/registration-success" element={<RegistrationSuccess />} />
              
              {/* Login page with simple header (no authentication required) */}
              <Route path="/login" element={
                <ProtectedRoute requireAuth={false}>
                  <SimpleHeader />
                  <Login />
                </ProtectedRoute>
              } />
              
              {/* Protected authenticated pages with full header */}
              <Route path="/dashboard" element={
                <ProtectedRoute requireAuth={true}>
                  <Header />
                  <Dashboard />
                </ProtectedRoute>
              } />
              
              {/* Catch all other routes - redirect to home */}
              <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
          </Box>
        </Router>
      </AuthProvider>
    </ThemeProvider>
  );
}

export default App;
