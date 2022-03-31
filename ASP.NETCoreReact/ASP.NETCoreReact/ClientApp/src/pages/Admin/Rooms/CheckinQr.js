import React , { useState , useEffect } from "react";
import { toast }                        from "react-toastify";
import { QrReader }                     from 'react-qr-reader';
import { checkIn , checkInConfirm }     from "../../../functions/checkin";
import avatar                           from "../../../images/images.jpeg";
import { Link }                         from "react-router-dom";
import Header                           from "../../../components/nav/Header";

const CheckinQr = ( { match } ) => {

    const [data , setData] = useState("");
    const [student , setStudent] = useState({});

    // router
    const { id } = match.params;

    useEffect(() => {
        console.log(data);
        onCheckIn();
    } , [data])

    useEffect(() => {
        console.log(student);
    } , [student])

    const onCheckIn = () => {
        checkIn(encodeURIComponent(data)).then((res)=>{
            toast.success("Load Student Info Successfully")
            setStudent(res.data);
            console.log(res.data);
        }).catch(( err ) => {
            console.log(err)
        });
    }
    
    const onConfirm = () =>{
        checkInConfirm(encodeURIComponent(data),id).then((res)=>{
            toast.success("Check In Student Successfully")
            setStudent({});
            setData("");
        }).catch(( err ) => {
            console.log(err)
        });
    }

    const onCancel = () =>{
        setStudent({});
        setData("");
    }

    return (
        <>
            <Header/>
            <div className="hidden xl:flex lg:flex md:flex flex-row justify-between shadow-md border rounded-md overflow-hidden">
                <div
                    className="flex flex-col items-center justify-between w-1/4 px-4 py-2 bg-white border-r-2 border-gray-500 border-dashed rounded-l-md overflow-hidden"
                >
                    <div className="flex-col">
                        <p className="my-2 text-xl font-bold text-gray-500">
                            Scan here to check in!
                        </p>
                        <div className="overflow-hidden">
                            <QrReader
                                onResult={( result , error ) => {
                                    if (!!result) {
                                        setData(result?.text);
                                    }

                                    if (!!error) {
                                        console.info(error);
                                    }
                                }}
                                style={{ width: '100%' }}
                            />
                            {
                                student.studentId > 0 ? (
                                    <div className="p-3 text-sm text-gray-700 whitespace-nowrap flex items-center">
                                        <button
                                            className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 mx-4 rounded"
                                            onClick={onConfirm}
                                            >
                                            CheckIn
                                        </button>
                                        <button
                                            className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
                                            onClick={onCancel}
                                            >
                                            Cancel
                                        </button>
                                    </div>
                                ) : (
                                    <h2>Please Scan New One</h2>
                                )
                            }
                            
                        </div>
                    </div>
                </div>
                <div className="relative flex justify-center flex-col w-3/4">
                    <div className="flex justify-center">
                        <img
                            src={student.studentAva ? student.studentAva : avatar}
                            className="w-2/4"
                        />
                    </div>
                    <div className="absolute p-1 bottom-24">
                        <div
                            className="flex flex-row px-4 py-2 text-xs font-bold text-red-800 bg-white rounded-md "
                        >
                            <span className="mr-2 font-normal text-gray-500">Organizer :</span>
                            <p className="font-semibold text-gray-800">Greenwich University</p>
                        </div>
                    </div>
                    <div className="absolute bottom-0 flex flex-col w-full h-24">
                        <div className="w-full h-full bg-white opacity-75 rounded-br-md"></div>
                        <div className="absolute flex flex-row p-2 text-gray-800 opacity-100">
                            <div className="flex flex-col">
                                <div className="flex flex-col">
                                    <p className="mb-1 text-xs text-gray-500">Student Id:</p>
                                    <p className="text-md font-semibold text-red-800">
                                        {student.studentId}
                                    </p>
                                </div>
                                <div className="flex flex-col mt-1">
                                    <p className="mb-1 text-xs text-gray-500">Student Name:</p>
                                    <p className="mb-1 text-md font-semibold text-red-800">
                                        {student.studentName}
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default CheckinQr;