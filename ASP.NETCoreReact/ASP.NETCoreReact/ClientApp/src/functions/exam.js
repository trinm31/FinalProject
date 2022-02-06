import axios from "axios";

export const getAllExams = async () => {
    return await axios.get("api/Exams/GetAllExam",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};