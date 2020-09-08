FROM node:14.3.0 AS compile-image

WORKDIR /app

COPY package.json ./
COPY package-lock.json ./

RUN npm install

ENV PATH="./node_modules/.bin:$PATH" 

COPY . ./ 
RUN ng build --prod

FROM nginx
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=compile-image /app/dist/client-app /usr/share/nginx/html