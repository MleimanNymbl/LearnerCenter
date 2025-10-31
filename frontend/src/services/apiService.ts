import { api } from './api';
import type {
  User,
  Course,
  Enrollment,
  Assignment,
  Submission,
  LoginRequest,
  LoginResponse,
  RegisterRequest,
  ApiResponse,
  PaginatedResponse,
  FilterParams,
  Campus,
} from '../types';

// Authentication API
export const authApi = {
  login: (credentials: LoginRequest) =>
    api.post<ApiResponse<LoginResponse>>('/auth/login', credentials),
  
  register: (userData: RegisterRequest) =>
    api.post<ApiResponse<User>>('/auth/register', userData),
  
  logout: () =>
    api.post('/auth/logout'),
  
  refreshToken: () =>
    api.post<ApiResponse<LoginResponse>>('/auth/refresh'),
  
  getProfile: () =>
    api.get<ApiResponse<User>>('/auth/profile'),
};

// Users API
export const usersApi = {
  getUsers: (params?: FilterParams) =>
    api.get<ApiResponse<PaginatedResponse<User>>>('/users', { params }),
  
  getUser: (id: number) =>
    api.get<ApiResponse<User>>(`/users/${id}`),
  
  updateUser: (id: number, userData: Partial<User>) =>
    api.put<ApiResponse<User>>(`/users/${id}`, userData),
  
  deleteUser: (id: number) =>
    api.delete<ApiResponse<void>>(`/users/${id}`),
  
  activateUser: (id: number) =>
    api.patch<ApiResponse<User>>(`/users/${id}/activate`),
  
  deactivateUser: (id: number) =>
    api.patch<ApiResponse<User>>(`/users/${id}/deactivate`),
};

// Courses API
export const coursesApi = {
  getCourses: (params?: FilterParams) =>
    api.get<ApiResponse<PaginatedResponse<Course>>>('/courses', { params }),
  
  getCourse: (id: number) =>
    api.get<ApiResponse<Course>>(`/courses/${id}`),
  
  getCoursesByEnrollment: (enrollmentId: string) =>
    api.get<any[]>(`/Course/enrollment/${enrollmentId}`),
  
  createCourse: (courseData: Omit<Course, 'id' | 'createdAt' | 'updatedAt'>) =>
    api.post<ApiResponse<Course>>('/courses', courseData),
  
  updateCourse: (id: number, courseData: Partial<Course>) =>
    api.put<ApiResponse<Course>>(`/courses/${id}`, courseData),
  
  deleteCourse: (id: number) =>
    api.delete<ApiResponse<void>>(`/courses/${id}`),
  
  getEnrollments: (courseId: number) =>
    api.get<ApiResponse<Enrollment[]>>(`/courses/${courseId}/enrollments`),
  
  getAssignments: (courseId: number) =>
    api.get<ApiResponse<Assignment[]>>(`/courses/${courseId}/assignments`),
};

// Enrollments API
export const enrollmentsApi = {
  getEnrollments: (params?: FilterParams) =>
    api.get<ApiResponse<PaginatedResponse<Enrollment>>>('/enrollments', { params }),
  
  getEnrollment: (id: number) =>
    api.get<ApiResponse<Enrollment>>(`/enrollments/${id}`),
  
  createEnrollment: (enrollmentData: { userId: number; courseId: number }) =>
    api.post<ApiResponse<Enrollment>>('/enrollments', enrollmentData),
  
  updateProgress: (id: number, progress: number) =>
    api.patch<ApiResponse<Enrollment>>(`/enrollments/${id}/progress`, { progress }),
  
  completeEnrollment: (id: number) =>
    api.patch<ApiResponse<Enrollment>>(`/enrollments/${id}/complete`),
  
  cancelEnrollment: (id: number) =>
    api.delete<ApiResponse<void>>(`/enrollments/${id}`),
  
  getUserEnrollments: (userId: number) =>
    api.get<ApiResponse<Enrollment[]>>(`/users/${userId}/enrollments`),
};

// Assignments API
export const assignmentsApi = {
  getAssignments: (params?: FilterParams) =>
    api.get<ApiResponse<PaginatedResponse<Assignment>>>('/assignments', { params }),
  
  getAssignment: (id: number) =>
    api.get<ApiResponse<Assignment>>(`/assignments/${id}`),
  
  createAssignment: (assignmentData: Omit<Assignment, 'id' | 'createdAt'>) =>
    api.post<ApiResponse<Assignment>>('/assignments', assignmentData),
  
  updateAssignment: (id: number, assignmentData: Partial<Assignment>) =>
    api.put<ApiResponse<Assignment>>(`/assignments/${id}`, assignmentData),
  
  deleteAssignment: (id: number) =>
    api.delete<ApiResponse<void>>(`/assignments/${id}`),
  
  getSubmissions: (assignmentId: number) =>
    api.get<ApiResponse<Submission[]>>(`/assignments/${assignmentId}/submissions`),
};

// Submissions API
export const submissionsApi = {
  getSubmissions: (params?: FilterParams) =>
    api.get<ApiResponse<PaginatedResponse<Submission>>>('/submissions', { params }),
  
  getSubmission: (id: number) =>
    api.get<ApiResponse<Submission>>(`/submissions/${id}`),
  
  createSubmission: (submissionData: Omit<Submission, 'id' | 'submittedAt' | 'gradedAt'>) =>
    api.post<ApiResponse<Submission>>('/submissions', submissionData),
  
  updateSubmission: (id: number, submissionData: Partial<Submission>) =>
    api.put<ApiResponse<Submission>>(`/submissions/${id}`, submissionData),
  
  gradeSubmission: (id: number, gradeData: { grade: number; feedback?: string }) =>
    api.patch<ApiResponse<Submission>>(`/submissions/${id}/grade`, gradeData),
  
  getUserSubmissions: (userId: number, assignmentId?: number) =>
    api.get<ApiResponse<Submission[]>>(`/users/${userId}/submissions`, {
      params: assignmentId ? { assignmentId } : undefined,
    }),
};

// Campus API
export const campusApi = {
  getAllCampuses: async () => {
    const response = await api.get<Campus[]>('/Campus');
    return response;
  },
  
  getCampusById: async (campusId: string) => {
    const response = await api.get<Campus>(`/Campus/${campusId}`);
    return response;
  },
};

// Enrollment API
export const enrollmentApi = {
  getAllEnrollments: async () => {
    const response = await api.get<Enrollment[]>('/Enrollment');
    return response;
  },
  
  getEnrollmentById: async (enrollmentId: string) => {
    const response = await api.get<Enrollment>(`/Enrollment/${enrollmentId}`);
    return response;
  },
  
  getEnrollmentsByCampus: async (campusId: string) => {
    const response = await api.get<Enrollment[]>(`/Enrollment/campus/${campusId}`);
    return response;
  },
};