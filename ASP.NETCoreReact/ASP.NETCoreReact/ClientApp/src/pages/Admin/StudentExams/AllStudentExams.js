import React , { useCallback , useEffect , useRef , useState } from "react";
import { toast }                                               from "react-toastify";
import { removeStudentExam } from "../../../functions/studentExam";
import ListAllStudentExamTable                                           from "../../../components/tables/ListAllStudentExamTable";
import UseStudentCourse                                                  from "../../../Hooks/useStudentCourse";

const AllStudentExam = () => {
    const [studentExams, setStudentExams] = useState([]);
    const [filter, setFilter] = useState([]);
    const [page, setPage] = useState(1);
    const { isLoading , error , hasMore } = UseStudentCourse(page , setFilter, setStudentExams);

    const observer = useRef();
    const lastCourseElementRef = useCallback(
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
    
    const handleSearch = e => {
        let target = e.target;
        if(e.target.value === ""){
            setFilter(studentExams)
        }

        let filteredData = studentExams.filter(x =>
            x.studentId.toLowerCase().includes(target.value)||
            x.examId.toLowerCase().includes(target.value)
        )
        setFilter(filteredData);
    }
    
    const handleRemove = (id) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeStudentExam(id)
                .then((res) => {
                    let studentCourseList = studentExams.filter(c=> c.id !== id);
                    setFilter(studentCourseList);
                    setStudentExams(studentCourseList);
                    toast.error(`Item is deleted`);
                })
                .catch((err) => {
                    if (err.response.status === 400) toast.error(err.response.data);
                    console.log(err);
                });
        }
    };

    return (
        <>
            <ListAllStudentExamTable lastCourseElementRef={lastCourseElementRef} handleSearch={handleSearch} studentExamLists={filter} handleRemove={handleRemove}/>
            <div>{isLoading && "Loading..."}</div>
            <div>{error && "Error..."}</div>
        </>
    );
}

export default AllStudentExam;