// User-related types
export interface User {
  userId: string;
  username: string;
  email: string;
  passwordHash: string;
  createdDate: string;
  lastLoginDate?: string;
  status: string;
  isActive: boolean;
  enrollmentId?: string;
}

export interface UserProfile {
  userProfileId: string;
  userId: string;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  dateOfBirth?: string;
  gender?: string;
  emergencyContactName?: string;
  emergencyContactPhone?: string;
  createdDate: string;
  updatedDate?: string;
}

export interface UserRegistrationData {
  // User fields
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
  enrollmentId: string;
  
  // UserProfile fields
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  dateOfBirth?: string;
  gender?: string;
  emergencyContactName?: string;
  emergencyContactPhone?: string;
}

export enum UserRole {
  Student = 'Student',
  Instructor = 'Instructor',
  Admin = 'Admin'
}