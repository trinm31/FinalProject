import axios from "axios";

export const currentUser = async () => {
    return await axios.get(`${process.env.REACT_APP_BASE_API}/bff/user`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};