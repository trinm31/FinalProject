import React , {useEffect, useState } from "react";
import { getAllSchedule }             from "../../../functions/schedule";
import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';

const SchedulePage = () => {
    const [events, setEvents] = useState([]);

    useEffect(()=>{
        loadEvents();
    },[]);
    
    const loadEvents = () =>{
        getAllSchedule().then((res)=>{
            setEvents(res.data);
        }).catch((err) =>{
            console.log(err);
        })
    }

    return (
        <>
            <div>
                <FullCalendar
                    plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
                    initialView="dayGridMonth"
                    headerToolbar={{
                        center: 'dayGridMonth,timeGridWeek,timeGridDay',
                    }}
                    events={events}
                />
            </div>
        </>
    );
}

export default SchedulePage;