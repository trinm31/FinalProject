import axios from "axios";

export const currentUser = async () => {
    return await axios.get("http://Trinm.com:80/bff/user",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};