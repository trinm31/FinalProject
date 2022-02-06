import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import UpSertCourseForm               from "../../components/Form/UpSertCourseForm";
import {createCourse}                 from "../../functions/exam";

const initialState = {
    name: "",
    examId:"",
    description: "", 
    status: false
}

const CreateCourse = ({history}) => {
    const [values, setValues] = useState(initialState);

    useEffect(()=>{console.log(values)},[values]);
    
    const handleSubmit = (e) => {
        e.preventDefault();
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
    };
    
    const handleChange = (e) => {
        setValues({ ...values, [e.target.name]: e.target.value });
    };
    
    return(
        <UpSertCourseForm setValues={setValues} values={values} handleChange={handleChange} handleSubmit={handleSubmit}/>
    );
}

export default CreateCourse;