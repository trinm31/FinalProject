import React, { useState, useEffect } from "react";
import { toast }                                                from "react-toastify";
import { editStudentExam , getStudentExam } from "../../../functions/studentExam";
import { getAllExams }                                          from "../../../functions/exam";
import { getAllStudents }          from "../../../functions/student";
import UpSertStudentExamForm          from "../../../components/Form/UpsertStudentExamForm";


const initialState = {
    studentId: "",
    examId:""
}

const EditStudentExam = ({history, match}) => {
    const [values, setValues] = useState(initialState);
    const [exams, setExams] = useState([]);
    const [students, setStudents] = useState([]);

    // router
    const { id } = match.params;
    
    useEffect(()=>{console.log(values)},[values]);

    useEffect(() => {
        loadExams();
        loadStudent();
        loadStudentExam();
    },[]);

    const loadStudentExam = () => {
        console.log(id);
        getStudentExam(id).then((res)=>{
            console.log(res.data);
            setValues({ ...values, ...res.data });
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
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
        editStudentExam(values)
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

export default EditStudentExam;