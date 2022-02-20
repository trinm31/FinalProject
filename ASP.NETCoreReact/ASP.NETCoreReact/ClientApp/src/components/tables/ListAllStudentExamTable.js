import React               from "react";
import { Link }            from "react-router-dom";
import { AiOutlineSearch } from "react-icons/ai";

const ListAllStudentExamTable = ( { lastCourseElementRef, studentExamLists,handleRemove , handleSearch}) => {

    return (
        <div className="p-5 bg-gray-100">
            <div className="grid grid-cols-3">
                <div className="flex justify-start my-3 items-center">
                    <h1 className="text-xl mb-2">All Student Exam</h1>
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
                        className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 mx-2 rounded" to="/admin/studentExam">
                        Create New Student Exam
                    </Link>
                    <Link
                        className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded" to="/admin/studentExamUpload">
                        Upload Student Exam
                    </Link>
                </div>
            </div>

            <div className="overflow-auto rounded-lg shadow hidden md:block">
                <table className="w-full">
                    <thead className="bg-gray-50 border-b-2 border-gray-200">
                    <tr>
                        <th className="w-54 p-3 text-sm font-semibold tracking-wide text-left">CourseId</th>
                        <th className="w-54 p-3 text-sm font-semibold tracking-wide text-left">StudentId</th>
                        <th className="p-3 text-sm font-semibold tracking-wide text-left">Action</th>
                    </tr>
                    </thead>
                    {
                        studentExamLists.map(( studentCourse , i ) => {
                            if (studentCourse.length === i + 1) {
                                return (
                                    <tbody key={i} className="divide-y divide-gray-100" ref={lastCourseElementRef}>
                                    <tr className="bg-white" key={i}>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <a className="font-bold text-blue-500 hover:underline">{studentCourse.examId}</a>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            {studentCourse.studentId}
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap flex items-center">
                                            <Link
                                                className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded" to={`/admin/studentExam/${studentCourse.id}`}>
                                                Edit
                                            </Link>
                                            <button
                                                className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(studentCourse.id)}>
                                                Delete
                                            </button>
                                        </td>
                                    </tr>
                                    </tbody>
                                );
                            } else {
                                return (
                                    <tbody key={i} className="divide-y divide-gray-100" ref={lastCourseElementRef}>
                                    <tr className="bg-white" key={i}>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <a className="font-bold text-blue-500 hover:underline">{studentCourse.examId}</a>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            {studentCourse.studentId}
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap flex items-center">
                                            <Link
                                                className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded" to={`/admin/studentExam/${studentCourse.id}`}>
                                                Edit
                                            </Link>
                                            <button
                                                className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(studentCourse.id)}>
                                                Delete
                                            </button>
                                        </td>
                                    </tr>
                                    </tbody>
                                );
                            }
                        })
                    }
                </table>
            </div>

            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 md:hidden">
                {
                    studentExamLists.map((studentExam)=>(
                        <div className="bg-white space-y-3 p-4 rounded-lg shadow" key={studentExam.id}>
                            <div className="flex items-center space-x-2 text-sm">
                                <div>
                                    <a className="text-blue-500 font-bold hover:underline">{studentExam.examId}</a>
                                </div>
                                <div className="font-bold text-gray-500">{studentExam.studentId}</div>
                            </div>
                            <div className="text-sm font-medium text-black">
                                <Link
                                    className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded" to={`/admin/studentExam/${studentExam.id}`}>
                                    Edit
                                </Link>
                                <button
                                    className="bg-red-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(studentExam.id)}>
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

export default ListAllStudentExamTable;