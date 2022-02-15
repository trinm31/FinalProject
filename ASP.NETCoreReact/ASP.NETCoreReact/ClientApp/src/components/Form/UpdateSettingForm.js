import React          from "react";
import DateTimePicker from 'react-datetime-picker';

const UpdateSettingForm = ( {handleSubmit, handleChange, values, setValues, errors}) => {

    const 
        {
        id,
        startDate,
        endDate,
        concurrencyLevelDefault ,
        externalDistance,
        internalDistance,
        noOfTimeSlot
        } = values;

    return(
        <div className="min-h-screen flex items-center justify-center bg-light">
            <div className="bg-white p-16 rounded shadow-2xl w-2/3  my-20">

                <h2 className="text-3xl font-bold mb-10 text-gray-800">Edit Setting</h2>

                <form className="space-y-5" onSubmit={handleSubmit}>
                    
                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Start Date</label>
                        <div className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500">
                            <DateTimePicker onChange={(d)=> setValues({...values, startDate: new Date(d)})}  value={startDate} className="w-full p-2 border-none"  />
                        </div>
                        {
                            errors.startDate.length > 0 && <span className="text-red-500">{errors.startDate}</span>
                        }
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">End Date</label>
                        <div className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500">
                            <DateTimePicker onChange={(d)=> setValues({...values, endDate: new Date(d)})}  value={endDate} className="w-full p-2 border-none"  />
                        </div>
                        {
                            errors.endDate.length > 0 && <span className="text-red-500">{errors.endDate}</span>
                        }
                    </div>
                    
                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Concurrency Level</label>
                        <input type="text" onChange={handleChange} name="concurrencyLevelDefault" value={concurrencyLevelDefault}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {
                            errors.concurrencyLevelDefault.length > 0 && <span className="text-red-500">{errors.concurrencyLevelDefault}</span>
                        }
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">External Distance</label>
                        <input type="text" onChange={handleChange} name="externalDistance" value={externalDistance}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {
                            errors.externalDistance.length > 0 && <span className="text-red-500">{errors.externalDistance}</span>
                        }
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Internal Distance</label>
                        <input type="text" onChange={handleChange} name="internalDistance" value={internalDistance}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {
                            errors.internalDistance.length > 0 && <span className="text-red-500">{errors.internalDistance}</span>
                        }
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Number Slot In A Day</label>
                        <input type="text" onChange={handleChange} name="noOfTimeSlot" value={noOfTimeSlot}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {
                            errors.noOfTimeSlot.length > 0 && <span className="text-red-500">{errors.noOfTimeSlot}</span>
                        }
                    </div>

                    <button
                        className="block w-full bg-yellow-400 hover:bg-yellow-300 p-4 rounded text-yellow-900 hover:text-yellow-800 transition duration-300">Submit
                    </button>
                </form>
            </div>
        </div>
    );
}

export default UpdateSettingForm;