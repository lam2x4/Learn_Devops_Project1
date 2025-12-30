# Deployment Guide - Split Architecture

## Architecture Overview

```
┌──────────────────────┐
│  Frontend (Vercel)   │  ← React + Vite
│  https://your-app    │
│  .vercel.app         │
└──────────┬───────────┘
           │ HTTPS API Calls
           ↓
┌──────────────────────┐
│  Backend (EC2)       │  ← .NET 8 Web API
│  http://EC2_IP:5000  │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│  PostgreSQL (EC2)    │  ← Database (Container)
│  Internal: 5432      │
└──────────────────────┘
```

---

## Part 1: Deploy Backend + Database to EC2

### Step 1: EC2 Setup

#### 1.1 Create EC2 Instance
- **AMI**: Ubuntu Server 24.04 LTS
- **Instance Type**: `t3.small` (2GB RAM recommended for .NET 8)
- **Storage**: 20-30 GB gp3
- **Key Pair**: Create or use existing `.pem` file

#### 1.2 Security Group Configuration
| Type | Protocol | Port | Source | Purpose |
|------|----------|------|--------|---------|
| SSH | TCP | 22 | Your IP | Admin access |
| Custom TCP | TCP | 5000 | 0.0.0.0/0 | Backend API |
| Custom TCP | TCP | 3663 | Your IP | PostgreSQL (optional) |

#### 1.3 SSH into EC2
```bash
ssh -i "your-key.pem" ubuntu@YOUR_EC2_PUBLIC_IP
```

#### 1.4 Install Docker & Docker Compose
```bash
# Update system
sudo apt update && sudo apt upgrade -y

# Install Docker
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh

# Add user to docker group
sudo usermod -aG docker ubuntu
newgrp docker

# Verify installation
docker --version
docker compose version
```

### Step 2: GitHub Actions CI/CD

#### 2.1 Add GitHub Secrets
Go to: **GitHub Repo → Settings → Secrets and variables → Actions**

Add these secrets:

| Secret Name | Value | Example |
|------------|-------|---------|
| `EC2_HOST` | EC2 Public IP | `54.123.45.67` |
| `EC2_USER` | SSH username | `ubuntu` |
| `EC2_SSH_KEY` | Private key content | Full content of `.pem` file |

**Get SSH key content (Windows):**
```powershell
Get-Content "path\to\your-key.pem" | Set-Clipboard
# Then paste into GitHub secret
```

#### 2.2 Update Backend CORS for Vercel

The backend is already configured to accept Vercel domains. Update `appsettings.json`:

```json
{
  "AllowedOrigins": [
    "http://localhost:5173",
    "https://your-actual-app.vercel.app"
  ]
}
```

Replace `your-actual-app.vercel.app` with your real Vercel domain after deployment.

#### 2.3 Initial EC2 Deployment

SSH into EC2 and run:

```bash
# Clone repository
cd ~
git clone https://github.com/YOUR_USERNAME/Project-Devops.git
cd Project-Devops

# Run initial deployment
chmod +x deploy-to-ec2.sh
./deploy-to-ec2.sh
```

### Step 3: Verify Backend Deployment

- **API Endpoint**: `http://YOUR_EC2_IP:5000/api/todos`
- **Swagger**: `http://YOUR_EC2_IP:5000/swagger`
- **Health Check**:
  ```bash
  curl http://YOUR_EC2_IP:5000/api/todos
  ```

---

## Part 2: Deploy Frontend to Vercel

### Step 1: Connect GitHub to Vercel

1. Go to [vercel.com](https://vercel.com) and sign in with GitHub
2. Click **"Add New Project"**
3. Import your `Project-Devops` repository
4. Configure:
   - **Framework Preset**: Vite
   - **Root Directory**: `frontend`
   - **Build Command**: `npm run build`
   - **Output Directory**: `dist`

### Step 2: Set Environment Variables

In Vercel project settings → **Environment Variables**, add:

| Name | Value | Example |
|------|-------|---------|
| `VITE_API_URL` | Backend URL | `http://YOUR_EC2_IP:5000/api` |

**Important**: Replace `YOUR_EC2_IP` with your actual EC2 public IP.

### Step 3: Deploy

1. Click **"Deploy"**
2. Vercel will automatically:
   - Install dependencies (`npm install`)
   - Build the app (`npm run build`)
   - Deploy to CDN

### Step 4: Update Backend CORS

After Vercel deploys, you'll get a URL like: `https://project-devops-xyz.vercel.app`

**Update backend to allow this domain:**

**Option 1 - Via GitHub (Recommended):**

```powershell
# On Windows, edit API-Learn-Devops/API-Learn-Devops/appsettings.json
# Update AllowedOrigins with your Vercel domain

git add .
git commit -m "Update CORS for Vercel domain"
git push origin main
# GitHub Actions will auto-deploy to EC2
```

**Option 2 - Direct on EC2:**

```bash
ssh -i "your-key.pem" ubuntu@YOUR_EC2_IP
cd ~/Project-Devops
nano API-Learn-Devops/API-Learn-Devops/appsettings.json

# Update AllowedOrigins:
# "AllowedOrigins": [
#   "http://localhost:5173",
#   "https://project-devops-xyz.vercel.app"
# ]

docker compose restart backend
```

### Step 5: Enable Auto-Deploy

In Vercel project settings:
- **Git → Production Branch**: `main`
- ✅ Enable **"Automatic deployments"**

Now every push to `main` will:
1. Deploy backend to EC2 (via GitHub Actions)
2. Deploy frontend to Vercel (automatically)

---

## Deployment Workflow

### Automatic (Recommended)

```bash
# On Windows - make changes
git add .
git commit -m "Your changes"
git push origin main
```

Triggers:
1. ✅ GitHub Actions: Build & test backend
2. 🚀 GitHub Actions: Deploy backend to EC2
3. 🌐 Vercel: Deploy frontend

### Manual

**Backend (EC2):**
```bash
ssh -i "your-key.pem" ubuntu@YOUR_EC2_IP
cd ~/Project-Devops
./deploy-to-ec2.sh
```

**Frontend (Vercel):**
- Push to GitHub, or click **"Redeploy"** in Vercel dashboard

---

## Monitoring & Debugging

### Backend Logs (EC2)
```bash
docker compose logs -f             # All services
docker compose logs -f backend     # Backend only
docker compose logs -f postgres    # Database only
```

### Frontend Logs (Vercel)
Vercel dashboard → Deployments → Click deployment → Logs

### Container Status
```bash
docker compose ps
docker compose top
```

---

## Troubleshooting

### CORS Errors

**Symptom**: `Access blocked by CORS policy`

**Fix**:
1. Verify Vercel domain in `appsettings.json` → `AllowedOrigins`
2. Restart: `docker compose restart backend`
3. Must include `https://` prefix

### API Connection Refused

**Fix**:
1. Check EC2 security group allows port 5000
2. Verify backend running: `docker compose ps`
3. Test: `http://YOUR_EC2_IP:5000/api/todos`
4. Check `VITE_API_URL` in Vercel env vars

### Database Connection Failed

```bash
docker compose logs postgres
docker compose restart postgres
```

---

## Access URLs

| Service | URL |
|---------|-----|
| Frontend | `https://your-app.vercel.app` |
| Backend API | `http://YOUR_EC2_IP:5000/api/todos` |
| Swagger | `http://YOUR_EC2_IP:5000/swagger` |

---

## Security Checklist

- ✅ Change default DB password (not `123`)
- ✅ Restrict SSH to your IP only
- ✅ Use environment variables for secrets
- ✅ Enable HTTPS for backend (reverse proxy)
- ✅ Keep packages updated
