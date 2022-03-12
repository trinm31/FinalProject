import React , { useEffect , useState } from "react";
import { toast }                        from "react-toastify";
import { getStudentQr }                 from "../../functions/student"
import { AiOutlineSearch }              from "react-icons/ai";
import avatar                           from "../../images/images.jpeg";
import { Link }                         from "react-router-dom";
import { getUserPersionalId }           from "../../functions/user";
import { useSelector }                  from "react-redux";


const SearchInfo = () => {
    const [student , setStudent] = useState({});
    const [keyword , setKeyword] = useState("");
    const { user } = useSelector(( state ) => (
        { ...state }));

    useEffect(() => {
        loadKeyword();
    } , []);

    const loadKeyword = () => {
        getUserPersionalId(user._id.value).then(( res ) => {
            
            if (res.data.position === "Student") {
                setKeyword(res.data.persionalId);
                console.log(keyword);
            }
            toast.success("Load Persional Id Successfully")
        }).catch(( err ) => {
            toast.error("Load Persional Id Fail")
            console.log(err);
        })
    }

    const onSearch = () => {
        getStudentQr(keyword).then(( res ) => {
            toast.success("Get User Info Successfully")
            setStudent(res.data);
            console.log(res.data);
        }).catch(( err ) => {
            toast.error("Get User Info Fail ReCheck Your Student Id")
            console.log(err);
        })
    }

    return (
        <>
            <div className="flex justify-center mt-40">
                <div
                    className="bg-grey rounded-lg border border-gray-200 shadow-md dark:bg-gray-800 dark:border-gray-700">
                    <div className="flex flex-col items-center p-10 pb-10">
                        <div className="border border-gray-200 p-2 mb-2">
                            <img className="w-24 h-24 shadow-lg"
                                 src={student.qr}/>
                        </div>
                        <h3 className="mb-1 text-xl font-medium text-gray-900 dark:text-white">{student.name ? (
                            <>{student.name}</>) : (
                            <>Student Name</>)}</h3>
                        <span className="text-sm text-gray-500 dark:text-gray-400">{student.studentId ? (
                            <>{student.studentId}</>) : (
                            <>Student Id</>)}</span>
                        <div className="flex mt-4 space-x-3 lg:mt-6">
                            {
                                student.studentId > 0 ? (
                                    <div>
                                        <Link to={`/student/schedule/${student.studentId}`}
                                              className="inline-flex items-center py-2 px-4 text-sm font-medium text-center text-white bg-blue-700 rounded-lg hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">View
                                            Schedule</Link>
                                    </div>

                                ) : (
                                    <div className="relative text-gray-600">
                                        <input type="search" name="serch" placeholder="Student Id"
                                               value={keyword}
                                               onChange={( e ) => setKeyword(e.target.value)}
                                               className="bg-white h-10 px-5 pr-10 rounded-full text-sm focus:outline-none"/>
                                        <button onClick={onSearch}
                                                className="absolute right-0 top-0 mt-2.5 mr-4 hover:bg-gray-200">
                                            <AiOutlineSearch className="text-2xl"/>
                                        </button>
                                    </div>
                                )
                            }

                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default SearchInfo;