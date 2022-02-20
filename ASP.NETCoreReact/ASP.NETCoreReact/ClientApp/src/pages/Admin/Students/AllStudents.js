import React , { useCallback , useEffect , useRef , useState } from "react";
import { toast }                                               from "react-toastify";
import { createStudentQr , removeStudent } from "../../../functions/student";
import ListAllStudentTable                                     from "../../../components/tables/ListAllStudentTable";
import useStudent                                              from "../../../Hooks/useStudent";

const AllStudents = () => {
    const [students, setStudents] = useState([]);
    const [filter, setFilter] = useState([]);
    const [page, setPage] = useState(1);
    const { isLoading , error , hasMore } = useStudent(page , setFilter, setStudents);
    
    const observer = useRef();
    const lastStudentElementRef = useCallback(
        ( node ) => {
            if (isLoading) return;
            if (observer.current) observer.current.disconnect();
            observer.current = new IntersectionObserver(( entries ) => {
                if (entries[0].isIntersecting && hasMore) {
                    setPage(( prev ) => prev + 1);
                }
            });
            if (node) observer.current.observe(node);
        } ,
        [isLoading , hasMore]
    );

    const handleRemove = (id) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeStudent(id)
                .then((res) => {
                    let courseList = students.filter(c=> c.id !== id);
                    setFilter(courseList);
                    setStudents(courseList);
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
            Window.location.reload();
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
            <ListAllStudentTable lastStudentElementRef={lastStudentElementRef} handleSearch={handleSearch} studentLists={filter} handleRemove={handleRemove} createQrCode={createQrCode}/>
            <div>{isLoading && "Loading..."}</div>
            <div>{error && "Error..."}</div>
        </>
    );
}

export default AllStudents;