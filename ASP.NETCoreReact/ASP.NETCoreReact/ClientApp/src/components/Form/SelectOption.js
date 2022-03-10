import { forwardRef } from "react";

const SelectOption = forwardRef(({ listData, defaultValue, ...rest }, ref) => {
    return (
        <select {...rest} ref={ref} className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500">
            {listData.length &&
                listData.map((item, index) => (
                    <option value={item.name} key={index}>
                        {item.name}
                    </option>
                ))}
        </select>
    );
})

export default SelectOption;
