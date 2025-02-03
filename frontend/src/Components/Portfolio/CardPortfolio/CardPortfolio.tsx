import { SyntheticEvent } from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";

interface Props {
    portfoliovalue: string;
    onPortfolioDelete: (e: SyntheticEvent) => void;
};

const CardPortfolio = ({portfoliovalue, onPortfolioDelete}: Props) => {
  return (
    <>
    <h4>{portfoliovalue}</h4>

    <DeletePortfolio onPortfolioDelete={onPortfolioDelete} PortfolioValue={portfoliovalue}/>
    </>
  )
}
export default CardPortfolio