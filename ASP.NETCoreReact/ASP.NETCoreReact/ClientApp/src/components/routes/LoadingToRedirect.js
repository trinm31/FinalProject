import React , { useState , useEffect } from 'react';
import { useHistory }                   from "react-router-dom";

const LoadingToRedirect = () => {
    const [count , setCount] = useState(10);
    let history = useHistory();

    useEffect(() => {
        const interval = setInterval(() => {
            setCount(( currentCount ) => --currentCount)
        } , 1000);

        count === 0 && history.push("/");

        return () => clearInterval(interval);
    } , [count , history]);

    return (
        // <div className="container p-5 text-center"><h1>Redirecting you in {count} seconds</h1></div>
        <div
            className="
    flex
    items-center
    justify-center
    w-screen
    h-screen
    bg-gray-400
  "
        >
            <div className="px-40 py-20 bg-grey rounded-md shadow-xl">
                <div className="flex flex-col items-center">
                    <h1 className="font-bold text-blue-600 text-9xl">404</h1>

                    <h6 className="mb-2 text-2xl font-bold text-center text-white md:text-3xl">
                        <span className="text-red-500">Oops!</span> Page not found
                    </h6>

                    <p className="mb-8 text-center text-white-500 md:text-lg">
                        The page you’re looking for doesn’t exist.
                    </p>

                    <div
                        className="px-6 rounded-lg py-2 text-sm font-semibold text-white bg-red-800"
                    ><h1>Redirecting you in {count} seconds</h1></div
                    >
                </div>
            </div>
        </div>);
}
export default LoadingToRedirect;