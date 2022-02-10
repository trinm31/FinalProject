import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import {createStudent}                from "../../../functions/student";
import UpSertStudentForm              from "../../../components/Form/UpSertStudentForm";

const initialState = {
    id: "",
    name: "",
    studentId:"",
    email: "",
    avatar: ""
}

const CreateStudent = ({history}) => {
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState({
        name: "",
        studentId:"",
        email: "",
    });

    useEffect(()=>{console.log(values)},[values]);

    const validateForm = () =>{
        let temp = {};
        temp.name = values.name !== "" ? "" : "This field is required";
        temp.studentId = values.studentId !== "" ? "" : "This field is required";
        temp.email = values.email !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }
    
    const handleSubmit = (e) => {
        e.preventDefault();
        if(validateForm()){
            createStudent(values)
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
        <UpSertStudentForm setValues={setValues} errors={errors} values={values} handleChange={handleChange} handleSubmit={handleSubmit} />
    );
}

export default CreateStudent;