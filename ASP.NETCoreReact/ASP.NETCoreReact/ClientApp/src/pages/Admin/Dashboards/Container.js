import React from 'react'

import Card from './Card'
import Middle from './Middle'
import RightBar from './RightBar'
import User from './User'

const Container = () => {
    return (
        <div className=" bg-gradient-to-r from-gray-100 to-gray-50 h-full " >
            <div className="  px-8 py-1 ">
                <p className="text-gray-500 text-lg">
                    Welcome To
                </p>
                <p className="font-bold text-2xl transform -translate-y-2">
                    Exam Management System
                </p>
            </div>
            <div className="flex p-4 space-x-3">
                <Card />
            </div>
            <div className="flex  ml-3 mt-6 space-x-6  mr-4">
                <Middle />
                <RightBar />
                
            </div>
            <div className='ml-3 mt-6 space-x-6  mr-4'>
                <User />
            </div>
        </div>
    )
}

export default Container
