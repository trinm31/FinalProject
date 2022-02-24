import React from "react";

const UpSertRoomForm = ( {handleSubmit, handleChange, values, errors}) => {
    
    const {name, courseId, id} = values;
    
    return(
        <div className="min-h-screen flex items-center justify-center bg-light">
            <div className="bg-white p-16 rounded shadow-2xl w-2/3">

                <h2 className="text-3xl font-bold mb-10 text-gray-800">{values.id !== "" ? "Edit" :"Create"} Room</h2>

                <form className="space-y-5" onSubmit={handleSubmit}>
                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Room Name</label>
                        <input type="text" onChange={handleChange} name="name" value={name}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {
                            errors.name.length > 0 && <span className="text-red-500">{errors.name}</span>
                        }
                    </div>
                    {
                        !id && (
                            <div>
                                <label className="block mb-1 font-bold text-gray-500">Course Id</label>
                                <input type="text" onChange={handleChange} name="courseId" value={courseId}
                                       className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                                {
                                    errors.courseId.length > 0 && <span className="text-red-500">{errors.courseId}</span>
                                }
                            </div>
                        )
                    }
                    <button
                        className="block w-full bg-yellow-400 hover:bg-yellow-300 p-4 rounded text-yellow-900 hover:text-yellow-800 transition duration-300">Submit
                    </button>
                </form>
            </div>
        </div>
    );
}

export default UpSertRoomForm;