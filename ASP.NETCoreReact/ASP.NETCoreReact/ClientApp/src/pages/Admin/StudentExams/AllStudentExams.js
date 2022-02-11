import React, { useEffect, useState } from "react";
import { useSelector }                from "react-redux";
import { toast }                      from "react-toastify";
import { getAllStudentExam , removeStudentExam } from "../../../functions/studentExam";
import ListAllStudentExamTable                   from "../../../components/tables/ListAllStudentExamTable";

const AllStudentExam = () => {
    const [studentExams, setStudentExams] = useState([]);
    const [loading, setLoading] = useState(false);
    const [filter, setFilter] = useState([]);

    //redux
    const { user } = useSelector((state) => ({ ...state }));

    useEffect(() => {
        loadAllStudentCourse();
    }, []);

    const loadAllStudentCourse = () =>{
        setLoading(true);
        getAllStudentExam().then((res)=> {
            setStudentExams(res.data);
            setFilter(res.data);
            console.log(res.data);
            setLoading(false)}
        ).catch((err) => {
            setLoading(false);
            console.log(err);
        });
    }

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
                    loadAllStudentCourse();
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
            {loading ? (
                <h4 className="text-danger">Loading...</h4>
            ) : (
                <ListAllStudentExamTable handleSearch={handleSearch} studentExamLists={filter} handleRemove={handleRemove}/>
            )
            }
        </>
    );
}

export default AllStudentExam;