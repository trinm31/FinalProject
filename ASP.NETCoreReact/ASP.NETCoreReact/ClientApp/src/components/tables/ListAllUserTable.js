import React               from "react";
import { Link }            from "react-router-dom";
import { AiOutlineSearch } from "react-icons/ai";

const ListAllUserTable = ( { lastUserElementRef , userLists , handleRemove , handleSearch } ) => {

    return (
        <div className="p-5 bg-gray-100">
            <div className="grid grid-cols-3">
                <div className="flex justify-start my-3 items-center">
                    <h1 className="text-xl mb-2">All Users</h1>
                </div>
                <div className="flex items-center">
                    <label className="relative block flex-1">
                        <span className="sr-only">Search</span>
                        <span className="absolute inset-y-0 left-0 flex items-center pl-2">
                           <AiOutlineSearch/>
                         </span>
                        <input
                            className="placeholder:italic placeholder:text-slate-400 block bg-white w-full border border-slate-300 rounded-md py-2 pl-9 pr-3 shadow-sm focus:outline-none focus:border-sky-500 focus:ring-sky-500 focus:ring-1 sm:text-sm"
                            placeholder="Search for anything..." type="text" name="search" onChange={handleSearch}/>
                    </label>
                </div>
                <div className="flex justify-end my-3 items-center">
                    <Link
                        className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 mx-2 rounded"
                        to="/admin/user">
                        Create New User
                    </Link>
                    <Link
                        className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
                        to="/admin/userUpload">
                        Upload Users
                    </Link>
                </div>
            </div>

            <div className="overflow-auto rounded-lg shadow hidden md:block" >
                <table className="w-full">
                    <thead className="bg-gray-50 border-b-2 border-gray-200">
                    <tr>
                        <th className="w-20 p-3 text-sm font-semibold tracking-wide text-left">PersionalId</th>
                        <th className="w-54 p-3 text-sm font-semibold tracking-wide text-left">LastName</th>
                        <th className="w-34 p-3 text-sm font-semibold tracking-wide text-left">FirstName</th>
                        <th className="w-44 p-3 text-sm font-semibold tracking-wide text-left">Position</th>
                        <th className="w-44 p-3 text-sm font-semibold tracking-wide text-left">PhoneNumber</th>
                        <th className="w-44 p-3 text-sm font-semibold tracking-wide text-left">Role</th>
                        <th className="p-3 text-sm font-semibold tracking-wide text-left">Action</th>
                    </tr>
                    </thead>
                    {
                        userLists.map(( user , i ) => {
                            if (user.length === i + 1) {
                                return (
                                    <tbody key={i} className="divide-y divide-gray-100" ref={lastUserElementRef}>
                                    <tr className="bg-white" key={i}>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <a className="font-bold text-blue-500 hover:underline">{user.persionalId}</a>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.lastName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.firstName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.position}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.phoneNumber}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.role}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap flex items-center">
                                            <Link
                                                className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded"
                                                to={`/admin/user/${user.id}`}>
                                                Edit
                                            </Link>
                                            {
                                                user.role !== "Admin" &&(
                                                    <button
                                                        className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
                                                        onClick={() => handleRemove(user.id)}>
                                                        Delete
                                                    </button>
                                                )
                                            }
                                        </td>
                                    </tr>
                                    </tbody>
                                );
                            } else {
                                return (
                                    <tbody key={i} className="divide-y divide-gray-100" ref={lastUserElementRef}>
                                    <tr className="bg-white" key={i}>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <a className="font-bold text-blue-500 hover:underline">{user.persionalId}</a>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.lastName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.firstName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.position}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.phoneNumber}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.role}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap flex items-center">
                                            <Link
                                                className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded"
                                                to={`/admin/user/${user.id}`}>
                                                Edit
                                            </Link>
                                            {
                                                user.role !== "Admin" &&(
                                                    <button
                                                        className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
                                                        onClick={() => handleRemove(user.id)}>
                                                        Delete
                                                    </button>
                                                )
                                            }
                                        </td>
                                    </tr>
                                    </tbody>
                                );
                            }
                        })
                    }

                </table>
            </div>

            {/*<div className="grid grid-cols-1 sm:grid-cols-2 gap-4 md:hidden">*/}
            {/*    {*/}
            {/*        courseLists.map((course)=>(*/}
            {/*            <div className="bg-white space-y-3 p-4 rounded-lg shadow" key={course.id}>*/}
            {/*                <div className="flex items-center space-x-2 text-sm">*/}
            {/*                    <div>*/}
            {/*                        <a className="text-blue-500 font-bold hover:underline">{course.examId}</a>*/}
            {/*                    </div>*/}
            {/*                    <div className="font-bold text-gray-500">{course.name}</div>*/}
            {/*                    <div>*/}
            {/*                        {course.status ?*/}
            {/*                            (*/}
            {/*                                <span className="p-1.5 text-xs font-medium uppercase tracking-wider text-green-800 bg-green-200 rounded-lg bg-opacity-50">Active</span>*/}
            {/*                            ):(*/}
            {/*                                <span className="p-1.5 text-xs font-medium uppercase tracking-wider text-white-800 bg-red-200 rounded-lg bg-opacity-50">NonActive</span>*/}
            {/*                            )*/}
            {/*                        }*/}
            {/*                    </div>*/}
            {/*                </div>*/}
            {/*                <div className="text-sm text-gray-700">*/}
            {/*                    {course.description && course.description.substring(0, 40)}*/}
            {/*                </div>*/}
            {/*                <div className="text-sm font-medium text-black">*/}
            {/*                    <Link*/}
            {/*                        className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded" to={`/admin/course/${course.id}`}>*/}
            {/*                        Edit*/}
            {/*                    </Link>*/}
            {/*                    <button*/}
            {/*                        className="bg-red-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(course.id)}>*/}
            {/*                        Delete*/}
            {/*                    </button>*/}
            {/*                </div>*/}
            {/*            </div>*/}
            {/*        ))*/}
            {/*    }*/}
            {/*</div>*/}
        </div>
    );
}

export default ListAllUserTable;