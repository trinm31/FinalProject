import axios from "axios";

export const getAllStudentExam = async () => {
    return await axios.get(`${process.env.REACT_APP_BASE_API}/api/StudentExams/GetAllStudentExam`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const removeStudentExam = async (id) =>
    await axios.delete(`${process.env.REACT_APP_BASE_API}/api/StudentExams/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudentExam = async (values) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/StudentExams/Create`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const editStudentExam = async (values) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/StudentExams/Edit`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getStudentExam = async (id) =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/StudentExams/GetStudentExamById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const uploadStudentExam = async (file) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/StudentExams/Upload`, file,{
        headers: {
            'content-type': 'multipart/form-data',
            'X-CSRF': '1'
        }
    });

export const paginationStudentExam = async (page) =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/StudentExams/StudentExamPagination?PageNumber=${page}&PageSize=25`,{
        headers: {
            'X-CSRF': '1'
        }
    });