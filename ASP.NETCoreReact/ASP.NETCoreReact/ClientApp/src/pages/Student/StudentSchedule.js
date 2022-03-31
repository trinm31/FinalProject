import React , { useEffect , useState } from "react";
import { toast }                        from "react-toastify";
import FullCalendar                     from '@fullcalendar/react';
import dayGridPlugin                    from '@fullcalendar/daygrid';
import timeGridPlugin                   from '@fullcalendar/timegrid';
import interactionPlugin                from '@fullcalendar/interaction';
import { getScheduleByStudentId }       from "../../functions/schedule";
import Header                           from "../../components/nav/Header";

const StudentSchedule = ({match}) => {

    const [events, setEvents] = useState([]);

    const { id } = match.params;

    useEffect(()=>{
        loadEvents();
    },[]);

    const loadEvents = () =>{
        getScheduleByStudentId(id).then((res)=>{
            setEvents(res.data);
        }).catch((err) =>{
            console.log(err);
        })
    }
    
    return(
        <>
            <Header/>
            <div className="p-10 shadow rounded-3">
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

export default StudentSchedule;
