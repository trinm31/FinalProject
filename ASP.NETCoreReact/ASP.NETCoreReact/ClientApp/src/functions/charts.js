import axios from "axios";

export const getCharts = async () => {
    return await axios.get("api/Charts/Index",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};