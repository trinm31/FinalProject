import React from 'react'
import DashboardIcon from '@material-ui/icons/Dashboard';
import CropLandscapeIcon from '@material-ui/icons/CropLandscape';
import AppsIcon from '@material-ui/icons/Apps';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import { useDispatch , useSelector }      from "react-redux";

const Header = () => {

    let { user } = useSelector(( state ) => (
        { ...state }));

    const logout = () => {

        dispatch({
            type: "LOGOUT" ,
            payload: null ,
        });

        window.location.assign("/bff/login")
    };
    
    return (
        <div className="flex shadow-sm bg-gray-50  p-4 justify-between  ">
            <div className="flex space-x-3  ">
                <p className="text-gray-400">Adress </p>
                <p>Ngo Si Lien - Quan Lien Chieu - Thanh Pho Da Nang</p>
                <CropLandscapeIcon className="text-gray-300" />
                <DashboardIcon className="text-gray-300" />

            </div>
            <div className="flex space-x-4 text-gray-400 mr-3">

                <AppsIcon />
                <ExitToAppIcon />
                {user && (
                    <a href={user.logoutUrl.value} className="text-gray-600 font-semibold" onClick={logout}>Log Out</a>
                )
                }
                {!user && (
                    
                    <a href="/bff/login" className="text-gray-600 font-semibold">Log In</a>
                )
                }
                {/*<p className="text-gray-600 font-semibold">Close</p>*/}
            </div>
        </div>
    )
}

export default Header
