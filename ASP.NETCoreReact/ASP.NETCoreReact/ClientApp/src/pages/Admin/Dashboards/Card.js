import React from 'react'
import AllOutIcon from '@material-ui/icons/AllOut';
import DoneIcon from '@material-ui/icons/Done';
import EcoIcon from '@material-ui/icons/Eco';
import LockIcon from '@material-ui/icons/Lock';


const Style = "text-white text-xs"

const Card = () => {
    
    return (
        <>
            <div className="transform hover:scale-110 cursor-pointer transition delay-100 w-3/12  p-2 py-4 shadow-xl  border rounded-xl bg-gradient-to-r from-indigo-500 to-blue-500">
                <div className="flex justify-between">
                    <div></div>
                    <div className=" w-10  h-10 flex items-center justify-center  bg-gray-300 rounded-xl m-1  bg-opacity-30">
                        <AllOutIcon fontSize="small" className={Style} />
                    </div>
                </div>
                <p className="text-gray-200 text-xs  ">
                    TOTAL
                </p>
                <p className="text-gray-50 text-lg  font-semibold ">
                    14406 Students
                </p>
            </div>
            <div className="transform hover:scale-110 cursor-pointer transition delay-100 w-3/12  p-2 py-4 shadow-xl  border rounded-xl bg-gradient-to-r from-blue-400 to-blue-300">
                <div className="flex justify-between">
                    <div></div>
                    <div className=" w-10  h-10 flex items-center justify-center  bg-gray-300 rounded-xl m-1  bg-opacity-30">
                        <AllOutIcon fontSize="small" className={Style} />
                    </div>
                </div>
                <p className="text-gray-200 text-xs  ">
                    TOTAL
                </p>
                <p className="text-gray-50 text-lg  font-semibold ">
                    92968 Course-Students
                </p>
            </div>
            <div className="transform hover:scale-110 cursor-pointer transition delay-100 w-3/12  p-2 py-4 shadow-xl  border rounded-xl bg-gradient-to-r from-green-500 to-green-400">
                <div className="flex justify-between">
                    <div></div>
                    <div className=" w-10  h-10 flex items-center justify-center  bg-gray-300 rounded-xl m-1  bg-opacity-30">
                        <AllOutIcon fontSize="small" className={Style} />
                    </div>
                </div>
                <p className="text-gray-200 text-xs  ">
                    TOTAL
                </p>
                <p className="text-gray-50 text-lg  font-semibold ">
                    2294 Courses
                </p>
            </div>
            <div className="transform hover:scale-110 cursor-pointer transition delay-100 w-3/12  p-2 py-4 shadow-xl  border rounded-xl bg-gradient-to-r from-yellow-600 to-yellow-500">
                <div className="flex justify-between">
                    <div></div>
                    <div className=" w-10  h-10 flex items-center justify-center  bg-gray-300 rounded-xl m-1  bg-opacity-30">
                        <AllOutIcon fontSize="small" className={Style} />
                    </div>
                </div>
                <p className="text-gray-200 text-xs  ">
                    TOTAL
                </p>
                <p className="text-gray-50 text-lg  font-semibold ">
                    409.0790 Users
                </p>
            </div>
        </>
 
    )
}

export default Card
