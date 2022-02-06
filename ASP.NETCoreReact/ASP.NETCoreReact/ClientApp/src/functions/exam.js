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

export const createCourse = async (values) =>
    await axios.post(`api/Exams/Create`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const editCourse = async (values) =>
    await axios.post(`api/Exams/Edit`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getCourse = async (id) =>
    await axios.get(`api/Exams/GetExamById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });