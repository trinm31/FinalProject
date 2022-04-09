import axios from "axios";

export const getAllSchedule = async () => {
    return await axios.get("http://Trinm.com:80/api/Schedule/GetAll",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const getScheduleByStudentId = async (id) => {
    return await axios.get(`http://Trinm.com:80/api/Schedule/GetByStudentId/${id}`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};