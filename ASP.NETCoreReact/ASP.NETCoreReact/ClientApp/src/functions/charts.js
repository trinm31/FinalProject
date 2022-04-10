import axios from "axios";

export const getCharts = async () => {
    return await axios.get("https://Trinm.com:80/api/Charts/Index",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};