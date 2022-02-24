import axios from "axios";

export const getAllRooms = async () => {
    return await axios.get("api/Rooms/GetAll",
        {
            headers: {
                'X-CSRF': '1'
            }
        });
};

export const removeRoom = async (id) =>
    await axios.delete(`api/Rooms/${id}`, {
        headers: {
            'X-CSRF': '1'
        }
    });

export const createRoom = async (values) =>
    await axios.post(`api/Rooms/Create`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const editRoom = async (values) =>
    await axios.post(`api/Rooms/Edit`, values,{
        headers: {
            'X-CSRF': '1'
        }
    });

export const getRoomById = async (id) =>
    await axios.get(`api/Rooms/GetRoomById/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });