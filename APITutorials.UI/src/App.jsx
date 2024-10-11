import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Content from './Content'

function App() {
  const [show, setShow] = useState(false)

  return (
    <>
      <div className='App'>
        <button onClick={() => setShow(!show)}>Toggle</button>
        {show && <Content />}
      </div>
    </>
  )
}

export default App
