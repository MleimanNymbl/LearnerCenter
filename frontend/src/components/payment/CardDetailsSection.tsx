import React from 'react';
import { Card, CardContent, Typography, Box, TextField } from '@mui/material';
import { CreditCard, Security } from '@mui/icons-material';
import { PaymentData } from '../../types/user';
import CardTypeIcon from './CardTypeIcon';

interface CardDetailsSectionProps {
    paymentData: PaymentData;
    paymentErrors: Record<string, string>;
    cardType: string;
    onInputChange: (field: keyof PaymentData, value: string) => void;
    isValidating: boolean;
}

const CardDetailsSection: React.FC<CardDetailsSectionProps> = ({
    paymentData,
    paymentErrors,
    cardType,
    onInputChange,
    isValidating
}) => {
    return (
        <Card>
            <CardContent>
                <Typography variant="h6" sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                    <CreditCard fontSize="small" sx={{ mr: 1 }} />
                    Card Details
                </Typography>
                <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                    <Box sx={{ position: 'relative' }}>
                        <TextField
                            fullWidth
                            label="Card Number"
                            value={paymentData.cardNumber}
                            onChange={(e) => onInputChange('cardNumber', e.target.value)}
                            error={!!paymentErrors.cardNumber}
                            helperText={paymentErrors.cardNumber}
                            placeholder="1234 5678 9012 3456"
                            inputProps={{ maxLength: 19 }}
                            disabled={isValidating}
                        />
                        {/* Detected Card Icon */}
                        {cardType && (
                            <Box sx={{
                                position: 'absolute',
                                right: 8,
                                top: '50%',
                                transform: 'translateY(-50%)',
                                transition: 'all 0.3s ease',
                                animation: 'fadeIn 0.3s ease'
                            }}>
                                <CardTypeIcon cardType={cardType} small />
                            </Box>
                        )}
                    </Box>
                    
                    <TextField
                        fullWidth
                        label="Cardholder Name"
                        value={paymentData.cardHolderName}
                        onChange={(e) => onInputChange('cardHolderName', e.target.value)}
                        error={!!paymentErrors.cardHolderName}
                        helperText={paymentErrors.cardHolderName}
                        disabled={isValidating}
                    />

                    <Box sx={{ display: 'flex', gap: 2 }}>
                        <Box sx={{ flex: 1 }}>
                            <TextField
                                fullWidth
                                label="Expiry Month"
                                value={paymentData.expiryMonth}
                                onChange={(e) => onInputChange('expiryMonth', e.target.value)}
                                error={!!paymentErrors.expiryMonth}
                                helperText={paymentErrors.expiryMonth}
                                placeholder="MM"
                                inputProps={{ maxLength: 2 }}
                                disabled={isValidating}
                            />
                        </Box>
                        <Box sx={{ flex: 1 }}>
                            <TextField
                                fullWidth
                                label="Expiry Year"
                                value={paymentData.expiryYear}
                                onChange={(e) => onInputChange('expiryYear', e.target.value)}
                                error={!!paymentErrors.expiryYear}
                                helperText={paymentErrors.expiryYear}
                                placeholder="YYYY"
                                inputProps={{ maxLength: 4 }}
                                disabled={isValidating}
                            />
                        </Box>
                        <Box sx={{ flex: 1 }}>
                            <TextField
                                fullWidth
                                label="CVV"
                                value={paymentData.cvv}
                                onChange={(e) => onInputChange('cvv', e.target.value)}
                                error={!!paymentErrors.cvv}
                                helperText={paymentErrors.cvv}
                                placeholder="123"
                                inputProps={{ maxLength: cardType === 'American Express' ? 4 : 3 }}
                                disabled={isValidating}
                                InputProps={{
                                    startAdornment: <Security fontSize="small" sx={{ mr: 1, color: 'text.secondary' }} />
                                }}
                            />
                        </Box>
                    </Box>
                </Box>
            </CardContent>
        </Card>
    );
};

export default CardDetailsSection;