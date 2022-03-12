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
    "/api/Students/CreateQrCode",
    "/api/Students/Edit",
    "/api/Students/GetStudentById",
    "/api/Students/Upload",
    "/api/StudentExams/GetAllStudentExam",
    "/api/StudentExams",
    "/api/StudentExams/Create",
    "/api/StudentExams/Edit",
    "/api/StudentExams/GetStudentExamById",
    "/api/StudentExams/Upload",
    "/api/Setting/Update",
    "/api/Setting/GetSetting",
    "/api/SchedulingGenerate",
    "/api/Schedule/GetAll",
    "/api/Rooms/GetAll",
    "/api/Rooms",
    "/api/Rooms/Create",
    "/api/Rooms/Edit",
    "/api/Rooms/GetRoomById",
    "/api/Rooms/RoomsPagination",
    "/api/Checkin/CheckIn",
    "/api/Checkin/CheckInConfirm",
    "/api/CheckQr/GetQrCode",
    "/api/Schedule/GetByStudentId",
    "/api/Checkin/Excel",
    "/api/Checkin/Detail",
    "/api/Users/UsersPagination",
    "/api/Users",
    "/api/Users/CreateUser",
    "/api/Users/UpdateUser",
    "/api/Users/GetUserById",
    "/api/Users/Upload",
    "/api/Users/GetUserPersionalId"
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, { 
    target: target,
    secure: false, 
      headers: {
          Connection: 'keep-alive'
      }
  });

  app.use(appProxy);
};
