import axios from "axios";

export const currentUser = async () => {
    return await axios.get("bff/user",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};