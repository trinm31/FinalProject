import axios from "axios";

export const getAllStudentExam = async () => {
    return await axios.get("api/StudentExams/GetAllStudentExam",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const removeStudentExam = async (id) =>
    await axios.delete(`api/StudentExams/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudentExam = async (values) =>
    await axios.post(`api/StudentExams/Create`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const editStudentExam = async (values) =>
    await axios.post(`api/StudentExams/Edit`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getStudentExam = async (id) =>
    await axios.get(`api/StudentExams/GetStudentExamById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const uploadStudentExam = async (file) =>
    await axios.post(`api/StudentExams/Upload`, file,{
        headers: {
            'content-type': 'multipart/form-data',
            'X-CSRF': '1'
        }
    });