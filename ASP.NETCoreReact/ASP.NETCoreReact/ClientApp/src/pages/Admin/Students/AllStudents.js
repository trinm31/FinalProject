import React, { useEffect, useState }     from "react";
import { useSelector }                    from "react-redux";
import { toast }                                                                from "react-toastify";
import { createStudentQr , paginationStudent , removeStudent } from "../../../functions/student";
import ListAllStudentTable                                                      from "../../../components/tables/ListAllStudentTable";

const AllStudents = () => {
    const [students, setStudents] = useState([]);
    const [loading, setLoading] = useState(false);
    const [filter, setFilter] = useState([]);
    const [page, setPage] = useState(1);

    //redux
    const { user } = useSelector((state) => ({ ...state }));

    useEffect(() => {
        loadMoreStudents(page);
    }, [page]);

    const loadMoreStudents = (page) =>{
        setLoading(true);
        paginationStudent(page).then((res)=> {
            setStudents([...students, ...res.data]);
            setFilter([...students, ...res.data]);
            console.log(res.data);
            setLoading(false)}
        ).catch((err) => {
            setLoading(false);
            console.log(err);
        });
    }

    const scrollToEnd = () => {
        setPage(page + 1);
    }

    window.onscroll = function(){
        if((window.innerHeight + Math.ceil(window.pageYOffset + 1)) >= document.body.offsetHeight){
            scrollToEnd();
        }
    }

    const handleRemove = (id) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeStudent(id)
                .then((res) => {
                    loadMoreStudents(page);
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
            loadMoreStudents(page);
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