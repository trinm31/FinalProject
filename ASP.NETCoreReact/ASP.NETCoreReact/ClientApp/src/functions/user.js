import axios from "axios";

export const removeUser = async (id) =>
    await axios.delete(`${process.env.REACT_APP_BASE_API}/api/Users/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createUser = async (values) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/Users/CreateUser`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const editUser = async (values) =>
    await axios.put(`${process.env.REACT_APP_BASE_API}/api/Users/UpdateUser`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const getUser = async (id) =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/Users/GetUserById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getUserPersionalId = async (id) =>
    await axios.get(`${process.env.REACT_APP_BASE_API}/api/Users/GetUserPersionalId/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const uploadUser = async (file) =>
    await axios.post(`${process.env.REACT_APP_BASE_API}/api/Users/Upload`, file,{
        headers: {
            'content-type': 'multipart/form-data',
            'X-CSRF': '1'
        }
    });