// User-related types
export interface User {
  id: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  role: UserRole;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export enum UserRole {
  Student = 'Student',
  Instructor = 'Instructor',
  Admin = 'Admin'
}