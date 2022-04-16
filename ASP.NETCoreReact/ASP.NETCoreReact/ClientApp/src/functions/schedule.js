import axios from "axios";

export const getAllSchedule = async () => {
    return await axios.get(`${process.env.REACT_APP_BASE_API}/api/Schedule/GetAll`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const getScheduleByStudentId = async (id) => {
    return await axios.get(`${process.env.REACT_APP_BASE_API}/api/Schedule/GetByStudentId/${id}`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};