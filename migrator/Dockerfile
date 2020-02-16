FROM node:13.8.0-alpine3.11 as base
WORKDIR /app

FROM base as base_with_packages
COPY package.json /app
COPY package-lock.json /app
RUN npm ci
COPY . /app

FROM base_with_packages as build_and_test
RUN npm run build

FROM build_and_test as runtime
RUN npm prune --production
EXPOSE 4000
USER 1000
CMD node build/index.js
