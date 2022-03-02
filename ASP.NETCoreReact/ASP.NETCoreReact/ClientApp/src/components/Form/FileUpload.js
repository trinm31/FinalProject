import React    from "react";
import { Link } from "react-router-dom";

const FileUpload = ({downloadFileTemplate , handleSubmit, saveFile, fileName}) =>{
    return(
        <div className="min-h-screen flex items-center justify-center bg-light">
            <div className="bg-white p-16 rounded shadow-2xl w-2/3">
                <div className="mb-2">
                    <h2 className="text-3xl font-bold mb-10 text-gray-800">Upload file</h2>
                    <Link className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 mx-2 rounded" to={downloadFileTemplate} target="_blank" download>File Template</Link>
                </div>
                
                <form onSubmit={handleSubmit} >
                    <div className="border border-dashed border-gray-500 relative">
                        <input type="file" multiple
                               className="cursor-pointer relative block opacity-0 w-full h-full p-20 z-50"
                               onChange={saveFile}/>
                        <div className="text-center p-10 absolute top-0 right-0 left-0 m-auto">
                            {!fileName?(
                                <div>
                                    <h4>
                                        Drop files anywhere to upload
                                        <br/>or
                                    </h4>
                                    <p className="">Select Files</p>
                                </div>
                            ):(
                                <h1>{fileName}</h1>
                                )
                            }
                        </div>
                    </div>

                    <button
                        className="block w-full bg-yellow-400 hover:bg-yellow-300 p-4 rounded text-yellow-900 hover:text-yellow-800 transition duration-300 mt-3" type="submit">Submit
                    </button>
                </form>
            </div>
        </div>
    );
}

export default FileUpload;