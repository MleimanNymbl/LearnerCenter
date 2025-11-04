import React from 'react';
import { Box } from '@mui/material';
import { 
  Visa, 
  Mastercard, 
  Amex, 
  Discover,
  Generic 
} from 'react-pay-icons';

interface CardTypeIconProps {
  cardType: string;
  small?: boolean;
}

const CardTypeIcon: React.FC<CardTypeIconProps> = ({ cardType, small = false }) => {
  const size = small ? 36 : 56;

  const getCardIcon = () => {
    switch (cardType) {
      case 'Visa':
        return <Visa style={{ width: size, height: 'auto' }} />;
      case 'Mastercard':
        return <Mastercard style={{ width: size, height: 'auto' }} />;
      case 'American Express':
        return <Amex style={{ width: size, height: 'auto' }} />;
      case 'Discover':
        return <Discover style={{ width: size, height: 'auto' }} />;
      default:
        return <Generic style={{ width: size, height: 'auto', opacity: 0.5 }} />;
    }
  };

  return (
    <Box sx={{ 
      display: 'flex', 
      alignItems: 'center', 
      justifyContent: 'center',
      height: small ? '40px' : '60px',
      width: 'fit-content',
      filter: 'drop-shadow(0 1px 2px rgba(0,0,0,0.1))'
    }}>
      {getCardIcon()}
    </Box>
  );
};

export default CardTypeIcon;