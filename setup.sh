#!/bin/bash

# 1. Install Entity Framework Tool (if missing)
echo "--- Checking dotnet-ef tool ---"
dotnet tool install --global dotnet-ef 2>/dev/null
echo "Done."

# 2. Apply Migrations and Update Database
echo "--- Updating Database ---"
# Navigate to API folder to run EF commands conveniently or use --project
# Using --project from root:
dotnet ef migrations add FinalUpdates --project src/MyMEDIA/MyMEDIA.API --startup-project src/MyMEDIA/MyMEDIA.API
dotnet ef database update --project src/MyMEDIA/MyMEDIA.API --startup-project src/MyMEDIA/MyMEDIA.API
echo "Database updated."

# 3. Instructions to Run
echo ""
echo "--- SETUP COMPLETE ---"
echo "To run the application, you need TWO terminals:"
echo ""
echo "Terminal 1 (Backend API):"
echo "dotnet run --project src/MyMEDIA/MyMEDIA.API"
echo ""
echo "Terminal 2 (Frontend Web):"
echo "dotnet run --project src/MyMEDIA/MyMEDIA.Web"
echo ""
