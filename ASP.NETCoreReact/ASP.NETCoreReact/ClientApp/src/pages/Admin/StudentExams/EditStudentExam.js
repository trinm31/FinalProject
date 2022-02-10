import React, { useState, useEffect } from "react";
import { toast }                                                from "react-toastify";
import { editStudentExam , getStudentExam } from "../../../functions/studentExam";
import { getAllExams }                                          from "../../../functions/exam";
import { getAllStudents }          from "../../../functions/student";
import UpSertStudentExamForm          from "../../../components/Form/UpsertStudentExamForm";


const initialState = {
    id: "",
    studentId: "",
    examId:""
}

const EditStudentExam = ({history, match}) => {
    const [values, setValues] = useState(initialState);
    const [exams, setExams] = useState([]);
    const [students, setStudents] = useState([]);
    const [errors, setErrors] = useState({
        studentId: "",
        examId:""
    });

    // router
    const { id } = match.params;
    
    useEffect(()=>{console.log(values)},[values]);

    useEffect(() => {
        loadExams();
        loadStudent();
        loadStudentExam();
    },[]);
    
    const validateForm = () =>{
        let temp = {};
        temp.studentId = values.studentId !== "" ? "" : "This field is required";
        temp.examId = values.examId !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }
    
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
        if(validateForm()){
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
        }
    };

    return(
        <UpSertStudentExamForm setValues={setValues} errors={errors} exams={exams} students={students} values={values} handleSubmit={handleSubmit}/>
    );
}

export default EditStudentExam;