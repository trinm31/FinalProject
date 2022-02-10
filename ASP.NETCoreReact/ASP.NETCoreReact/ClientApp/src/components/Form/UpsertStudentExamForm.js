import React from "react";
import Select from 'react-select';

const UpSertStudentExamForm = ( {handleSubmit, values, setValues, students, exams}) => {
    
    
    return(
        <div className="min-h-screen flex items-center justify-center bg-light">
            <div className="bg-white p-16 rounded shadow-2xl w-2/3">

                <h2 className="text-3xl font-bold mb-10 text-gray-800">Create New Student Exam </h2>

                <form className="space-y-5" onSubmit={handleSubmit}>
                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Exam Id</label>
                        <Select onChange={opt => setValues({...values, examId: opt.value})} name="examId" value={{ label: values.examId, value: values.examId }} options={exams.map(opt => ({ label: opt.examId, value: opt.examId }))}
                        />
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Student Id</label>
                        <Select onChange={opt => setValues({...values, studentId: opt.value})} name="examId" value={{ label: values.studentId, value: values.studentId }} options={students.map(opt => ({ label: opt.studentId, value: opt.studentId }))}
                        />
                    </div>

                    <button
                        className="block w-full bg-yellow-400 hover:bg-yellow-300 p-4 rounded text-yellow-900 hover:text-yellow-800 transition duration-300">Submit
                    </button>
                </form>
            </div>
        </div>
    );
}

export default UpSertStudentExamForm;