// Enrollment-related types

export interface Enrollment {
  enrollmentId: string;
  name: string;
  description: string;
  programType: string;
  durationWeeks: number;
  cost: number;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
  
  // Campus information
  campusId: string;
  campusName: string;
  campusLocation: string;
  
  // Course count for this enrollment
  courseCount: number;
}