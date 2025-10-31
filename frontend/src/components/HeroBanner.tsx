import React from 'react';
import { Box, Typography } from '@mui/material';
import { School } from '@mui/icons-material';

interface HeroBannerProps {
  title: string;
  subtitle: string;
  icon?: React.ReactNode;
}

const HeroBanner: React.FC<HeroBannerProps> = ({ 
  title, 
  subtitle, 
  icon = <School sx={{ fontSize: 80, mb: 2, color: 'white' }} />
}) => {
  return (
    <Box
      sx={{
        width: '100%',
        height: 300,
        backgroundImage: 'url(https://images.unsplash.com/photo-1522202176988-66273c2fd55f?ixlib=rb-4.0.3&auto=format&fit=crop&w=1471&q=80)',
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        backgroundRepeat: 'no-repeat',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        position: 'relative',
        '&::before': {
          content: '""',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          backgroundColor: 'rgba(0, 0, 0, 0.4)',
          zIndex: 1,
        },
      }}
    >
      <Box
        sx={{
          textAlign: 'center',
          color: 'white',
          zIndex: 2,
          position: 'relative',
        }}
      >
        {icon}
        <Typography component="h1" variant="h2" gutterBottom fontWeight="bold">
          {title}
        </Typography>
        <Typography variant="h5" sx={{ opacity: 0.9 }}>
          {subtitle}
        </Typography>
      </Box>
    </Box>
  );
};

export default HeroBanner;