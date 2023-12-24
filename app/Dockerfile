FROM node:20-buster as build
WORKDIR /app
COPY package.json yarn.lock ./
RUN yarn
COPY . .
RUN yarn build:prod

FROM nginx:stable-alpine-slim
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist /usr/share/nginx/html
RUN chown nginx.nginx /usr/share/nginx/html/ -R
