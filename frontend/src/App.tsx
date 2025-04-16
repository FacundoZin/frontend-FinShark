
import './index.css';
import NavBar from './Components/NavBar/NavBar';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/react/ReactToastify.css"


function App() {

  return (
    <>
    <NavBar />
    <Outlet />
    <ToastContainer />
    
    </>
  );

}
export default App
