import React, { useEffect, useState }     from "react";
import { useSelector }                    from "react-redux";
import { toast }                                            from "react-toastify";
import { createStudentQr , getAllStudents , removeStudent } from "../../../functions/student";
import ListAllStudentTable                                  from "../../../components/tables/ListAllStudentTable";

const AllStudents = () => {
    const [students, setStudents] = useState([]);
    const [loading, setLoading] = useState(false);
    const [filter, setFilter] = useState([]);

    //redux
    const { user } = useSelector((state) => ({ ...state }));

    useEffect(() => {
        loadAllStudents();
    }, []);

    const loadAllStudents = () =>{
        setLoading(true);
        getAllStudents().then((res)=> {
            setStudents(res.data);
            setFilter(res.data);
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

    const handleSearch = e => {
        let target = e.target;
        if(e.target.value === ""){
            setFilter(students)
        }

        let filteredData = students.filter(x =>
            x.name.toLowerCase().includes(target.value)||
            x.studentId.toLowerCase().includes(target.value) ||
            x.email.toLowerCase().includes(target.value)
        )
        setFilter(filteredData);
    }

    return (
        <>
            {loading ? (
                <h4 className="text-danger">Loading...</h4>
            ) : (
                <ListAllStudentTable handleSearch={handleSearch} studentLists={filter} handleRemove={handleRemove} createQrCode={createQrCode}/>
            )
            }
        </>
    );
}

export default AllStudents;