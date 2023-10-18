#Base image (Node.js is a JavaScript runtime)
FROM node:16

#Install angular CLI in the container
RUN npm install -g @angular/cli

#Working directory
WORKDIR /app

#Copy the 2 json for the information dependencies
COPY package*.json ./

#Copy all the files
COPY . .

#Install dependencies
RUN npm install

#Build angular for production
RUN ng build --configuration=production

#Exposes port 4200 for the container
EXPOSE 4200

CMD ["ng", "serve", "--port", "4200", "--host", "0.0.0.0"]
