import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import { editRoom , getRoomById }     from "../../../functions/room";
import UpSertRoomForm                 from "../../../components/Form/UpSertRoomForm";
import Header                         from "../../../components/nav/Header";

const initialState = {
    id: 0,
    name: "",
    courseId:""
}

const EditRoom = ({history, match}) => {
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState({
        name: "",
        courseId:""
    });

    // router
    const { id } = match.params;
    
    useEffect(()=>{console.log(values)},[values]);
    
    useEffect(()=>{
        loadRoom()
    },[]);

    const loadRoom = () => {
        console.log(id);
        getRoomById(id).then((res)=>{
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
        temp.courseId = values.courseId !== "" ? "" : "This field is required";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        if(validateForm()){
            editRoom(values)
                .then((res) => {
                    console.log(res);
                    toast.success("Item is edited");
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
        <>
            <Header/>
            <UpSertRoomForm errors={errors} values={values} handleChange={handleChange} handleSubmit={handleSubmit}/>
        </>
    );
}

export default EditRoom;