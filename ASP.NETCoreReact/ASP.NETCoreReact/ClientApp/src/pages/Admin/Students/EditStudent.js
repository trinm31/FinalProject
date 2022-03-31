import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import {editStudent, getStudent}      from "../../../functions/student";
import UpSertStudentForm              from "../../../components/Form/UpSertStudentForm";
import Header                         from "../../../components/nav/Header";

const initialState = {
    id: "",
    name: "",
    studentId:"",
    email: "",
    avatar: ""
}

const EditStudent = ({history,match}) => {
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState({
        name: "",
        studentId:"",
        email: "",
    });

    useEffect(()=>{console.log(values)},[values]);

    useEffect(()=>{
        loadStudent()
    },[]);

    // router
    const { id } = match.params;

    const validateForm = () =>{
        let temp = {};
        temp.name = values.name !== "" ? "" : "This field is required";
        temp.studentId = values.studentId !== "" ? "" : "This field is required";
        temp.email = values.email !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }
    
    const loadStudent = () => {
        console.log(id);
        getStudent(id).then((res)=>{
            console.log(res.data);
            setValues({ ...values, ...res.data });
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
    }
    
    const handleSubmit = (e) => {
        e.preventDefault();
        if (validateForm()){
            editStudent(values)
                .then((res) => {
                    console.log(res);
                    toast.success("Item is created");
                    history.push("/admin/students")
                })
                .catch((err) => {
                    console.log(err);
                    toast.error(err.response.data.err);
                });   
        }
    };

    const handleChange = (e) => {
        setValues({ ...values, [e.target.name]: e.target.value });
    };

    return(
        <>
            <Header/>
            <UpSertStudentForm setValues={setValues} errors={errors} values={values} handleChange={handleChange} handleSubmit={handleSubmit} />
        </>
        
    );
}

export default EditStudent;