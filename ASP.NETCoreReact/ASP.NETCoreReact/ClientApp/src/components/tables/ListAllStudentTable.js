import React              from "react";
import { Link }           from "react-router-dom";
import { Button }         from "reactstrap";
import avatar             from "../../images/images.jpeg";

const ListAllCourseTable = ( {studentLists,handleRemove, createQrCode}) => {
    
    return (
        <div className="p-5 h-screen bg-gray-100">
            <div className="grid grid-cols-2">
                <div className="flex justify-start my-3 items-center">
                    <h1 className="text-xl mb-2">All Students</h1>
                </div>
               <div className="flex justify-end my-3 items-center">
                   <Link
                       className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 mx-2 rounded" to="/admin/student">
                       Create New Student 
                   </Link>
                   <Link
                       className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded" to="/admin/studentUpload">
                       Upload Student
                   </Link>
                   <Button
                       className="border-none bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 mx-2 rounded" onClick={createQrCode}>
                       Create QR Code
                   </Button>
               </div>
            </div>
            
            <div className="overflow-auto rounded-lg shadow hidden md:block">
                <table className="w-full">
                    <thead className="bg-gray-50 border-b-2 border-gray-200">
                        <tr>
                            <th className="w-30 p-3 text-sm font-semibold tracking-wide text-left">Student Id</th>
                            <th className="w-54 p-3 text-sm font-semibold tracking-wide text-left">Name</th>
                            <th className="w-34 p-3 text-sm font-semibold tracking-wide text-left">Email</th>
                            <th className="w-134 p-3 text-sm font-semibold tracking-wide text-left">Qr</th>
                            <th className="w-34 p-3 text-sm font-semibold tracking-wide text-left">Avatar</th>
                            <th className="p-3 text-sm font-semibold tracking-wide text-left">Action</th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-gray-100">
                    {
                        studentLists.map((student)=>(
                            <tr className="bg-white" key={student.id}>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    <a className="font-bold text-blue-500 hover:underline">{student.studentId}</a>
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    {student.name}
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    {student.email}
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    <img src={student.qr} height="300" width="300" alt=""/>
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                    <img src={student.avatar? student.avatar: avatar} height="300" width="300" alt=""/>
                                </td>
                                <td className="p-3 text-sm text-gray-700 whitespace-nowrap flex items-center">
                                    <Link
                                        className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded" to={`/admin/student/${student.id}`}>
                                        Edit
                                    </Link>
                                    <button
                                        className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(student.id)}>
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
                    studentLists.map((student)=>(
                        <div className="bg-white space-y-3 p-4 rounded-lg shadow" key={student.id}>
                            <div className="flex items-center space-x-2 text-sm">
                                <div>
                                    <a className="text-blue-500 font-bold hover:underline">{student.studentId}</a>
                                </div>
                                <div className="font-bold text-gray-500">{student.name}</div>
                            </div>
                            <div className="text-sm text-gray-700">
                                {student.email}
                            </div>
                            <div className="text-sm font-medium text-black">
                                <Link
                                    className="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4 mx-4 rounded" to={`/admin/student/${student.id}`}>
                                    Edit
                                </Link>
                                <button
                                    className="bg-red-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={()=> handleRemove(student.id)}>
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