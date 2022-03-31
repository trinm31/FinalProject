import React , { useCallback , useEffect , useRef , useState } from 'react'
import OptionsIcon                                             from "./Option";
import useUser                                                 from "../../../Hooks/useUser";
import { Link }                                                from "react-router-dom";

function User() {
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
    
    return (
        <>
            <div className="p-6 bg-white shadow-sm  rounded-lg">
                <div className="flex justify-between items-center pb-4">
                    <h2 className="text-xl font-semibold leading-loose text-gray">List User</h2>
                    <button className="flex py-3 px-4 rounded-lg border border-gray-700 gap-x-2.5">
                        <OptionsIcon />
                        <span className="text-sm text-gray">Filter user</span>
                    </button>
                </div>
                <table className="w-full">
                    <thead className="bg-gray-50 border-b-2 border-gray-200">
                    <tr>
                        <th className="w-20 p-3 text-sm font-semibold tracking-wide text-left">PersionalId</th>
                        <th className="w-54 p-3 text-sm font-semibold tracking-wide text-left">LastName</th>
                        <th className="w-34 p-3 text-sm font-semibold tracking-wide text-left">FirstName</th>
                        <th className="w-44 p-3 text-sm font-semibold tracking-wide text-left">Position</th>
                        <th className="w-44 p-3 text-sm font-semibold tracking-wide text-left">PhoneNumber</th>
                        <th className="w-44 p-3 text-sm font-semibold tracking-wide text-left">Role</th>
                    </tr>
                    </thead>
                    {
                        filter.map(( user , i ) => {
                            if (user.length === i + 1) {
                                return (
                                    <tbody key={i} className="divide-y divide-gray-100" ref={lastUserElementRef}>
                                    <tr className="bg-white" key={i}>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <a className="font-bold text-blue-500 hover:underline">{user.persionalId}</a>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.lastName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.firstName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.position}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.phoneNumber}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.role}</td>
                                    </tr>
                                    </tbody>
                                );
                            } else {
                                return (
                                    <tbody key={i} className="divide-y divide-gray-100" ref={lastUserElementRef}>
                                    <tr className="bg-white" key={i}>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <a className="font-bold text-blue-500 hover:underline">{user.persionalId}</a>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.lastName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                            <div>
                                                {user.firstName}
                                            </div>
                                        </td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.position}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.phoneNumber}</td>
                                        <td className="p-3 text-sm text-gray-700 whitespace-nowrap">{user.role}</td>
                                    </tr>
                                    </tbody>
                                );
                            }
                        })
                    }

                </table>
            </div>
            <div>{isLoading && "Loading..."}</div>
            <div>{error && "Error..."}</div>
        </>
    )
}

export default User