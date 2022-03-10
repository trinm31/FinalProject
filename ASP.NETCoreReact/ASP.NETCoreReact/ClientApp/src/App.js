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

import CreateCourse    from "./pages/Admin/Courses/CreateCourse";
import EditCourse      from "./pages/Admin/Courses/EditCourse";
import AllCourse  from "./pages/Admin/Courses/AllCourse";
import UploadFile        from "./pages/Admin/Courses/UploadFile";
import Header            from "./components/nav/Header";
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
            <Header/>
            <ToastContainer />
            <Switch>
                <Route exact path="/" component={Home} />
                <Route path="/user-session" component={UserSession} />
                <AdminRoute exact path="/admin/courses" component={AllCourse}/>
                <AdminRoute exact path="/admin/course" component={CreateCourse}/>
                <AdminRoute exact path="/admin/course/:id" component={EditCourse}/>
                <AdminRoute exact path="/admin/courseUpload" component={UploadFile}/>
                <AdminRoute exact path="/admin/students" component={AllStudents}/>
                <AdminRoute exact path="/admin/student" component={CreateStudent}/>
                <AdminRoute exact path="/admin/student/:id" component={EditStudent}/>
                <AdminRoute exact path="/admin/studentUpload" component={UploadStudentFile}/>
                <AdminRoute exact path="/admin/studentExams" component={AllStudentExams}/>
                <AdminRoute exact path="/admin/studentExam" component={CreateStudentExam}/>
                <AdminRoute exact path="/admin/studentExam/:id" component={EditStudentExam}/>
                <AdminRoute exact path="/admin/studentExamUpload" component={UploadStudentExamFile}/>
                <AdminRoute exact path="/admin/settings" component={AllSetting}/>
                <AdminRoute exact path="/admin/setting" component={EditSetting}/>
                <AdminRoute exact path="/admin/schedules" component={SchedulePage}/>
                <AdminRoute exact path="/admin/rooms" component={AllRooms}/>
                <AdminRoute exact path="/admin/room" component={CreateRoom}/>
                <AdminRoute exact path="/admin/room/:id" component={EditRoom}/>
                <AdminRoute exact path="/admin/roomDetail/:id" component={Detail}/>
                <AdminRoute exact path="/admin/roomCheckin/:id" component={CheckinQr}/>
                <AdminRoute exact path="/admin/users" component={AllUser}/>
                <AdminRoute exact path="/admin/user" component={CreateUser}/>
                <AdminRoute exact path="/admin/user/:id" component={UpdateUser}/>
                <Route exact path="/student" component={SearchInfo} />
                <Route exact path="/student/schedule/:id" component={StudentSchedule} />
            </Switch>
       </>
        
    );
}

export default App;