// Course-related types
import { User } from './user';

export interface Course {
  id: number;
  title: string;
  description: string;
  instructorId: number;
  instructor?: User;
  duration: number; // in hours
  level: CourseLevel;
  category: string;
  isActive: boolean;
  enrollmentCount: number;
  createdAt: string;
  updatedAt: string;
}

export enum CourseLevel {
  Beginner = 'Beginner',
  Intermediate = 'Intermediate',
  Advanced = 'Advanced'
}