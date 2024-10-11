import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Comment from './Components/Comment'

function App() {
  const [show, setShow] = useState(false)

  return (
    <>
      <div className='App'>
        <button onClick={() => setShow(!show)}>Toggle</button>
        {show && <Comment />}
      </div>
    </>
  )
}

export default App
