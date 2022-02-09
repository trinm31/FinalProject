import React from "react";
import Resizer from "react-image-file-resizer";

const UpSertStudentForm = ( {handleSubmit, handleChange, values, setValues}) => {
    
    const {name, studentId, email, avatar} = values;

    const fileUploadAndResize = (e) => {
        let files = e.target.files; // 3

        if (files) {
            for (let i = 0; i < files.length; i++) {
                Resizer.imageFileResizer(
                    files[i],
                    720,
                    720,
                    "JPEG",
                    80,
                    0,
                    (uri) => {
                        setValues({ ...values, avatar: uri })
                    },
                    "base64"
                );
            }
        }
    };
    
    return(
        <div className="min-h-screen flex items-center justify-center bg-light">
            <div className="bg-white p-16 rounded shadow-2xl w-2/3">

                <h2 className="text-3xl font-bold mb-10 text-gray-800">Create New Stdent</h2>

                <form className="space-y-5" onSubmit={handleSubmit}>
                    <div>
                        <label className="block mb-1 font-bold text-gray-500">StudentName</label>
                        <input type="text" onChange={handleChange} name="name" value={name}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Student Id</label>
                        <input type="text" onChange={handleChange} name="studentId" value={studentId}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Email</label>
                        <input type="text" onChange={handleChange} name="email" value={email}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                    </div>

                    <div>
                        <label className="block text-sm font-medium text-gray-700">Avatar photo</label>
                        <div className="mt-1 flex justify-center px-6 pt-5 pb-6 border-2 border-gray-300 border-dashed rounded-md">
                            <div className="space-y-1 text-center">
                                <svg
                                    className="mx-auto h-12 w-12 text-gray-400"
                                    stroke="currentColor"
                                    fill="none"
                                    viewBox="0 0 48 48"
                                    aria-hidden="true"
                                >
                                    <path
                                        d="M28 8H12a4 4 0 00-4 4v20m32-12v8m0 0v8a4 4 0 01-4 4H12a4 4 0 01-4-4v-4m32-4l-3.172-3.172a4 4 0 00-5.656 0L28 28M8 32l9.172-9.172a4 4 0 015.656 0L28 28m0 0l4 4m4-24h8m-4-4v8m-12 4h.02"
                                        strokeWidth={2}
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                    />
                                </svg>
                                <div className="flex text-sm text-gray-600">
                                    <label
                                        htmlFor="file-upload"
                                        className="relative cursor-pointer bg-white rounded-md font-medium text-indigo-600 hover:text-indigo-500 focus-within:outline-none focus-within:ring-2 focus-within:ring-offset-2 focus-within:ring-indigo-500"
                                    >
                                        <span>Upload a file</span>
                                        <input type="file"
                                            multiple
                                            accept="images/*"
                                            onChange={fileUploadAndResize} id="file-upload" name="file-upload" className="sr-only" />
                                    </label>
                                    <p className="pl-1">or drag and drop</p>
                                </div>
                                <p className="text-xs text-gray-500">PNG, JPG, GIF up to 10MB</p>
                            </div>
                        </div>
                    </div>

                    <button
                        className="block w-full bg-yellow-400 hover:bg-yellow-300 p-4 rounded text-yellow-900 hover:text-yellow-800 transition duration-300">Submit
                    </button>
                </form>
            </div>
        </div>
    );
}

export default UpSertStudentForm;