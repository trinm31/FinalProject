import axios from "axios";

export const getAllExams = async () => {
    return await axios.get("api/Exams/GetAllExam",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const removeCourse = async (id) =>
    await axios.delete(`api/Exams/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });