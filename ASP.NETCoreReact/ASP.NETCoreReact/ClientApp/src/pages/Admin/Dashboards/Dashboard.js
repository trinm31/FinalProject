import React   from 'react';
import Sidebar from "./sidebar";
import Header from "./Header"
import Container from "./Container"

const Dashboard = () =>{
    
    return(
        <div className="flex w-screen">
            <Sidebar />
            <div className="w-screen ">
                <Header />
                <Container />
            </div>
        </div>
    )
}

export default Dashboard;