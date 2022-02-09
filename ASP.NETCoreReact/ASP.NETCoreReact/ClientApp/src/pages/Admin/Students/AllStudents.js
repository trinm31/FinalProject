import React, { useEffect, useState }     from "react";
import { useSelector }                    from "react-redux";
import { toast }                                            from "react-toastify";
import { createStudentQr , getAllStudents , removeStudent } from "../../../functions/student";
import ListAllStudentTable                                  from "../../../components/tables/ListAllStudentTable";

const AllStudents = () => {
    const [students, setStudents] = useState([]);
    const [loading, setLoading] = useState(false);

    //redux
    const { user } = useSelector((state) => ({ ...state }));

    useEffect(() => {
        loadAllStudents();
    }, []);

    const loadAllStudents = () =>{
        setLoading(true);
        getAllStudents().then((res)=> {
            setStudents(res.data);
            console.log(res.data);
            setLoading(false)}
        ).catch((err) => {
            setLoading(false);
            console.log(err);
        });
    }

    const handleRemove = (id) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeStudent(id)
                .then((res) => {
                    loadAllStudents();
                    toast.error(`Item is deleted`);
                })
                .catch((err) => {
                    if (err.response.status === 400) toast.error(err.response.data);
                    console.log(err);
                });
        }
    };

    const createQrCode = () =>{
        createStudentQr() .then((res) => {
            loadAllStudents();
            toast.success(`QR code is created`);
        })
            .catch((err) => {
                toast.error(err.response.data);
                console.log(err);
            });
    }

    return (
        <>
            {loading ? (
                <h4 className="text-danger">Loading...</h4>
            ) : (
                <ListAllStudentTable studentLists={students} handleRemove={handleRemove} createQrCode={createQrCode}/>
            )
            }
        </>
    );
}

export default AllStudents;