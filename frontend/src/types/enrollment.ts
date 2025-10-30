// Enrollment-related types
import { User } from './user';
import { Course } from './course';

export interface Enrollment {
  id: number;
  userId: number;
  courseId: number;
  user?: User;
  course?: Course;
  enrolledAt: string;
  completedAt?: string;
  progress: number; // percentage 0-100
  isActive: boolean;
}