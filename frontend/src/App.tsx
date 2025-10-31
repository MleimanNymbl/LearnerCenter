import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline, Box } from '@mui/material';
import SimpleHeader from './components/SimpleHeader';
import CampusSelector from './pages/CampusSelector';
import EnrollmentRegistration from './pages/EnrollmentRegistration';
import UserRegistration from './pages/UserRegistration';
import RegistrationSuccess from './pages/RegistrationSuccess';
import Login from './components/Login';
import SimpleDashboard from './components/SimpleDashboard';

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
      <Router>
        <Box sx={{ flexGrow: 1 }}>
          <Routes>
            {/* Pages without header - campus and enrollment flow */}
            <Route path="/" element={<CampusSelector />} />
            <Route path="/enrollment-registration" element={<EnrollmentRegistration />} />
            <Route path="/enrollment-registration/:campusId" element={<EnrollmentRegistration />} />
            <Route path="/user-registration" element={<UserRegistration />} />
            <Route path="/registration-success" element={<RegistrationSuccess />} />
            
            {/* Pages with header - authenticated pages */}
            <Route path="/login" element={
              <>
                <SimpleHeader />
                <Login />
              </>
            } />
            <Route path="/dashboard" element={
              <>
                <SimpleHeader />
                <SimpleDashboard />
              </>
            } />
            
            {/* Catch all other routes - redirect to home */}
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </Box>
      </Router>
    </ThemeProvider>
  );
}

export default App;
