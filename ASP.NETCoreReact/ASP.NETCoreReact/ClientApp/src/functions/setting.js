import axios from "axios";

export const getSetting = async () => {
    return await axios.get("https://Trinm.com:80/api/Setting/GetSetting",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const editSetting = async (values) =>
    await axios.post(`https://Trinm.com:80/api/Setting/Update`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const generateSchedule = async () =>
    await axios.get(`https://Trinm.com:80/api/SchedulingGenerate`,{
        headers: {
            'X-CSRF': '1'
        }
    });