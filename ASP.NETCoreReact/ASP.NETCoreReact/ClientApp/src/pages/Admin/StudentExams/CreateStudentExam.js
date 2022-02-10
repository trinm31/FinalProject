import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import { createStudentExam }          from "../../../functions/studentExam";
import { getAllExams }                from "../../../functions/exam";
import { getAllStudents }             from "../../../functions/student";
import UpSertStudentExamForm          from "../../../components/Form/UpsertStudentExamForm";


const initialState = {
    studentId: "",
    examId:""
}

const CreateStudentExam = ({history}) => {
    const [values, setValues] = useState(initialState);
    const [exams, setExams] = useState([]);
    const [students, setStudents] = useState([]);

    useEffect(()=>{console.log(values)},[values]);
    
    useEffect(() => {
        loadExams();
        loadStudent();
    },[]);
    
    const loadExams =() =>{
        getAllExams().then((res)=>{
            setExams(res.data);
            console.log(res.data);
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
    }
    
    const loadStudent = ()=>{
        getAllStudents().then((res)=>{
            setStudents(res.data);
            console.log(res.data);
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        createStudentExam(values)
            .then((res) => {
                console.log(res);
                toast.success("Item is created");
                history.push("/admin/studentExams")
            })
            .catch((err) => {
                console.log(err);
                toast.error(err.response.data.err);
            });
    };
    
    return(
        <UpSertStudentExamForm setValues={setValues} exams={exams} students={students} values={values} handleSubmit={handleSubmit}/>
    );
}

export default CreateStudentExam;