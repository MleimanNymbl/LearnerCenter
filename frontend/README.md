# Learner Center Frontend

A modern React TypeScript application built with Material-UI for the Learner Center platform.

## Features

- 🔐 **Authentication & Authorization** - Login, registration, and role-based access control
- 🎨 **Material-UI Design System** - Modern, responsive UI components
- 📱 **Responsive Design** - Works on desktop, tablet, and mobile devices
- 🔗 **API Integration** - Ready to connect with .NET Web API backend
- 🛣️ **Routing** - React Router for client-side navigation
- 🔒 **Protected Routes** - Role-based route protection
- 📊 **Dashboard** - User-specific dashboard with statistics
- 🎯 **TypeScript** - Type-safe development experience

## Tech Stack

- **React 18** - Modern React with TypeScript
- **Material-UI v5** - Component library and design system
- **React Router v6** - Client-side routing
- **Axios** - HTTP client for API calls
- **TypeScript** - Type safety and better developer experience

## Project Structure

```
src/
├── components/          # React components
│   ├── Dashboard.tsx    # Main dashboard component
│   ├── Header.tsx       # Navigation header
│   ├── Login.tsx        # Login form
│   ├── Register.tsx     # Registration form
│   └── ProtectedRoute.tsx # Route protection component
├── contexts/           # React contexts
│   └── AuthContext.tsx # Authentication context
├── services/          # API services
│   ├── api.ts         # Axios configuration
│   └── apiService.ts  # API endpoints
├── types/            # TypeScript type definitions
│   └── index.ts      # Common types and interfaces
└── App.tsx          # Main app component
```

## API Integration

The app is configured to work with a .NET Web API backend. Expected endpoints:

### Authentication Endpoints
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `POST /api/auth/logout` - User logout
- `GET /api/auth/profile` - Get user profile

### Core Endpoints
- `/api/users` - User management
- `/api/courses` - Course management
- `/api/enrollments` - Enrollment management
- `/api/assignments` - Assignment management
- `/api/submissions` - Submission management

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).
