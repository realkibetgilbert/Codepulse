# Stage 1: Build the Angular app
FROM node:20 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install --legacy-peer-deps
COPY . .
RUN mkdir -p node_modules/parse5-html-rewriting-stream/node_modules/parse5/node_modules/entities/lib/generated \
    && cp node_modules/entities/lib/escape.js node_modules/parse5-html-rewriting-stream/node_modules/parse5/node_modules/entities/lib/escape.js || true \
    && cp node_modules/entities/lib/decode.js node_modules/parse5-html-rewriting-stream/node_modules/parse5/node_modules/entities/lib/decode.js || true \
    && cp -r node_modules/entities/lib/generated/* node_modules/parse5-html-rewriting-stream/node_modules/parse5/node_modules/entities/lib/generated/ || true \
    && cp node_modules/entities/lib/decode_codepoint.js node_modules/parse5-html-rewriting-stream/node_modules/parse5/node_modules/entities/lib/decode_codepoint.js || true
RUN npm run prepare
RUN npm run build --prod

# Stage 2: Serve with Nginx
FROM nginx:alpine
COPY --from=build /app/dist/* /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
