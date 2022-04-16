import axios from "axios";

export const getSetting = async () => {
    return await axios.get(`${process.env.REACT_APP_BASE_API}/api/Setting/GetSetting`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const editSetting = async (values) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/Setting/Update`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const generateSchedule = async () =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/SchedulingGenerate`,{
        headers: {
            'X-CSRF': '1'
        }
    });