import { useState, useEffect, useRef } from 'react';
import { usersApi } from '../services/apiService';

interface AvailabilityResult {
  available: boolean;
  checked: boolean;
}

interface UseUserAvailabilityReturn {
  checkingAvailability: Record<string, boolean>;
  availabilityResults: Record<string, AvailabilityResult>;
  availabilityErrors: Record<string, string>;
}

export const useUserAvailability = (
  username: string,
  email: string,
  debounceMs: number = 300
): UseUserAvailabilityReturn => {
  const [checkingAvailability, setCheckingAvailability] = useState<Record<string, boolean>>({});
  const [availabilityResults, setAvailabilityResults] = useState<Record<string, AvailabilityResult>>({});
  const [availabilityErrors, setAvailabilityErrors] = useState<Record<string, string>>({});
  
  const controllerRef = useRef<AbortController | null>(null);

  useEffect(() => {
    // Cleanup previous controller
    if (controllerRef.current) {
      controllerRef.current.abort();
    }
    
    controllerRef.current = new AbortController();
    const controller = controllerRef.current;
    
    const checkAvailability = async (usernameValue: string, emailValue: string) => {
      // Reset previous results when checking new values
      setAvailabilityResults(prev => ({
        ...prev,
        username: { available: false, checked: false },
        email: { available: false, checked: false }
      }));
      
      const shouldCheckUsername = usernameValue.trim().length >= 3;
      const shouldCheckEmail = emailValue.trim().includes('@') && emailValue.trim().length >= 5;
      
      if (!shouldCheckUsername && !shouldCheckEmail) return;
      
      setCheckingAvailability({
        username: shouldCheckUsername,
        email: shouldCheckEmail
      });

      try {
        const response = await usersApi.checkUserExists(
          shouldCheckUsername ? usernameValue : '',
          shouldCheckEmail ? emailValue : ''
        );
        
        // Only update if this request wasn't cancelled
        if (!controller.signal.aborted) {
          const { usernameExists, emailExists } = response.data || { usernameExists: false, emailExists: false };
          
          setAvailabilityResults(prev => ({
            ...prev,
            username: { 
              available: shouldCheckUsername ? !usernameExists : true, 
              checked: shouldCheckUsername 
            },
            email: { 
              available: shouldCheckEmail ? !emailExists : true, 
              checked: shouldCheckEmail 
            }
          }));

          // Update availability errors
          setAvailabilityErrors(prev => {
            const newErrors = { ...prev };
            if (shouldCheckUsername) {
              if (usernameExists) {
                newErrors.username = 'Username is already taken';
              } else {
                delete newErrors.username;
              }
            }
            if (shouldCheckEmail) {
              if (emailExists) {
                newErrors.email = 'Email is already registered';
              } else {
                delete newErrors.email;
              }
            }
            return newErrors;
          });
        }
        
      } catch (error: any) {
        if (!controller.signal.aborted) {
          console.error('Error checking user availability:', error);
          // Set availability as unknown on error
          setAvailabilityResults(prev => ({
            ...prev,
            username: { available: false, checked: false },
            email: { available: false, checked: false }
          }));
        }
      } finally {
        if (!controller.signal.aborted) {
          setCheckingAvailability({
            username: false,
            email: false
          });
        }
      }
    };

    const timeoutId = setTimeout(() => {
      if (username.length >= 3 || (email.includes('@') && email.length >= 5)) {
        checkAvailability(username, email);
      }
    }, debounceMs);

    return () => {
      clearTimeout(timeoutId);
      controller.abort();
    };
  }, [username, email, debounceMs]);

  // Cleanup on unmount
  useEffect(() => {
    return () => {
      if (controllerRef.current) {
        controllerRef.current.abort();
      }
    };
  }, []);

  return {
    checkingAvailability,
    availabilityResults,
    availabilityErrors
  };
};