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
        <button>X</button>
    </form>
    </div>
  )
}
export default DeletePortfolio;