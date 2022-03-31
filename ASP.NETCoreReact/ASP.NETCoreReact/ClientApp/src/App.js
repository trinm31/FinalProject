import React, {useEffect} from "react";
import {
    Switch,
    Route
} from "react-router-dom";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { useDispatch } from "react-redux";
import { currentUser }               from "./functions/auth";
import AdminRoute from "./components/routes/AdminRoute"
import ManagementRoute from "./components/routes/ManagementRoute"
import PrivateRoute from "./components/routes/PrivateRoute"

import CreateCourse    from "./pages/Admin/Courses/CreateCourse";
import EditCourse      from "./pages/Admin/Courses/EditCourse";
import AllCourse  from "./pages/Admin/Courses/AllCourse";
import UploadFile        from "./pages/Admin/Courses/UploadFile";
import AllStudents           from "./pages/Admin/Students/AllStudents";
import CreateStudent         from "./pages/Admin/Students/CreateStudent";
import EditStudent           from "./pages/Admin/Students/EditStudent";
import UploadStudentFile     from "./pages/Admin/Students/UploadStudentFile";
import AllStudentExams       from "./pages/Admin/StudentExams/AllStudentExams";
import CreateStudentExam     from "./pages/Admin/StudentExams/CreateStudentExam";
import EditStudentExam       from "./pages/Admin/StudentExams/EditStudentExam";
import UploadStudentExamFile from "./pages/Admin/StudentExams/UploadStudentExamFile";
import AllSetting      from "./pages/Admin/Settings/AllSetting";
import EditSetting     from "./pages/Admin/Settings/EditSetting";
import SchedulePage    from "./pages/Admin/Schedules/";
import Home            from "./components/Home";
import AllRooms        from "./pages/Admin/Rooms/AllRooms";
import CreateRoom      from "./pages/Admin/Rooms/CreateRoom";
import EditRoom        from "./pages/Admin/Rooms/EditRoom";
import CheckinQr       from "./pages/Admin/Rooms/CheckinQr"; 
import SearchInfo      from "./pages/Student/SearchInfo";
import StudentSchedule from "./pages/Student/StudentSchedule";
import Detail          from "./pages/Admin/Rooms/Detail";
import AllUser         from "./pages/Admin/Users/AllUser";
import CreateUser      from "./pages/Admin/Users/CreateUser";
import UpdateUser      from "./pages/Admin/Users/UpdateUser";
import UploadUserFile  from "./pages/Admin/Users/UploadFile";
import Dashboard       from "./pages/Admin/Dashboards/Dashboard";
import { UserSession } from "./components/UserSession";

import "./Index.css";


const App = () => {

    const dispatch = useDispatch();

    useEffect(()=>{
        currentUser().then(
            (res) => {
                dispatch({
                    type: "LOGGED_IN_USER",
                    payload: {
                        name: res.data[5],
                        role: res.data[8],
                        logoutUrl: res.data[9],
                        _id: res.data[2],
                    },
                });
            }
        ).catch(err =>{
            console.log(err)
        });
    },[dispatch]);
    
    return (
        <>
            {/*<Header/>*/}
            <ToastContainer />
            <Switch>
                <Route exact path="/" component={Home} />
                <Route path="/user-session" component={UserSession} />
                <ManagementRoute exact path="/admin/courses" component={AllCourse}/>
                <ManagementRoute exact path="/admin/course" component={CreateCourse}/>
                <ManagementRoute exact path="/admin/course/:id" component={EditCourse}/>
                <ManagementRoute exact path="/admin/courseUpload" component={UploadFile}/>
                <ManagementRoute exact path="/admin/students" component={AllStudents}/>
                <ManagementRoute exact path="/admin/student" component={CreateStudent}/>
                <ManagementRoute exact path="/admin/student/:id" component={EditStudent}/>
                <ManagementRoute exact path="/admin/studentUpload" component={UploadStudentFile}/>
                <ManagementRoute exact path="/admin/studentExams" component={AllStudentExams}/>
                <ManagementRoute exact path="/admin/studentExam" component={CreateStudentExam}/>
                <ManagementRoute exact path="/admin/studentExam/:id" component={EditStudentExam}/>
                <ManagementRoute exact path="/admin/studentExamUpload" component={UploadStudentExamFile}/>
                <ManagementRoute exact path="/admin/settings" component={AllSetting}/>
                <ManagementRoute exact path="/admin/setting" component={EditSetting}/>
                <ManagementRoute exact path="/admin/schedules" component={SchedulePage}/>
                <ManagementRoute exact path="/admin/rooms" component={AllRooms}/>
                <ManagementRoute exact path="/admin/room" component={CreateRoom}/>
                <ManagementRoute exact path="/admin/room/:id" component={EditRoom}/>
                <ManagementRoute exact path="/admin/roomDetail/:id" component={Detail}/>
                <ManagementRoute exact path="/admin/roomCheckin/:id" component={CheckinQr}/>
                <AdminRoute exact path="/admin/users" component={AllUser}/>
                <AdminRoute exact path="/admin/user" component={CreateUser}/>
                <AdminRoute exact path="/admin/user/:id" component={UpdateUser}/>
                <AdminRoute exact path="/admin/userUpload" component={UploadUserFile}/>
                <AdminRoute exact path="/admin/dashboard" component={Dashboard}/>
                <PrivateRoute exact path="/student" component={SearchInfo} />
                <PrivateRoute exact path="/student/schedule/:id" component={StudentSchedule} />
            </Switch>
       </>
        
    );
}

export default App;