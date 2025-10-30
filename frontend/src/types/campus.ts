// Campus-related types
export interface Campus {
  campusId: string;
  campusName: string;
  campusCode?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  phoneNumber?: string;
  email?: string;
  isActive: boolean;
  createdDate: string;
}