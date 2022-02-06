import React, { useEffect, useState } from "react";
import { useSelector }                from "react-redux";
import { toast }                      from "react-toastify";
import { getAllExams , removeCourse } from "../../functions/exam";
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

    const handleRemove = (id) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeCourse(id)
                .then((res) => {
                    loadAllCourses();
                    toast.error(`Item is deleted`);
                })
                .catch((err) => {
                    if (err.response.status === 400) toast.error(err.response.data);
                    console.log(err);
                });
        }
    };
    
    return (
        <ListAllCourseTable courseLists={courses} handleRemove={handleRemove}/>
    );
}

export default AllCourse;