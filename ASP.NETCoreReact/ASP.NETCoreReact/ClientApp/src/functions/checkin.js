import axios from "axios";

export const checkIn = async (studentId) => {
    return await axios.get(`api/Checkin/CheckIn/${studentId}`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const checkInConfirm = async (studentId, roomId) => {
    return await axios.get(`api/Checkin/CheckInConfirm/${studentId}/${roomId}`,
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};