import React, {useState} from "react";

import { AiFillSchedule,AiOutlineBars } from "react-icons/ai";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

const Header = () => {

    const [isActive, setActive] = useState(false);
    
    let dispatch = useDispatch();
    let { user } = useSelector((state) => ({ ...state }));

    const logout = () => {
       
        dispatch({
            type: "LOGOUT",
            payload: null,
        });

        window.location.assign("/bff/login")
    };

    const toggleClass = () => {
        setActive(!isActive);
    };
    
    return(
        <nav className="flex items-center bg-grey p-3 flex-wrap">
            <a href="#" className="p-2 mr-4 inline-flex items-center text-white text-2xl">
              <AiFillSchedule className="text-white text-4xl mr-2"/> Exam Management System
            </a>
            <button
                className="text-white inline-flex p-3 hover:bg-gray-900 rounded lg:hidden ml-auto hover:text-white outline-none nav-toggler"
                data-target="#navigation"
                onClick={toggleClass}
            >
                <AiOutlineBars className="text-white text-xl"/>
            </button>
            <div
                className={isActive ? "top-navbar w-full lg:inline-flex lg:flex-grow lg:w-auto": "hidden top-navbar w-full lg:inline-flex lg:flex-grow lg:w-auto"}
                id="navigation"
            >
                <div
                    className="lg:inline-flex lg:flex-row lg:ml-auto lg:w-auto w-full lg:items-center items-start flex flex-col lg:h-auto"
                >
                    {
                        user&&(
                            <div>
                                <div
                                    className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                                >
                                    <Link to="/" className="hover:text-white">Home</Link>
                                </div>
                                <div
                                    className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                                >
                                    <Link to="/admin/rooms" className="hover:text-white">Rooms</Link>
                                </div>
                                <div
                                    className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                                >
                                    <Link to="/admin/studentExams" className="hover:text-white">Student Exams</Link>
                                </div>
                                <div
                                    className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                                >
                                    <Link to="/admin/courses" className="hover:text-white">All Courses</Link>
                                </div>
                                <div
                                    className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                                >
                                    <Link to="/admin/students" className="hover:text-white">All Students</Link>
                                </div>
                                <div
                                    className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                                >
                                    <Link to="/admin/schedules" className="hover:text-white">Schedule</Link>
                                </div>
                                <div
                                    className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                                >
                                    <Link to="/admin/settings" className="hover:text-white">Setting Schedule</Link>
                                </div>
                            </div>
                        )
                    }
                    {user&&(
                        <div
                            className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                        >
                            <a href={user.logoutUrl.value} className="hover:text-white" onClick={logout}>Log Out</a>
                        </div>
                    )
                    }
                    {!user&&(
                        <>
                            <div
                                className="lg:inline-flex lg:w-auto w-full px-3 py-2 rounded text-gray-400 items-center justify-center hover:bg-grey hover:text-white"
                            >
                                <a href="/bff/login" className="hover:text-white">Log In</a>
                            </div>
                        </>
                    )
                    }
                </div>
            </div>
        </nav>
    );
}

export default Header;