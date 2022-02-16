import axios from "axios";

export const getAllStudents = async () => {
    return await axios.get("api/Students/ListAllStudent",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const removeStudent = async (id) =>
    await axios.delete(`api/Students/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudent = async (values) =>
    await axios.post(`api/Students/Create`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudentQr = async () =>
    await axios.get(`api/Students/CreateQrCode`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const editStudent = async (values) =>
    await axios.put(`api/Students/Edit`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const getStudent = async (id) =>
    await axios.get(`api/Students/GetStudentById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const uploadStudent = async (file) =>
    await axios.post(`api/Students/Upload`, file,{
        headers: {
            'content-type': 'multipart/form-data',
            'X-CSRF': '1'
        }
    });

export const paginationStudent = async (page) =>
    await axios.get(`api/Students/StudentsPagination?PageNumber=${page}&PageSize=50`,{
        headers: {
            'X-CSRF': '1'
        }
    });