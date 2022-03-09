import axios from "axios";

export const removeUser = async (id) =>
    await axios.delete(`api/Users/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });
