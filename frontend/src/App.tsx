import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline, Box } from '@mui/material';
import SimpleHeader from './components/SimpleHeader';
import CampusSelector from './components/CampusSelector';
import EnrollmentRegistration from './components/EnrollmentRegistration';
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
          <SimpleHeader />
          <Routes>
            {/* Campus selector - landing page */}
            <Route path="/" element={<CampusSelector />} />
            
            {/* Enrollment registration page */}
            <Route path="/enrollment-registration" element={<EnrollmentRegistration />} />
            
            {/* Login page */}
            <Route path="/login" element={<Login />} />
            
            {/* Simple dashboard after login */}
            <Route path="/dashboard" element={<SimpleDashboard />} />
            
            {/* Catch all other routes - redirect to home */}
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </Box>
      </Router>
    </ThemeProvider>
  );
}

export default App;
