#!/bin/bash
# Manual deployment script for EC2
# Usage: ./deploy-to-ec2.sh

set -e

echo "🚀 Starting deployment to EC2..."

# Pull latest changes
echo "📥 Pulling latest code from GitHub..."
git pull origin main

# Stop running containers
echo "🛑 Stopping current containers..."
docker compose down

# Rebuild and start containers
echo "🔨 Building and starting containers..."
docker compose up --build -d

# Wait for services to be healthy
echo "⏳ Waiting for services to start..."
sleep 10

# Show container status
echo "📊 Container status:"
docker compose ps

# Show recent logs
echo "📋 Recent logs:"
docker compose logs --tail=30

# Clean up unused images
echo "🧹 Cleaning up old Docker images..."
docker image prune -f

echo "✅ Deployment completed successfully!"
echo ""
echo "🌐 Access your application:"
echo "   Frontend: http://$(curl -s ifconfig.me):5173"
echo "   Backend:  http://$(curl -s ifconfig.me):5000"
echo "   Swagger:  http://$(curl -s ifconfig.me):5000/swagger"
