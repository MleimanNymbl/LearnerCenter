import React from 'react';
import { Chip } from '@mui/material';

export type StatusType = 'Active' | 'Blocked' | 'In Progress' | 'Done' | 'Inactive';

interface StatusChipProps {
  status: StatusType;
  size?: 'small' | 'medium';
}

const StatusChip: React.FC<StatusChipProps> = ({ status, size = 'small' }) => {
  const getStatusConfig = (status: StatusType) => {
    switch (status) {
      case 'Active':
        return {
          label: 'Active',
          color: '#4caf50',
          backgroundColor: '#e8f5e9',
        };
      case 'Blocked':
        return {
          label: 'Blocked',
          color: '#f44336',
          backgroundColor: '#ffebee',
        };
      case 'In Progress':
        return {
          label: 'In Progress',
          color: '#ff9800',
          backgroundColor: '#fff3e0',
        };
      case 'Done':
        return {
          label: 'Done',
          color: '#2196f3',
          backgroundColor: '#e3f2fd',
        };
      case 'Inactive':
        return {
          label: 'Inactive',
          color: '#9e9e9e',
          backgroundColor: '#f5f5f5',
        };
      default:
        return {
          label: status,
          color: '#757575',
          backgroundColor: '#eeeeee',
        };
    }
  };

  const config = getStatusConfig(status);

  return (
    <Chip
      label={config.label}
      size={size}
      sx={{
        backgroundColor: config.backgroundColor,
        color: config.color,
        fontWeight: 600,
        border: 'none',
      }}
    />
  );
};

export default StatusChip;
