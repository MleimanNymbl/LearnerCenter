import { useAuth } from '../contexts/AuthContext';

interface Enrollment {
  enrollmentId: string;
  programName: string;
  degree?: string;
  description?: string;
  isActive: boolean;
  createdDate: string;
}

export const useEnrollment = (): Enrollment | null => {
  const { user } = useAuth();
  
  return user?.enrollment || null;
};
