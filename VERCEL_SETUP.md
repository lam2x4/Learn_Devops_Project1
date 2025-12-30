# Vercel Deployment Quick Guide

## Setup Steps

### 1. Connect Repository to Vercel

1. Visit [vercel.com](https://vercel.com)
2. Sign in with GitHub
3. Click **"Add New Project"**
4. Select `Project-Devops` repository
5. Configure:
   - **Framework**: Vite
   - **Root Directory**: `frontend`
   - **Build Command**: `npm run build`
   - **Output Directory**: `dist`

### 2. Environment Variables

Add in Vercel project → Settings → Environment Variables:

| Variable | Value | Note |
|----------|-------|------|
| `VITE_API_URL` | `http://YOUR_EC2_IP:5000/api` | Replace with actual EC2 IP |

**Example:**
```
VITE_API_URL = http://54.123.45.67:5000/api
```

### 3. Deploy

Click **Deploy** button. Vercel will:
- Install dependencies
- Build production bundle
- Deploy to global CDN

### 4. Get Vercel URL

After deployment, you'll receive a URL like:
```
https://project-devops-abc123.vercel.app
```

### 5. Update Backend CORS

**On Windows**, edit `API-Learn-Devops/API-Learn-Devops/appsettings.json`:

```json
{
  "AllowedOrigins": [
    "http://localhost:5173",
    "https://project-devops-abc123.vercel.app"
  ]
}
```

**Push changes:**
```powershell
git add .
git commit -m "Add Vercel domain to CORS"
git push origin main
```

GitHub Actions will auto-deploy backend with updated CORS.

### 6. Enable Auto-Deployment

In Vercel Settings → Git:
- Production Branch: `main`
- ✅ Automatic deployments

Now every push to `main` triggers automatic Vercel deployment.

---

## Testing

1. Open your Vercel URL: `https://your-app.vercel.app`
2. Check browser console (F12) for errors
3. Verify API calls to EC2 backend work
4. Test CRUD operations (Create, Read, Update, Delete todos)

---

## Troubleshooting

### Build Fails

**Check Vercel build logs:**
- Dashboard → Deployments → Failed deployment → View logs

**Common issues:**
- Missing dependencies: Commit `package-lock.json`
- Build errors: Run `npm run build` locally first
- Wrong Node version: Add `.nvmrc` with version `20`

### CORS Errors

**Symptom:** `Access to fetch at '...' blocked by CORS`

**Fix:**
1. Verify backend `AllowedOrigins` includes your Vercel domain
2. Must use `https://` (not `http://`)
3. Restart backend: `docker compose restart backend`

### API Not Found (404)

**Check:**
- `VITE_API_URL` in Vercel env vars is correct
- EC2 security group allows port 5000 from `0.0.0.0/0`
- Backend is running: `docker compose ps`

---

## Custom Domain (Optional)

### Add Custom Domain

1. Vercel → Settings → Domains → Add Domain
2. Enter your domain: `www.yourdomain.com`
3. Configure DNS (see Vercel instructions)
4. Update backend CORS:
   ```json
   "AllowedOrigins": [
     "https://www.yourdomain.com",
     "https://yourdomain.com"
   ]
   ```

---

## Monitoring

### View Deployment Logs
Vercel Dashboard → Deployments → Click deployment → Logs

### Analytics (Pro Plan)
Vercel → Analytics → View page performance, visitors, etc.

---

## Redeploy

### Automatic
Push to `main` branch → Auto-deploys

### Manual
Vercel Dashboard → Deployments → Latest → **"Redeploy"**

---

## Cost

- **Hobby Plan**: Free
  - 100 GB bandwidth/month
  - 6,000 build minutes/month
  - Perfect for personal projects
- **Pro Plan**: $20/month
  - Unlimited bandwidth
  - Analytics, custom domains

---

## Useful Commands

```bash
# Install Vercel CLI (optional)
npm i -g vercel

# Deploy from command line
vercel --prod

# View deployment logs
vercel logs
```
