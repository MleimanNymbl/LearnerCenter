import React from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Typography,
  Box,
  IconButton,
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  Chip,
  Divider,
  CircularProgress,
  Alert,
} from '@mui/material';
import { Close, MenuBook, Schedule } from '@mui/icons-material';

interface Course {
  courseId: string;
  courseCode: string;
  courseName: string;
  description: string;
  creditHours: number;
  isActive: boolean;
}

interface CourseDetailsModalProps {
  open: boolean;
  onClose: () => void;
  enrollmentName: string;
  enrollmentDescription: string;
  courses: Course[];
  loading: boolean;
  error: string | null;
}

const CourseDetailsModal: React.FC<CourseDetailsModalProps> = ({
  open,
  onClose,
  enrollmentName,
  enrollmentDescription,
  courses,
  loading,
  error
}) => {
  return (
    <Dialog 
      open={open} 
      onClose={onClose}
      maxWidth="md"
      fullWidth
    >
      <DialogTitle sx={{ 
        display: 'flex', 
        alignItems: 'center', 
        justifyContent: 'space-between',
        pb: 1
      }}>
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
          <MenuBook color="primary" />
          <Typography variant="h6">
            Course Details
          </Typography>
        </Box>
        <IconButton onClick={onClose} size="small">
          <Close />
        </IconButton>
      </DialogTitle>
      
      <DialogContent dividers>
        <Box sx={{ mb: 3 }}>
          <Typography variant="subtitle1" sx={{ fontWeight: 'bold', mb: 1 }}>
            {enrollmentName}
          </Typography>
          <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
            {enrollmentDescription}
          </Typography>
          <Chip 
            label={`${courses.length} Total Courses`} 
            color="primary" 
            variant="outlined" 
            size="small"
          />
        </Box>

        <Divider sx={{ mb: 2 }} />

        <Typography variant="h6" sx={{ mb: 2, display: 'flex', alignItems: 'center', gap: 1 }}>
          <Schedule color="primary" />
          Course Curriculum
        </Typography>

        {loading && (
          <Box sx={{ display: 'flex', justifyContent: 'center', py: 4 }}>
            <CircularProgress />
          </Box>
        )}

        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}

        {!loading && !error && courses.length === 0 && (
          <Alert severity="info">
            No courses found for this enrollment program.
          </Alert>
        )}

        {!loading && !error && courses.length > 0 && (
          <List sx={{ p: 0 }}>
            {courses.map((course, index) => (
              <ListItem
                key={course.courseId}
                sx={{
                  border: 1,
                  borderColor: 'divider',
                  borderRadius: 1,
                  mb: 1,
                  bgcolor: 'background.paper'
                }}
              >
                <ListItemIcon>
                  <Box
                    sx={{
                      width: 32,
                      height: 32,
                      borderRadius: '50%',
                      bgcolor: 'primary.main',
                      color: 'white',
                      display: 'flex',
                      alignItems: 'center',
                      justifyContent: 'center',
                      fontSize: '0.875rem',
                      fontWeight: 'bold'
                    }}
                  >
                    {index + 1}
                  </Box>
                </ListItemIcon>
                <ListItemText
                  primary={
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 0.5 }}>
                      <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>
                        {course.courseName}
                      </Typography>
                      <Chip 
                        label={course.courseCode} 
                        size="small" 
                        variant="outlined" 
                        sx={{ fontSize: '0.75rem' }}
                      />
                      <Chip 
                        label={`${course.creditHours} Credits`} 
                        size="small" 
                        color="secondary"
                        sx={{ fontSize: '0.75rem' }}
                      />
                    </Box>
                  }
                  secondary={
                    <Typography variant="body2" color="text.secondary">
                      {course.description}
                    </Typography>
                  }
                />
              </ListItem>
            ))}
          </List>
        )}
      </DialogContent>
      
      <DialogActions sx={{ p: 2 }}>
        <Button onClick={onClose} variant="outlined">
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CourseDetailsModal;