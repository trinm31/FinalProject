import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import UpSertCourseForm               from "../../../components/Form/UpSertCourseForm";
import {editCourse, getCourse}        from "../../../functions/exam";

const initialState = {
    id: "",
    name: "",
    examId:"",
    description: "",
    status: false
}

const EditCourse = ({history, match}) => {
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState({
        name: "",
        examId:""
    });
    
    useEffect(()=>{console.log(values)},[values]);

    // router
    const { id } = match.params;
    
    useEffect(()=>{
        loadCourse()
    },[]);
    
    const loadCourse = () => {
        console.log(id);
        getCourse(id).then((res)=>{
            console.log(res.data);
            setValues({ ...values, ...res.data });
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
    }

    const validateForm = () =>{
        let temp = {};
        temp.name = values.name !== "" ? "" : "This field is required";
        temp.examId = values.examId !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }
    
    const handleSubmit = (e) => {
        e.preventDefault();
        if (validateForm()){
            editCourse(values)
                .then((res) => {
                    console.log(res);
                    toast.success("Item is updated");
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

export default EditCourse;