import "./App.css"
import { ToastContainer } from 'react-toastify'
import "react-toastify/dist/ReactToastify.css"
import { UserProvider } from "./Context/useAuth"

function App() {

  return (
    <>
        <UserProvider>
          <ToastContainer />
        </UserProvider>
    </>
  )
}

export default App
