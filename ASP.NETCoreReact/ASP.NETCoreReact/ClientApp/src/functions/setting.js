import axios from "axios";

export const getSetting = async () => {
    return await axios.get("api/Setting/GetSetting",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const editSetting = async (values) =>
    await axios.post(`api/Setting/Update`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });