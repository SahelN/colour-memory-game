#!/bin/bash

# Check if Docker is installed
if ! command -v docker &> /dev/null
then
    echo "❌ Docker could not be found. Please install Docker first."
    exit
fi

# Start MongoDB if not already running
if [ "$(docker ps -q -f name=mongodb)" ]; then
    echo "✅ MongoDB is already running."
else
    echo "🚀 Starting MongoDB Docker container..."
    docker start mongodb
fi

# Start backend
echo "🛠 Starting Backend API..."
cd backend
dotnet run &
cd ..

# Start frontend
echo "🎨 Starting Frontend React App..."
cd frontend
npm start
