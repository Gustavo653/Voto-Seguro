FROM node:latest as builder
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

FROM nginx:alpine
COPY --from=builder /app/dist/sakai-ng /usr/share/nginx/html
EXPOSE 80

CMD ["sh", "-c", "envsubst '${API_URL}' < /usr/share/nginx/html/assets/json/runtime.template.json > /usr/share/nginx/html/assets/json/runtime.json && nginx -g 'daemon off;'"]
