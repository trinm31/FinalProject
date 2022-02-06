import React, { useEffect, useState } from "react";
import { useSelector }                from "react-redux";
import { toast }                      from "react-toastify";
import {getAllExams}                  from "../../functions/exam";
import ListAllCourseTable             from "../../components/tables/ListAllCourseTable";

const AllCourse = () => {
    const [courses, setCourses] = useState([]);
    const [loading, setLoading] = useState(false);
    
    //redux
    const { user } = useSelector((state) => ({ ...state }));

    useEffect(() => {
        loadAllCourses();
    }, []);
    
    const loadAllCourses = () =>{
        setLoading(true);
        getAllExams().then((res)=> {
            setCourses(res.data);
            console.log(res.data);
            setLoading(false)}
        ).catch((err) => {
            setLoading(false);
            console.log(err);
        });
    }
    
    return (
        <ListAllCourseTable courseLists={courses}/>
    );
}

export default AllCourse;