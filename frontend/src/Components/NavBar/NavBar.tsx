import { Link } from 'react-router-dom'
import logo from './logo.png'

interface Props {}

const NavBar = (props: Props) => {
  return (
  <nav className="relative container mx-auto p-6">
    <div className="flex items-center justify-between">
      <div className="flex items-center space-x-20">

        <Link to={"/"}>
          <img src={logo} alt="" />
        </Link>

        <div className="hidden font-bold lg:flex">
          <Link to={"/search"} className="text-black hover:text-darkBlue">
            Search 
          </Link>
        </div>
      </div>
      
      <div className="hidden lg:flex items-center space-x-6 text-back">
        <div className="hover:text-darkBlue">Login</div>
        <a href="" className="px-8 py-3 font-bold rounded text-white bg-green-400 hover:opacity-70">
          Signup
        </a>
      </div>
    </div>
  </nav>
  )
}
export default NavBar