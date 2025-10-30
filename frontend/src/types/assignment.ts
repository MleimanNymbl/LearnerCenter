// Assignment and submission types
export interface Assignment {
  id: number;
  courseId: number;
  title: string;
  description: string;
  dueDate: string;
  maxPoints: number;
  isActive: boolean;
  createdAt: string;
}

export interface Submission {
  id: number;
  assignmentId: number;
  userId: number;
  content: string;
  submittedAt: string;
  grade?: number;
  feedback?: string;
  gradedAt?: string;
}