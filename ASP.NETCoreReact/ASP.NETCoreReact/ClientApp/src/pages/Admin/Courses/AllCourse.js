import React, { useEffect, useState } from "react";
import { useSelector }                from "react-redux";
import { toast }                      from "react-toastify";
import {paginationCourse , removeCourse } from "../../../functions/exam";
import ListAllCourseTable             from "../../../components/tables/ListAllCourseTable";

const AllCourse = () => {
    const [courses, setCourses] = useState([]);
    const [filter, setFilter] = useState([]);
    const [loading, setLoading] = useState(false);
    const [page, setPage] = useState(1);
    
    //redux
    const { user } = useSelector((state) => ({ ...state }));

    useEffect(() => {
        console.log(page)
        loadMoreCourses(page);
    }, [page]);
    
    const loadMoreCourses = (page) =>{
        setLoading(true);
        paginationCourse(page).then((res)=> {
            setCourses([...courses, ...res.data]);
            setFilter([...courses, ...res.data]);
            console.log(res.data);
            setLoading(false)}
        ).catch((err) => {
            setLoading(false);
            console.log(err);
        });
    }
    
    const scrollToEnd = () => {
        setPage(page + 1);
    }
    
    window.onscroll = function(){
        if((window.innerHeight + Math.ceil(window.pageYOffset + 1)) >= document.body.offsetHeight){
            scrollToEnd();
        }
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
                    loadMoreCourses(page);
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