import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import {createStudent}                from "../../../functions/student";
import UpSertStudentForm              from "../../../components/Form/UpSertStudentForm";

const initialState = {
    name: "",
    studentId:"",
    email: "",
    avatar: ""
}

const CreateStudent = ({history}) => {
    const [values, setValues] = useState(initialState);

    useEffect(()=>{console.log(values)},[values]);

    const handleSubmit = (e) => {
        e.preventDefault();
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
    };

    const handleChange = (e) => {
        setValues({ ...values, [e.target.name]: e.target.value });
    };

    return(
        <UpSertStudentForm setValues={setValues} values={values} handleChange={handleChange} handleSubmit={handleSubmit} />
    );
}

export default CreateStudent;