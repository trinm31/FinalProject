import React, { useEffect, useState } from "react";
import { useSelector }                from "react-redux";
import { toast }                      from "react-toastify";
import { getAllExams , removeCourse } from "../../../functions/exam";
import ListAllCourseTable             from "../../../components/tables/ListAllCourseTable";

const AllCourse = () => {
    const [courses, setCourses] = useState([]);
    const [filter, setFilter] = useState([]);
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
            setFilter(res.data);
            console.log(res.data);
            setLoading(false)}
        ).catch((err) => {
            setLoading(false);
            console.log(err);
        });
    }

    const handleSearch = e => {
        let target = e.target;
        if(e.target.value === ""){
            setFilter(courses)
        }

        let filteredData = courses.filter(x =>
            x.name.toLowerCase().includes(target.value)||
            x.examId.toLowerCase().includes(target.value) ||
            x.status === (target.value.toLowerCase() === "active") 
        )
        setFilter(filteredData);
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
        <>
            {loading ? (
                <h4 className="text-danger">Loading...</h4>
            ) : (
                <ListAllCourseTable handleSearch={handleSearch} courseLists={filter} handleRemove={handleRemove}/>
            )
            }
        </>
    );
}

export default AllCourse;