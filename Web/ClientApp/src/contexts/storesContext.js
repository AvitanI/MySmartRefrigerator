import React from 'react'

const StoresContext = React.createContext()

export const StoresProvider = StoresContext.Provider
export const StoresConsumer = StoresContext.Consumer

export default StoresContext