using './main.bicep'

param baseName = 'learnercenter'
param location = 'centralus'
param environment = 'prod'
param postgresqlConnectionString = 'postgresql://postgres.hpqliofwxllnsoaqaolv:Password123!@aws-1-us-east-2.pooler.supabase.com:5432/postgres'
