import React from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  IconButton,
  Divider,
} from '@mui/material';
import { CheckCircle, Assignment } from '@mui/icons-material';

const MyTasksCard: React.FC = () => {
  return (
    <Card sx={{ flex: 1 }}>
      <CardContent>
        <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
          <CheckCircle color="primary" sx={{ mr: 1 }} />
          <Typography variant="h6">My Tasks</Typography>
        </Box>
        <Divider sx={{ mb: 2 }} />
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', p: 1.5, backgroundColor: '#f5f5f5', borderRadius: 1 }}>
            <Box>
              <Typography variant="body1" sx={{ fontWeight: 500 }}>
                Complete Module 3 Quiz
              </Typography>
              <Typography variant="caption" color="textSecondary">
                Due in 2 days
              </Typography>
            </Box>
            <IconButton color="primary" onClick={() => {}}>
              <Assignment />
            </IconButton>
          </Box>
          <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', p: 1.5, backgroundColor: '#f5f5f5', borderRadius: 1 }}>
            <Box>
              <Typography variant="body1" sx={{ fontWeight: 500 }}>
                Submit Final Project Proposal
              </Typography>
              <Typography variant="caption" color="textSecondary">
                Due in 5 days
              </Typography>
            </Box>
            <IconButton color="primary" onClick={() => {}}>
              <Assignment />
            </IconButton>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};

export default MyTasksCard;
