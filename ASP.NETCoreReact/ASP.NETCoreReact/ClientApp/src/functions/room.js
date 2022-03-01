import axios from 'axios';
import { saveAs } from 'file-saver';

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

export const getDetail = async (id) =>
    await axios.get(`api/Checkin/Detail/${id}`,{
        headers: {
            'X-CSRF': '1'
        }
    });

export function downloadExcelReportRoom(id) {
    let instance = axios.create();
    let url = `api/Checkin/Excel/${id}`;
    let options = {
        url,
        method: 'get',
        responseType: 'blob' 
    };
    return instance.request(options)
        .then(response => {
            let filename = response.headers['content-disposition']
                .split(';')
                .find((n) => n.includes('filename='))
                .replace('filename=', '')
                .trim();
            let url = window.URL
                .createObjectURL(new Blob([response.data]));
            saveAs(url, filename);
        });
}