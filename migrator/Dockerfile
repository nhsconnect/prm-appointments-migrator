FROM node:10.15-alpine as base
WORKDIR /stubs
RUN apk update && apk add bash
RUN echo "PS1='🚧 \[\033[01;32m\]stubs\[\033[01;34m\] \w \$\[\033[00m\] '" > /root/.bashrc

FROM base as base_with_node_modules
COPY package.json /stubs
COPY package-lock.json /stubs
RUN npm ci
COPY . /stubs

FROM base_with_node_modules as build_and_test
RUN npm run build
RUN npm run test:no-watch
RUN npm run lint

From build_and_test as runtime
RUN npm prune --production
EXPOSE 4010
USER 1000
ENTRYPOINT exec node_modules/.bin/nodemon build/index.js
