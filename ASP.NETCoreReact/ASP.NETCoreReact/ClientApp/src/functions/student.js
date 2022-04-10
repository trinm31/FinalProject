import axios from "axios";

export const getAllStudents = async () => {
    return await axios.get("https://Trinm.com:80/api/Students/ListAllStudent",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const removeStudent = async (id) =>
    await axios.delete(`https://Trinm.com:80/api/Students/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudent = async (values) =>
    await axios.post(`https://Trinm.com:80/api/Students/Create`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const createStudentQr = async () =>
    await axios.get(`https://Trinm.com:80/api/Students/CreateQrCode`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const editStudent = async (values) =>
    await axios.put(`https://Trinm.com:80/api/Students/Edit`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const getStudent = async (id) =>
    await axios.get(`https://Trinm.com:80/api/Students/GetStudentById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const uploadStudent = async (file) =>
    await axios.post(`https://Trinm.com:80/api/Students/Upload`, file,{
        headers: {
            'content-type': 'multipart/form-data',
            'X-CSRF': '1'
        }
    });

export const paginationStudent = async (page) =>
    await axios.get(`https://Trinm.com:80/api/Students/StudentsPagination?PageNumber=${page}&PageSize=50`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getStudentQr = async (id) =>
    await axios.get(`https://Trinm.com:80/api/CheckQr/GetQrCode/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });