import axios from "axios";

export const getAllStudents = async () => {
    return await axios.get(`${process.env.REACT_APP_BASE_API}/api/Students/ListAllStudent`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const removeStudent = async (id) =>
    await axios.delete(`${process.env.REACT_APP_BASE_API}/api/Students/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudent = async (values) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/Students/Create`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudentQr = async () =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/Students/CreateQrCode`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const editStudent = async (values) =>
    await axios.put(`${process.env.REACT_APP_BASE_API}/api/Students/Edit`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const getStudent = async (id) =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/Students/GetStudentById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const uploadStudent = async (file) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/Students/Upload`, file,{
        headers: {
            'content-type': 'multipart/form-data',
            'X-CSRF': '1'
        }
    });

export const paginationStudent = async (page) =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/Students/StudentsPagination?PageNumber=${page}&PageSize=50`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getStudentQr = async (id) =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/CheckQr/GetQrCode/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });