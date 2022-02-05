import React from 'react';

import { AiFillSchedule } from "react-icons/ai";
import { Link } from "react-router-dom";

import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";


const Header = () => {
    
    let dispatch = useDispatch();
    let { user } = useSelector((state) => ({ ...state }));

    let history = useHistory();

    const logout = () => {
       
        dispatch({
            type: "LOGOUT",
            payload: null,
        });

        window.location.assign("/bff/login")
    };
    
    return(
        <nav className="flex items-center bg-grey p-3 flex-wrap">
            <a href="#" className="p-2 mr-4 inline-flex items-center text-white text-2xl">
              <AiFillSchedule className="text-white text-4xl"/> Exam Management System
            </a>
            <button
                className="text-white inline-flex p-3 hover:bg-gray-900 rounded lg:hidden ml-auto hover:text-white outline-none nav-toggler"
                data-target="#navigation"
            >
                <i className="material-icons">menu</i>
            </button>
            <div
                className="hidden top-navbar w-full lg:inline-flex lg:flex-grow lg:w-auto"
                id="navigation"
            >
                <div
                    className="lg:inline-flex lg:flex-row lg:ml-auto lg:w-auto w-full lg:items-center items-start flex flex-col lg:h-auto"
                >
                    <div
                        className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                    >
                        <Link to="/" className="hover:text-white">Home</Link>
                    </div>
                    <div
                        className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                    >
                        <Link to="/user-session" className="hover:text-white">User Session</Link>
                    </div>
                    <div
                        className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                    >
                        <Link to="/weatherforecast" className="hover:text-white">Weather forecast</Link>
                    </div>
                    <div
                        className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                    >
                        <Link to="#" className="hover:text-white">Cho</Link>
                    </div>
                    <a
                        href="#"
                        className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                    >
                        <span>Products</span>
                    </a>
                    {user&&(
                        <div
                            className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                        >
                            <a href={user.logoutUrl.value} className="hover:text-white" onClick={logout}>Log Out</a>
                        </div>
                    )
                    }
                    {!user&&(
                        <div
                            className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                        >
                            <a href="/bff/login" className="hover:text-white">Log In</a>
                        </div>
                    )
                    }
                </div>
            </div>
        </nav>
    );
}

export default Header;