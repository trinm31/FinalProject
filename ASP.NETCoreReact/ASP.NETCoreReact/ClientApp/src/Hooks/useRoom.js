import React, { useState, useEffect } from "react";
import axios from "axios";

function useRoom( pageNum, setFilter, setRooms) {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(false);
   
    const [hasMore, setHasMore] = useState(false);
    
    useEffect(() => {
        const CancelToken = axios.CancelToken;
        let cancel;

        setIsLoading(true);
        setError(false);
 
        axios
            .get(`api/Rooms/RoomsPagination?PageNumber=${pageNum}&PageSize=50`, {
                cancelToken: new CancelToken((c) => (cancel = c))
            })
            .then((res) => {
                setRooms(( prev) => {
                    return [...new Set([...prev, ...res.data])];
                });
                setFilter(( prev) => {
                    return [...new Set([...prev, ...res.data])];
                });
                setHasMore(res.data.length > 0);
                setIsLoading(false);
            })
            .catch((err) => {
                if (axios.isCancel(err)) return;
                console.log(err);
                setError(err);
            });

        return () => cancel();
    }, [pageNum]);

    return { isLoading, error, hasMore };
}

export default useRoom;
