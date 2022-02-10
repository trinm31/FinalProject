import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import UpSertCourseForm               from "../../../components/Form/UpSertCourseForm";
import {createCourse}                 from "../../../functions/exam";

const initialState = {
    id: "",
    name: "",
    examId:"",
    description: "", 
    status: false
}



const CreateCourse = ({history}) => {
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState({
        name: "",
        examId:""
    });

    useEffect(()=>{console.log(values)},[values]);

    const validateForm = () =>{
        let temp = {};
        temp.name = values.name !== "" ? "" : "This field is required";
        temp.examId = values.examId !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }
    
    const handleSubmit = (e) => {
        e.preventDefault();
        if(validateForm()){
            createCourse(values)
                .then((res) => {
                    console.log(res);
                    toast.success("Item is created");
                    history.push("/admin/courses")
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
        <UpSertCourseForm setValues={setValues} errors={errors} values={values} handleChange={handleChange} handleSubmit={handleSubmit}/>
    );
}

export default CreateCourse;