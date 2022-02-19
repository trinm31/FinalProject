import React, { useState, useEffect } from "react";
import axios from "axios";

function useSearchBook(pageNum) {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(false);
    const [courses, setCourses] = useState([]);
    const [hasMore, setHasMore] = useState(false);

    useEffect(() => {
        setCourses([]);
    }, []);

    useEffect(() => {
        const CancelToken = axios.CancelToken;
        let cancel;

        setIsLoading(true);
        setError(false);

        axios
            .get(`api/Exams/StudentsPagination?PageNumber=${pageNum}&PageSize=50`, {
                cancelToken: new CancelToken((c) => (cancel = c))
            })
            .then((res) => {
                setCourses(( prev) => {
                    return [...new Set([...prev, ...res.data])];
                });
                setHasMore(res.data.length > 0);
                setIsLoading(false);
            })
            .catch((err) => {
                if (axios.isCancel(err)) return;
                setError(err);
            });

        return () => cancel();
    }, [pageNum]);

    return { isLoading, error, courses, hasMore };
}

export default useSearchBook;
