import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import { createStudentExam }          from "../../../functions/studentExam";
import { getAllExams }                from "../../../functions/exam";
import { getAllStudents }             from "../../../functions/student";
import UpSertStudentExamForm          from "../../../components/Form/UpsertStudentExamForm";
import Header                         from "../../../components/nav/Header";


const initialState = {
    studentId: "",
    examId:""
}

const CreateStudentExam = ({history}) => {
    const [values, setValues] = useState(initialState);
    const [exams, setExams] = useState([]);
    const [students, setStudents] = useState([]);
    const [errors, setErrors] = useState({
        studentId: "",
        examId:""
    });

    useEffect(()=>{console.log(values)},[values]);
    
    useEffect(() => {
        loadExams();
        loadStudent();
    },[]);

    const validateForm = () =>{
        let temp = {};
        temp.studentId = values.studentId !== "" ? "" : "This field is required";
        temp.examId = values.examId !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }
    
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
        if(validateForm()){
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
        }
    };
    
    return(
        <>
            <Header/>
            <UpSertStudentExamForm setValues={setValues} errors={errors} exams={exams} students={students} values={values} handleSubmit={handleSubmit}/>
        </>
    );
}

export default CreateStudentExam;