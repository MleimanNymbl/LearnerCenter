# Azure FREE Tier Deployment Plan for LearnerCenter

## Architecture Overview (100% FREE)
- **Frontend**: Azure Static Web Apps (FREE tier)
- **Backend**: Azure App Service F1 (FREE tier)  
- **Database**: SQLite file database (FREE)
- **Monitoring**: Basic Azure monitoring (FREE)

## FREE Tier Benefits
- **Static Web Apps**: FREE tier includes:
  - 100GB bandwidth/month
  - Custom authentication
  - Staging environments
  - `.azurewebsites.net` domain
- **App Service F1**: FREE tier includes:
  - 1GB storage
  - 165MB RAM
  - 60 CPU minutes/day
  - Auto-sleep after 20 minutes (wakes on request)

## Database Strategy
Instead of Azure SQL Database ($5+/month), we'll use SQLite:
- File-based database stored with the backend
- No separate database service needed
- Perfect for 4-user application
- Zero additional cost

## Deployment Steps

### Step 1: Modify Backend for SQLite
- Change connection string to SQLite
- Update Entity Framework to use SQLite provider
- Create production migration

### Step 2: Create Azure Resources
- Create App Service (F1 Free tier)
- Create Static Web App (Free tier)
- Configure CORS between services

### Step 3: Deploy Backend to App Service
- Build and package .NET application
- Deploy via Visual Studio Code Azure extension
- Configure environment variables

### Step 4: Deploy Frontend to Static Web Apps  
- Build React production bundle
- Deploy via GitHub Actions integration
- Configure API routes to backend

## Estimated Timeline: 20-30 minutes
## Estimated Cost: $0/month

## Limitations to Consider
- **App Sleep**: Backend sleeps after 20 minutes of inactivity (2-3 second wake-up time)
- **CPU Quota**: 60 minutes of CPU time per day (resets daily)
- **Custom Domain**: Requires paid tier for custom domains
- **Performance**: Free tier has limited resources but sufficient for 4 users

## Next Steps
1. Modify backend to use SQLite
2. Create Azure resources
3. Deploy backend and frontend
4. Test the deployment

Ready to proceed?