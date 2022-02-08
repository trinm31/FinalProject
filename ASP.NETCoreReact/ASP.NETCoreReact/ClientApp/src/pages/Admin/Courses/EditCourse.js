import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import UpSertCourseForm               from "../../../components/Form/UpSertCourseForm";
import {editCourse, getCourse}        from "../../../functions/exam";

const initialState = {
    name: "",
    examId:"",
    description: "",
    status: false
}

const EditCourse = ({history, match}) => {
    const [values, setValues] = useState(initialState);

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

    const handleSubmit = (e) => {
        e.preventDefault();
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
    };

    const handleChange = (e) => {
        setValues({ ...values, [e.target.name]: e.target.value });
    };

    return(
        <UpSertCourseForm setValues={setValues} values={values} handleChange={handleChange} handleSubmit={handleSubmit}/>
    );
}

export default EditCourse;