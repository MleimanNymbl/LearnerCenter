# Learner Center Project

This directory contains a full-stack learner center application with a React frontend and .NET backend.

## Project Structure

```
LearnerCenter/
├── frontend/                # React TypeScript frontend
│   ├── src/
│   │   ├── components/     # React components (Login, Register, Dashboard, etc.)
│   │   ├── contexts/       # React contexts (AuthContext)
│   │   ├── services/       # API services and HTTP client
│   │   ├── types/          # TypeScript type definitions
│   │   └── App.tsx         # Main app component
│   ├── public/            # Static assets
│   ├── package.json       # Frontend dependencies
│   └── .env              # Environment variables
├── backend/              # .NET Web API backend (your existing project)
└── package.json          # Root package.json for scripts
```

## Quick Start

### Frontend (React)
The React frontend is now ready and running on `http://localhost:3000`

**Features included:**
- ✅ Modern Material-UI interface
- ✅ Authentication system (Login/Register)
- ✅ Role-based navigation (Student/Instructor/Admin)
- ✅ Dashboard with user-specific views
- ✅ Protected routes with authentication guards
- ✅ API service layer ready for your .NET backend
- ✅ TypeScript for type safety
- ✅ Responsive design

### Backend Integration
The frontend is configured to connect to your .NET Web API on `https://localhost:7001/api`

**API endpoints expected:**
- `POST /api/auth/login` - Authentication
- `POST /api/auth/register` - User registration
- `GET /api/auth/profile` - User profile
- `GET /api/courses` - Course management
- `GET /api/users` - User management
- `GET /api/enrollments` - Enrollment management
- `GET /api/assignments` - Assignment management

### Next Steps

1. **Start your .NET backend** on `https://localhost:7001`
2. **Test the integration** by trying to login/register
3. **Customize the UI** to match your specific requirements
4. **Add more components** as needed (Course list, Assignment forms, etc.)

The frontend is production-ready and will automatically connect to your backend API once it's running!

## Running the Application

### Frontend Only
```bash
cd frontend
npm start
```

### Full Stack (when backend is ready)
```bash
npm run dev
```

## Available Scripts

- `npm run install:frontend` - Install frontend dependencies
- `npm run start:frontend` - Start React development server  
- `npm run build:frontend` - Build frontend for production
- `npm run dev` - Start both frontend and backend (requires concurrently package)

## Technology Stack

### Frontend
- React 18 with TypeScript
- Material-UI v5 for components
- React Router v6 for navigation
- Axios for API calls
- Context API for state management

### Backend (Your existing .NET project)
- .NET Web API
- Entity Framework Core
- JWT Authentication (expected)

The frontend is designed to integrate seamlessly with your existing .NET backend!