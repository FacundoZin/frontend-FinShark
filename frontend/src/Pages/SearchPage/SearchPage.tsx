import { ChangeEvent, SyntheticEvent, useState } from "react";
import { CompanySearch } from "../../companydates";
import { searchCompanies } from "../../api";
import Search from "../../Components/Search/Search";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";
import CardList from "../../Components/CardList/CardList";

interface Props {

}
const SearchPage = (props: Props) => {

  const [Searchvalue, setSearchvalue] = useState<string>("");
  const [PortfolioValues, setPortfolioValues] = useState<string[]>([]);
  const [SearchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setserverError] = useState<string | null>(null);
  
  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    const updatedPortfolio = [...PortfolioValues, e.target[0].value];
    setPortfolioValues(updatedPortfolio);
  };

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    const updatedPortfolio = PortfolioValues.filter ((matriz) => {
      return matriz !== e.target[0].value
    })
    setPortfolioValues(updatedPortfolio);
  }

    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) =>{
        setSearchvalue(e.target.value);
        console.log(e);
    };
    
    const onSearchSubmit = async (e: SyntheticEvent) => {

        e.preventDefault();

        const result = await searchCompanies(Searchvalue);

        if (result !== null && result !== undefined) {

          // Ahora que sabemos que result no es null ni undefined, podemos acceder a result.data
          if (typeof result === "string") {

              setserverError(result);

          } else if (Array.isArray(result.data)) {
              
              setSearchResult(result.data);
              console.log(SearchResult);

          }

      } else {
        
          console.log("El resultado es null o undefined");
      }
      
    };
  return (
    <>
    <Search onSearchSubmit={onSearchSubmit} search={Searchvalue} handleSearchChange={handleSearchChange} />
    <ListPortfolio PortfolioValues={PortfolioValues} onPortfolioDelete={onPortfolioDelete}/>
    <CardList searchResult={SearchResult} onPortfolioCreate={onPortfolioCreate}/>
    {serverError && <h1>{serverError}</h1>}
  </>
  )
}
export default SearchPage