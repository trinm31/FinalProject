import React, {useState, useEffect} from 'react'
import Chart                        from "react-apexcharts";
import { getCharts }                from "../../../functions/charts";
import { toast }                    from "react-toastify";

const Middle = () => {
    
    const[options, setOptions] = useState([]);
    const [data, setData] = useState(["Students", "StudentInCourse", "Courses"]);
    
    useEffect(()=>{LoadData()}, []);
    const LoadData = () => {
        getCharts().then( async (res)=>{
            console.log(res.data);
            const result = await res.data;
            setOptions(result);
            toast.success(`Load Data Successfully`);
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        });
    }
    
    return (
        <div className=" bg-white ml-2 shadow-sm w-8/12 border rounded-xl border-gray-100">
            <div className="border-b p-3 border-gray-100">
                <p className="font-semibold ">Data</p>
            </div>
            <div id="chart">
                <Chart
                    options={{
                        chart: {
                            id: 'apexchart-example'
                        },
                        xaxis: {
                            categories: data
                        }
                    }}
                    series={[{
                        name: 'series-1',
                        data: options
                    }]}
                    type="bar"
                    height="400"
                    width="750"
                />
            </div>
        </div>
    )
}

export default Middle
