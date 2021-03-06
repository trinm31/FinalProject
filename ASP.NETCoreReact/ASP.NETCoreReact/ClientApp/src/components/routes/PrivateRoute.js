import React , { useEffect , useState } from "react";
import { Route }                        from "react-router-dom";
import { useSelector }                  from "react-redux";
import LoadingToRedirect                from "./LoadingToRedirect";

const PrivateRoute = ( { children , ...rest } ) => {
        const { user } = useSelector(( state ) => (
            { ...state }));
        const [ok , setOk] = useState(false);

        useEffect(() => {
                if (user && user._id) {
                    setOk(true);
                } else {
                    setOk(false);
                }
            } , [user]
        )
        ;

        return ok ? <Route {...rest} /> : <LoadingToRedirect/>;
    }
;

export default PrivateRoute;
