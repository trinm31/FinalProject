import React , { useEffect , useState } from "react";
import { toast }                        from "react-toastify";
import { getDetail }                    from "../../../functions/room";
import Header                           from "../../../components/nav/Header";

const Detail = ({match}) =>{
    const [students, setStudents] = useState([]);

    // router
    const { id } = match.params;
    
    useEffect(()=>{
        loadStudent();
    },[]);
    
    const loadStudent = () =>{
        getDetail(id).then((res)=>{
            toast.success("Get All Student Successfully")
            setStudents(res.data);
        }).catch((err)=>{
            toast.error("Get All Student fail")
            console.log(err);
        })
    }
    
    return (
        <>
            <Header/>
            <div className="h-screen p-5 bg-gray-100">
                <div className="grid grid-cols-3">
                    <div className="flex justify-start my-3 items-center">
                        <h1 className="text-xl mb-2">All Students</h1>
                    </div>
                </div>

                <div className="overflow-auto rounded-lg shadow hidden md:block" >
                    <table className="w-full">
                        <thead className="bg-gray-50 border-b-2 border-gray-200">
                        <tr>
                            <th className="w-40 p-3 text-sm font-semibold tracking-wide text-left">No</th>
                            <th className="w-104 p-3 text-sm font-semibold tracking-wide text-left">Name</th>
                            <th className="w-104 p-3 text-sm font-semibold tracking-wide text-left">Student Id</th>
                        </tr>
                        </thead>

                        <tbody className="divide-y divide-gray-100" >
                        {
                            students.map(( student , i ) => (
                                <tr className="bg-white" key={i}>
                                    <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                        <div>
                                            {i}
                                        </div>
                                    </td>
                                    <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                        <div>
                                            {student.name}
                                        </div>
                                    </td>
                                    <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{student.studentId}</td>
                                </tr>
                            ))
                        }
                        </tbody>


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
        </>
    );
}

export default Detail;