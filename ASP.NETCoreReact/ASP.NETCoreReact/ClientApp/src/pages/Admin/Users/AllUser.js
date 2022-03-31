import React , { useCallback , useEffect , useRef , useState } from "react";
import { toast }                                               from "react-toastify";
import useUser                                                 from "../../../Hooks/useUser";
import ListAllUserTable                                        from "../../../components/tables/ListAllUserTable";
import { removeUser }                                          from "../../../functions/user";
import Header                                                  from "../../../components/nav/Header";

const AllUser = () => {
    const [page , setPage] = useState(1);
    const [filter , setFilter] = useState([]);
    const [users, setUsers] = useState([]);
    const { isLoading , error , hasMore } = useUser(page , setFilter, setUsers);

    useEffect(()=>{console.log(users)},[users])

    const observer = useRef();
    const lastUserElementRef = useCallback(
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
            setFilter(users);
        }

        let filteredData = users.filter(x =>
            x.firstName.toLowerCase().includes(target.value) ||
            x.lastName.toLowerCase().includes(target.value)  ||
            x.phoneNumber.toLowerCase().includes(target.value) ||
            x.position.toLowerCase().includes(target.value) ||
            x.role.toLowerCase().includes(target.value)     ||
            x.persionalId.toLowerCase().includes(target.value)
        )
        setFilter(filteredData);
    }

    const handleRemove = ( id ) => {
        if (window.confirm("Do You Want To Delete This Item?")) {
            removeUser(id)
                .then(( res ) => {
                    let userList = users.filter(c=> c.id !== id);
                    setFilter(userList);
                    setUsers(userList);
                    toast.error("Item is deleted");
                })
                .catch(( err ) => {
                    if (err.response.status === 400) toast.error(err.response.data);
                    console.log(err);
                });
        }
    };

    return (
        <>
            <Header/>
            <ListAllUserTable lastUserElementRef={lastUserElementRef} handleSearch={handleSearch} userLists={filter} handleRemove={handleRemove}/>
            <div>{isLoading && "Loading..."}</div>
            <div>{error && "Error..."}</div>
        </>
    );
}

export default AllUser;