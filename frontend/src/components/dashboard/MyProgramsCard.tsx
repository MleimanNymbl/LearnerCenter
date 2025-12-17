import React from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Divider,
} from '@mui/material';
import { Assignment } from '@mui/icons-material';
import StatusChip from '../common/StatusChip';

interface Enrollment {
  enrollmentId: string;
  programName: string;
  degree?: string;
  createdDate: string;
}

interface MyProgramsCardProps {
  enrollment: Enrollment | null;
}

const MyProgramsCard: React.FC<MyProgramsCardProps> = ({ enrollment }) => {
  return (
    <Card sx={{ flex: 1 }}>
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
                  ? new Date(enrollment.createdDate).toLocaleDateString()
                  : 'N/A'}
              </Typography>
            </Box>
          </Box>
        ) : (
          <Typography color="textSecondary">No enrollment information available.</Typography>
        )}
      </CardContent>
    </Card>
  );
};

export default MyProgramsCard;
