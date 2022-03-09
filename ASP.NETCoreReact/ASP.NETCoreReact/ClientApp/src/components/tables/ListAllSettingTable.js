import React                from "react";
import { Link }             from "react-router-dom";
import { removeCourse }     from "../../functions/exam";
import { toast }            from "react-toastify";
import { generateSchedule } from "../../functions/setting";

const ListAllSettingTable = ( {setting}) => {
    
    const handleGenerate = () => {
        if (window.confirm("Wait about one hour to get the results and do not click 2 times")) {
            generateSchedule().then(
                (res)=>{
                    toast.success("Generating Scheduling please wait");
                    history.push("/admin/settings")
                }
            ).catch((err)=>{
                console.log(err);
            });
        }
    };

    const dateFormatter = (day) => {
        const d = new Date(day);
        var datestring =
            d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
        return datestring;
    };
    
    return (
        <div className="p-5 h-screen bg-gray-100">
            <div className="grid grid-cols-2">
                <div className="flex justify-start my-3 items-center">
                    <h1 className="text-xl mb-2">All Settings</h1>
                </div>
                <div className="flex justify-end my-3 items-center">
                    <Link
                        className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded" to="/admin/setting">
                        Update Setting
                    </Link>
                    <a
                        className="mx-2 bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded" onClick={handleGenerate}>
                        Generate Schedule
                    </a>
                </div>
            </div>

            <div className="overflow-auto rounded-lg shadow hidden md:block">
                <table className="w-full">
                    <thead className="bg-gray-50 border-b-2 border-gray-200">
                    <tr>
                        <th className="p-3 text-sm font-semibold tracking-wide text-left">Setting Category</th>
                        <th className="p-3 text-sm font-semibold tracking-wide text-left">Value</th>
                    </tr>
                    </thead>
                    <tbody className="divide-y divide-gray-100">
                        <tr className="bg-white">
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                <a className="font-bold text-blue-500 hover:underline"> Start Date</a>
                            </td>
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                {dateFormatter(setting.startDate)}
                            </td>
                        </tr>
                        <tr className="bg-white">
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                <a className="font-bold text-blue-500 hover:underline"> End Date</a>
                            </td>
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                {dateFormatter(setting.endDate)}
                            </td>
                        </tr>
                        <tr className="bg-white">
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                <a className="font-bold text-blue-500 hover:underline">Concurrency Level</a>
                            </td>
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                {setting.concurrencyLevelDefault}
                            </td>
                        </tr>
                        <tr className="bg-white">
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                <a className="font-bold text-blue-500 hover:underline">Internal Distance</a>
                            </td>
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                {setting.internalDistance}
                            </td>
                        </tr>
                        <tr className="bg-white">
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                <a className="font-bold text-blue-500 hover:underline">External Distance</a>
                            </td>
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                {setting.externalDistance}
                            </td>
                        </tr>
                        <tr className="bg-white">
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                <a className="font-bold text-blue-500 hover:underline">Number of Time Slot</a>
                            </td>
                            <td className="p-3 text-sm text-gray-700 whitespace-nowrap">
                                {setting.noOfTimeSlot}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default ListAllSettingTable;