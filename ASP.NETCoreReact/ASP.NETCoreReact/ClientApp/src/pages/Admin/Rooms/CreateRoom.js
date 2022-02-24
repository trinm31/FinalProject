import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import {createRoom}                   from "../../../functions/room";
import UpSertRoomForm                 from "../../../components/Form/UpSertRoomForm";

const initialState = {
    id: 0,
    name: "",
    courseId:""
}

const CreateRoom = ({history}) => {
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState({
        name: "",
        courseId:""
    });

    useEffect(()=>{console.log(values)},[values]);

    const validateForm = () =>{
        let temp = {};
        temp.name = values.name !== "" ? "" : "This field is required";
        temp.courseId = values.courseId !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        if(validateForm()){
            createRoom(values)
                .then((res) => {
                    console.log(res);
                    toast.success("Item is created");
                    history.push("/admin/rooms")
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
        <UpSertRoomForm errors={errors} values={values} handleChange={handleChange} handleSubmit={handleSubmit}/>
    );
}

export default CreateRoom;