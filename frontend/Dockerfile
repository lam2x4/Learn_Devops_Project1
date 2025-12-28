# Frontend Dockerfile (Vite + React)
# Build static assets, then serve via Nginx

FROM node:20-alpine AS build
WORKDIR /app

# Install deps
COPY package.json package-lock.json* yarn.lock* pnpm-lock.yaml* ./
RUN if [ -f package-lock.json ]; then npm ci; \
    elif [ -f yarn.lock ]; then yarn install --frozen-lockfile; \
    elif [ -f pnpm-lock.yaml ]; then npm i -g pnpm && pnpm i --frozen-lockfile; \
    else npm i; fi

# Copy source and build
COPY . .

# Accept API URL at build time
ARG VITE_API_URL
ENV VITE_API_URL=$VITE_API_URL

RUN npm run build

FROM nginx:alpine AS production
# Copy built assets
COPY --from=build /app/dist /usr/share/nginx/html
# SPA fallback config
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
