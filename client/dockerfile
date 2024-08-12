# Stage 1: Build the React app with Vite
FROM node:18 AS build

WORKDIR /app
COPY package.json yarn.lock ./

RUN npm install --global yarn --force
RUN yarn install

COPY . .

RUN npm run build

# Stage 2: Serve the app using nginx
FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]