import React, { useEffect, useState } from "react";
import { Route } from "react-router-dom";
import { useSelector } from "react-redux";
import LoadingToRedirect from "./LoadingToRedirect";

const StaffRoute = ({ children, ...rest }) => {
    const { user } = useSelector((state) => ({ ...state }));
    const [ok, setOk] = useState(false);

    useEffect(() => {
        if (user && user._id) {
            if (user.role.value === "Staff"){
                console.log("CURRENT Staff RES");
                setOk(true);
            }else {
                console.log("ADMIN Staff ERR");
                setOk(false);
            }
        }
    }, [user]);

    return ok ? <Route {...rest} /> : <LoadingToRedirect />;
};

export default StaffRoute;
