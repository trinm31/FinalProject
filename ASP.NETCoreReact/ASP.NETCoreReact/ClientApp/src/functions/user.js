import axios from "axios";

export const removeUser = async (id) =>
    await axios.delete(`https://Trinm.com:80/api/Users/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createUser = async (values) =>
    await axios.post(`https://Trinm.com:80/api/Users/CreateUser`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const editUser = async (values) =>
    await axios.put(`https://Trinm.com:80/api/Users/UpdateUser`, values,{
        headers: {
            'content-type': 'application/json',
            'X-CSRF': '1'
        }
    });

export const getUser = async (id) =>
    await axios.get(`https://Trinm.com:80/api/Users/GetUserById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getUserPersionalId = async (id) =>
    await axios.get(`https://Trinm.com:80/api/Users/GetUserPersionalId/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const uploadUser = async (file) =>
    await axios.post(`https://Trinm.com:80/api/Users/Upload`, file,{
        headers: {
            'content-type': 'multipart/form-data',
            'X-CSRF': '1'
        }
    });