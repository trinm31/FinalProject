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
import UploadFile from "./pages/Admin/Courses/UploadFile";
import Header     from './components/nav/Header';
import AllStudents from "./pages/Admin/Students/AllStudents";
import CreateStudent from "./pages/Admin/Students/CreateStudent";
import { Home }        from './components/Home';
import { UserSession } from './components/UserSession';
import { FetchData }   from  './components/FetchData';

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
                <Route path="/weatherforecast" component={FetchData} />
                <AdminRoute exact path="/admin/courses" component={AllCourse}/>
                <AdminRoute exact path="/admin/course" component={CreateCourse}/>
                <AdminRoute exact path="/admin/course/:id" component={EditCourse}/>
                <AdminRoute exact path="/admin/courseUpload" component={UploadFile}/>
                <AdminRoute exact path="/admin/students" component={AllStudents}/>
                <AdminRoute exact path="/admin/student" component={CreateStudent}/>
            </Switch>
       </>
        
    );
}

export default App;