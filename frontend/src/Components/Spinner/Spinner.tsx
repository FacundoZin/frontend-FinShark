import { ClipLoader } from "react-spinners"
import "./Spinner.css"

type Props = {
    loading?: boolean
}

const Spinner = ({ loading = true}: Props) => {
  return (
    <>
    <div id="loading-spinner">
        <ClipLoader 
        color="#36d7b7"
        loading={loading}
        size={35}
        aria-label="Loading spinner"
        data-testid="loader"
        />
    </div>
    </>
  )
}
export default Spinner