import axios from "axios";

export const getAllSchedule = async () => {
    return await axios.get("api/Schedule/GetAll",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};