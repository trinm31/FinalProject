import axios from "axios";

export const removeUser = async (id) =>
    await axios.delete(`api/Users/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createUser = async (values) =>
    await axios.post(`api/Users/CreateUser`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });