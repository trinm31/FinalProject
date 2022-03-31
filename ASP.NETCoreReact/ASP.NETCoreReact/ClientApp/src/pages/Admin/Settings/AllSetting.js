import React, { useEffect, useState } from "react";
import { getSetting }                 from "../../../functions/setting";
import ListAllSettingTable            from "../../../components/tables/ListAllSettingTable";
import Header                         from "../../../components/nav/Header";

const AllSetting = () => {
    const [setting, setSetting] = useState({});
    const [loading, setLoading] = useState(false);
    
    useEffect(() => {
        loadAllSetting();
    }, []);
    
    const loadAllSetting = () =>{
        setLoading(true);
        getSetting().then((res)=> {
            setSetting(res.data);
            console.log(res.data);
            setLoading(false)}
        ).catch((err) => {
            setLoading(false);
            console.log(err);
        });
    }
    
    return (
        <>
            {loading ? (
                <h4 className="text-danger">Loading...</h4>
            ) : (
                <>
                    <Header/>
                    <ListAllSettingTable setting={setting} />
                </>
            )
            }
        </>
    );
}

export default AllSetting;