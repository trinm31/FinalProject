import axios from "axios";

export const getCharts = async () => {
    return await axios.get(`${process.env.REACT_APP_BASE_API}/api/Charts/Index`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};