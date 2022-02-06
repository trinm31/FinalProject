import React, { useEffect, useState } from "react";
import { Route } from "react-router-dom";
import { useSelector } from "react-redux";
import LoadingToRedirect from "./LoadingToRedirect";

const StudentRoute = ({ children, ...rest }) => {
    const { user } = useSelector((state) => ({ ...state }));
    const [ok, setOk] = useState(false);

    useEffect(() => {
        if (user && user._id) {
            if (user.role === "Student"){
                console.log("CURRENT Student RES");
                setOk(true);
            }else {
                console.log("ADMIN Student ERR");
                setOk(false);
            }
        }
    }, [user]);

    return ok ? <Route {...rest} /> : <LoadingToRedirect />;
};

export default StudentRoute;
