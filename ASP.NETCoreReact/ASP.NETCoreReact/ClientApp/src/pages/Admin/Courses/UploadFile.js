import React, {useState, useEffect} from 'react';
import FileUpload                   from '../../../components/Form/FileUpload';
import {uploadCourse}               from '../../../functions/exam';
import { toast }                    from "react-toastify";
import { saveAs }                   from "file-saver";
import Header                       from "../../../components/nav/Header";

const UploadFile = ({history}) => {
    const [file, setFile] = useState();
    const [fileName, setFileName] = useState();
    
    const saveFile=(e)=>{
        setFile(e.target.files[0]);
        setFileName(e.target.files[0].name)
    }
    
    useEffect(()=>{
        console.log(file);
        console.log(fileName);
    },[file])
    
    const handleSubmit = (e) =>{
        e.preventDefault();
        const formData = new FormData();
        formData.append('formFile', file);
        formData.append('fileName', fileName);
        
        uploadCourse(formData).then((res)=>{
            console.log(res);
            toast.success("File Uploaded");
            history.push("/admin/courses");
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
    }
    
    const downloadFileTemplate = "/Static/CourseExcelTemplate.xlsx";
    
    return (
        <>
            <Header/>
            <FileUpload downloadFileTemplate={downloadFileTemplate} handleSubmit={handleSubmit} saveFile={saveFile} fileName={fileName}/>
        </>
    );
}

export default UploadFile;