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
  // Extended properties for frontend use
  role?: UserRole;
  firstName?: string;
  lastName?: string;
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

// Separate type for demo payment data (not persisted)
export interface PaymentData {
  cardNumber: string;
  expiryMonth: string;
  expiryYear: string;
  cvv: string;
  cardHolderName: string;
  billingAddress: string;
  billingCity: string;
  billingState: string;
  billingZipCode: string;
}

export enum UserRole {
  Student = 'Student',
  Instructor = 'Instructor',
  Admin = 'Admin'
}