import React, { useState, useEffect } from "react";
import { toast }                      from "react-toastify";
import { editSetting , getSetting }   from "../../../functions/setting";
import UpdateSettingForm              from "../../../components/Form/UpdateSettingForm";
import Header                         from "../../../components/nav/Header";

const initialState = {
    id: "",
    startDate: new Date(),
    endDate: new Date(),
    concurrencyLevelDefault: 0,
    externalDistance: 0,
    internalDistance: 0,
    noOfTimeSlot:0
}

const EditSetting = ({history, match}) => {
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState({
        startDate: "",
        endDate: "",
        concurrencyLevelDefault: 0,
        externalDistance: 0,
        internalDistance: 0,
        noOfTimeSlot:0
    });
    
    useEffect(()=>{console.log(values)},[values]);

    // router
    const { id } = match.params;
    
    useEffect(()=>{
        loadSetting()
    },[]);
    
    const loadSetting = () => {
        console.log(id);
        getSetting().then((res)=>{
            console.log(res.data);
            setValues({ ...values, ...res.data, startDate: new Date(res.data.startDate), endDate: new Date(res.data.endDate) });
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
    }

    const validateForm = () =>{
        let temp = {};
        temp.startDate = values.startDate >= Date.now() ? "" : "Date need to be in the future";
        temp.endDate = values.endDate >= Date.now() ? "" : "Date need to be in the future";
        temp.concurrencyLevelDefault = values.concurrencyLevelDefault >= 0 ? "": "Number need to be more than zero";
        temp.externalDistance = values.externalDistance > -1 ? "": "Number need to be more than zero";
        temp.internalDistance = values.internalDistance > -1? "": "Number need to be more than zero";
        temp.noOfTimeSlot = values.noOfTimeSlot > -1 ? "": "Number need to be more than zero";
        temp.concurrencyLevelDefault = values.concurrencyLevelDefault > 101 ? "Number need to be less than hundred":"" ;
        temp.externalDistance = values.externalDistance > 101 ? "Number need to be less than hundred": "";
        temp.internalDistance = values.internalDistance > 101 ? "Number need to be less than hundred" : "";
        temp.noOfTimeSlot = values.noOfTimeSlot > 101 ? "Number need to be less than hundred" : "";
        setErrors({...temp});
        return Object.values(temp).every(x=> x === "");
    }
    
    const handleSubmit = (e) => {
        e.preventDefault();
        if (validateForm()){
            editSetting(values)
                .then((res) => {
                    console.log(res);
                    toast.success("Item is updated");
                    history.push("/admin/settings")
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
            <UpdateSettingForm setValues={setValues} errors={errors} values={values} handleChange={handleChange} handleSubmit={handleSubmit}/>
        </>
    );
}

export default EditSetting;