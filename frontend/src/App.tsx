
import './index.css';
import NavBar from './Components/NavBar/NavBar';
import { Outlet } from 'react-router-dom';


function App() {

  return (
    <>
    <NavBar />
    <Outlet />
    </>
  );

}
export default App
