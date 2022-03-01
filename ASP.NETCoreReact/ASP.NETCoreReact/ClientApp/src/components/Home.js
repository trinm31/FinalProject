import React  from "react";
import { Link } from "react-router-dom";

const Home = () =>{
        return (
            <>
                <div className="landing-page">
                    <div className="container">
                        <div className="info">
                            <h1>Exam Scheduling App</h1>
                            <p>A fantastic app that allows you to divide the exam schedule and check in to the exam room using a QR code</p>
                            <div
                                className="mt-2 lg:inline-flex lg:w-auto w-full px-3 py-2 rounded-lg bg-green-900 text-white items-center justify-center hover:bg-green-600 hover:text-white"
                            >
                                <Link to="/student" className="hover:text-white">Student Check</Link>
                            </div>
                        </div>
                        <div className="clearfix"></div>
                    </div>
                </div>
                
            </>
        );
}

export default Home;