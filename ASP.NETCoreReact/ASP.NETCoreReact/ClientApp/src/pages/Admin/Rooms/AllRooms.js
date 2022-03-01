import React , { useCallback , useEffect , useRef , useState } from "react";
import { toast }                                               from "react-toastify";
import { removeRoom, downloadExcelReportRoom }                                          from "../../../functions/room";
import useRoom                                               from "../../../Hooks/useRoom";
import ListAllRoomTable                                        from "../../../components/tables/ListAllRoomTable";

const AllRooms = () => {
    const [page , setPage] = useState(1);
    const [filter , setFilter] = useState([]);
    const [rooms, setRooms] = useState([]);
    const { isLoading , error , hasMore } = useRoom(page , setFilter, setRooms);


    const observer = useRef();
    const lastRoomElementRef = useCallback(
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
            setFilter(rooms);
        }

        let filteredData = rooms.filter(x =>
            x.name.toLowerCase().includes(target.value) ||
            x.courseId.toLowerCase().includes(target.value)
        )
        setFilter(filteredData);
    }

    const handleRemove = ( id ) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeRoom(id)
                .then(( res ) => {
                    let roomList = rooms.filter(c=> c.id !== id);
                    setFilter(roomList);
                    setRooms(roomList);
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
            <ListAllRoomTable download={downloadExcelReportRoom} lastRoomElementRef={lastRoomElementRef} handleSearch={handleSearch} roomLists={filter} handleRemove={handleRemove}/>
            <div>{isLoading && "Loading..."}</div>
            <div>{error && "Error..."}</div>
        </>
    );
}

export default AllRooms;