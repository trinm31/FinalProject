import React, {useEffect, useState} from 'react';
import {
    Switch,
    Route, Redirect
} from "react-router-dom";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { useDispatch } from "react-redux";
import { currentUser } from "./functions/auth";
import AdminRoute from "./components/routes/AdminRoute"

import CreateCourse    from "./pages/Admin/Courses/CreateCourse";
import EditCourse      from "./pages/Admin/Courses/EditCourse";
import AllCourse  from "./pages/Admin/Courses/AllCourse";
import UploadFile        from "./pages/Admin/Courses/UploadFile";
import Header            from './components/nav/Header';
import AllStudents       from "./pages/Admin/Students/AllStudents";
import CreateStudent     from "./pages/Admin/Students/CreateStudent";
import EditStudent       from "./pages/Admin/Students/EditStudent";
import UploadStudentFile from "./pages/Admin/Students/UploadStudentFile";
import AllStudentExams   from "./pages/Admin/StudentExams/AllStudentExams";
import CreateStudentExam from "./pages/Admin/StudentExams/CreateStudentExam";
import EditStudentExam   from "./pages/Admin/StudentExams/EditStudentExam";
import { Home }          from './components/Home';
import { UserSession }   from './components/UserSession';

import './Index.css';




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
            window.location.assign("/bff/login")
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
            </Switch>
       </>
        
    );
}

export default App;