import React , { useCallback , useEffect , useRef , useState } from "react";
import { toast }                                               from "react-toastify";
import { removeCourse }                                        from "../../../functions/exam";
import ListAllCourseTable  from "../../../components/tables/ListAllCourseTable";
import useCourse           from "../../../Hooks/useCourse.js";

const AllCourse = () => {
    const [page , setPage] = useState(1);
    const [filter , setFilter] = useState([]);
    const [courses, setCourses] = useState([]);
    const { isLoading , error , hasMore } = useCourse(page , setFilter, setCourses);
    

    const observer = useRef();
    const lastCourseElementRef = useCallback(
        ( node ) => {
            if (isLoading) return;
            if (observer.current) observer.current.disconnect();
            observer.current = new IntersectionObserver(( entries ) => {
                if (entries[0].isIntersecting && hasMore) {
                    setPage(( prev ) => prev + 1);
                }
            });
            if (node) observer.current.observe(node);
        } ,
        [isLoading , hasMore]
    );

    const handleSearch = e => {
        let target = e.target;
        if (e.target.value === "") {
            setFilter(courses);
        }

        let filteredData = courses.filter(x =>
            x.name.toLowerCase().includes(target.value) ||
            x.examId.toLowerCase().includes(target.value)
        )
        setFilter(filteredData);
    }

    const handleRemove = ( id ) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeCourse(id)
                .then(( res ) => {
                    let courseList = courses.filter(c=> c.id !== id);
                    setFilter(courseList);
                    setCourses(courseList);
                    toast.error(`Item is deleted`);
                })
                .catch(( err ) => {
                    if (err.response.status === 400) toast.error(err.response.data);
                    console.log(err);
                });
        }
    };

    return (
        <>
            <ListAllCourseTable lastCourseElementRef={lastCourseElementRef} handleSearch={handleSearch} courseLists={filter} handleRemove={handleRemove}/>
            <div>{isLoading && "Loading..."}</div>
            <div>{error && "Error..."}</div>
        </>
    );
}

export default AllCourse;