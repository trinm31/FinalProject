import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import {editStudent, getStudent}                from "../../../functions/student";
import UpSertStudentForm              from "../../../components/Form/UpSertStudentForm";
import { getCourse }                  from "../../../functions/exam";

const initialState = {
    id: "",
    name: "",
    studentId:"",
    email: "",
    avatar: ""
}

const EditStudent = ({history,match}) => {
    const [values, setValues] = useState(initialState);

    useEffect(()=>{console.log(values)},[values]);

    useEffect(()=>{
        loadStudent()
    },[]);

    // router
    const { id } = match.params;

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
    };

    const handleChange = (e) => {
        setValues({ ...values, [e.target.name]: e.target.value });
    };

    return(
        <UpSertStudentForm setValues={setValues} values={values} handleChange={handleChange} handleSubmit={handleSubmit} />
    );
}

export default EditStudent;