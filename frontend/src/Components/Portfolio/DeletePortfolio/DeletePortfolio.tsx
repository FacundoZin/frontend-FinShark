import { SyntheticEvent } from "react"

interface Props {
    PortfolioValue: string;
    onPortfolioDelete: (e: SyntheticEvent) => void;

}

const DeletePortfolio = ({onPortfolioDelete, PortfolioValue}: Props) => {
  return (
    <div>
    <form onSubmit={onPortfolioDelete}>
        <input  hidden={true} value={PortfolioValue} />
        <button className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
          Remove from portfolio
        </button>
    </form>
    </div>
  )
}
export default DeletePortfolio;