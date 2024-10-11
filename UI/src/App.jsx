import { useState } from 'react'
import './App.css'
import Comments from './components/Comments.jsx'

function App() {
  const [show, setShow] = useState(false)

  return (
    <>
      <div className='App'>
        <button onClick={() => setShow(!show)}>Get All Comments</button>
        {show && <Comments />}
      </div>
    </>
  )
}

export default App
