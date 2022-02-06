import React    from "react";
import { Link } from "react-router-dom";

const ListAllCourseTable = ( {courseLists,handleRemove}) => {
   
    return (
        <div className="p-5 h-screen bg-gray-100">
            <h1 className="text-xl mb-2">All Courses</h1>

            <div className="overflow-auto rounded-lg shadow hidden md:block">
                <table className="w-full">
                    <thead className="bg-gray-50 border-b-2 border-gray-200">
                        <tr>
                            <th className="w-20 p-3 text-sm font-semibold tracking-wide text-left">CourseId</th>
                            <th className="w-54 p-3 text-sm font-semibold tracking-wide text-left">Name</th>
                            <th className="w-34 p-3 text-sm font-semibold tracking-wide text-left">Status</th>
                            <th className="w-74 p-3 text-sm font-semibold tracking-wide text-left">Description</th>
                            <th className="p-3 text-sm font-semibold tracking-wide text-left">Action</th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-gray-100">
                    {
                        courseLists.map((course)=>(
                            <tr className="bg-white" key={course.id}>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    <a className="font-bold text-blue-500 hover:underline">{course.examId}</a>
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    {course.name}
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    {course.status ?
                                        (
                                            <span className="p-1.5 text-xs font-medium uppercase tracking-wider text-green-800 bg-green-200 rounded-lg bg-opacity-50">Active</span>
                                        ):(
                                            <span className="p-1.5 text-xs font-medium uppercase tracking-wider text-white-800 bg-red-200 rounded-lg bg-opacity-50">NonActive</span>
                                        )
                                    }
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{course.description && course.description.substring(0, 40)}</td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    <button
                                        className="bg-red-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(course.id)}>
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))
                    }
                    </tbody>
                </table>
            </div>

            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 md:hidden">
                {
                    courseLists.map((course)=>(
                        <div className="bg-white space-y-3 p-4 rounded-lg shadow" key={course.id}>
                            <div className="flex items-center space-x-2 text-sm">
                                <div>
                                    <a className="text-blue-500 font-bold hover:underline">{course.examId}</a>
                                </div>
                                <div className="font-bold text-gray-500">{course.name}</div>
                                <div>
                                    {course.status ?
                                        (
                                            <span className="p-1.5 text-xs font-medium uppercase tracking-wider text-green-800 bg-green-200 rounded-lg bg-opacity-50">Active</span>
                                        ):(
                                            <span className="p-1.5 text-xs font-medium uppercase tracking-wider text-white-800 bg-red-200 rounded-lg bg-opacity-50">NonActive</span>
                                        )
                                    }
                                </div>
                            </div>
                            <div className="text-sm text-gray-700">
                                {course.description && course.description.substring(0, 40)}
                            </div>
                            <div className="text-sm font-medium text-black">
                                <button
                                    className="bg-red-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(course.id)}>
                                    Delete
                                </button>
                            </div>
                        </div>
                    ))
                }
            </div>
        </div>
    );
}

export default ListAllCourseTable;