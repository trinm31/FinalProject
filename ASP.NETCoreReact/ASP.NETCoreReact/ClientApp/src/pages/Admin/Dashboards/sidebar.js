import React             from 'react'
import DonutLargeIcon    from '@material-ui/icons/DonutLarge';
import ClearAllIcon      from '@material-ui/icons/ClearAll';
import ArrowUpwardIcon   from '@material-ui/icons/ArrowUpward';
import ArrowDownwardIcon from '@material-ui/icons/ArrowDownward';
import SyncAltIcon       from '@material-ui/icons/SyncAlt';
import LayersIcon        from '@material-ui/icons/Layers';
import LockIcon          from '@material-ui/icons/Lock';
import EcoIcon           from '@material-ui/icons/Eco';
import { Link }          from "react-router-dom";

const Sidebar = () => {
    return (
        <div className="md:w-3/12 w-6/12 shadow-2xl">
            <div className=" border-b py-3 mt-1 flex justify-around ">
                <h1>Exam Management System</h1>
            </div>
            <div className="p-4 space-y-14">
                <div className="space-y-4" >
                    <h1 className="text-gray-400">Menu</h1>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <DonutLargeIcon className=" text-gray-300" />
                            <Link to="/" className="hover:text-white">Home</Link>
                        </div>
                    </div>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <ClearAllIcon className="text-gray-300" />
                            <Link to="/admin/dashboard" className="hover:text-white">Dashboard</Link>
                        </div>
                    </div>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <ArrowUpwardIcon className="text-gray-300" />
                            <Link to="/admin/studentExams" className="hover:text-white">Student Exams</Link>
                        </div>
                    </div>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <ArrowDownwardIcon className="text-gray-300" />
                            <Link to="/admin/courses" className="hover:text-white">All Courses</Link>
                        </div>
                    </div>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <SyncAltIcon className="text-gray-300" />
                            <Link to="/admin/students" className="hover:text-white">All Students</Link>
                        </div>
                    </div>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <SyncAltIcon className="text-gray-300" />
                            <Link to="/admin/schedules" className="hover:text-white">Schedule</Link>
                        </div>
                    </div>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <SyncAltIcon className="text-gray-300" />
                            <Link to="/admin/settings" className="hover:text-white">Setting Schedule</Link>
                        </div>
                    </div>
                    <div className="">
                        <div className="flex p-3 text-gray-700  space-x-4 0 hover:bg-gray-50 hover:text-blue-600  cursor-pointer  ">
                            <SyncAltIcon className="text-gray-300" />
                            <Link to="/admin/users" className="hover:text-white">User Management</Link>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    )
}

export default Sidebar;
