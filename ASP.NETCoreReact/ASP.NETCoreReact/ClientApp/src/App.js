import React, { useEffect } from 'react';
import {
    BrowserRouter,
    Routes,
    Route
} from "react-router-dom";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { useDispatch } from "react-redux";
import { currentUser } from "./functions/auth";

import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { UserSession } from './components/UserSession';
import { FetchData } from  './components/FetchData'

import './Index.css'

const App = () => {

    const dispatch = useDispatch();

    useEffect(()=>{
        currentUser().then(
            (res) => {
                console.log(res.data)
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
        ).catch(err => console.log(err));
    },[dispatch]);
    
    return (
      <Layout>
          <Route exact path="/" component={Home} />
          <Route path="/user-session" component={UserSession} />
          <Route path="/weatherforecast" component={FetchData} />
      </Layout>
    );
}

export default App;