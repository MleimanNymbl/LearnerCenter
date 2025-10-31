import React from 'react';
import { Paper, Box, Typography } from '@mui/material';
import { School } from '@mui/icons-material';
import { Campus } from '../types';

interface CampusInfoBannerProps {
  campus: Campus;
  showIcon?: boolean;
}

const CampusInfoBanner: React.FC<CampusInfoBannerProps> = ({ campus, showIcon = true }) => {
  return (
    <Paper elevation={2} sx={{ p: 3, mb: 4, bgcolor: 'primary.main', color: 'white' }}>
      <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
        {showIcon && <School fontSize="large" />}
        <Box>
          <Typography variant="h5" sx={{ fontWeight: 'bold' }}>
            {campus.campusName}
          </Typography>
          <Typography variant="body1">
            {campus.city}, {campus.state} • {campus.zipCode}
          </Typography>
          {campus.address && (
            <Typography variant="body2" sx={{ opacity: 0.9, mt: 0.5 }}>
              {campus.address}
            </Typography>
          )}
        </Box>
      </Box>
    </Paper>
  );
};

export default CampusInfoBanner;