const createProxyMiddleware = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:29836';

const context = [
    "/weatherforecast", 
    "/bff", 
    "/signin-oidc",  
    "/signout-callback-oidc", 
    "/api/Exams/GetAllExam", 
    "/api/Exams", 
    "/api/Exams/Create", 
    "/api/Exams/Edit", 
    "/api/Exams/GetExamById", 
    "/api/Exams/Upload",
    "/api/Students/ListAllStudent",
    "/api/Students",
    "/api/Students/Create",
    "/api/Students/CreateQrCode"
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, { 
    target: target,
    secure: false
  });

  app.use(appProxy);
};
