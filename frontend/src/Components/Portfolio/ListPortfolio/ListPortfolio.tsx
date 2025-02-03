import { SyntheticEvent } from "react";
import CardPortfolio from "../CardPortfolio/CardPortfolio";

interface Props {
  PortfolioValues: string[];
  onPortfolioDelete: (e: SyntheticEvent) => void;
};

const ListPortfolio = ({PortfolioValues, onPortfolioDelete}: Props) => {
  return (
    <>
    <h3>My Portfolio</h3>
    <ul>
      {PortfolioValues &&
      PortfolioValues.map((portfoliovalue) => {
        return <CardPortfolio portfoliovalue={portfoliovalue} onPortfolioDelete={onPortfolioDelete}/>
      })
      }
    </ul>
    </>
  )
}
export default ListPortfolio;