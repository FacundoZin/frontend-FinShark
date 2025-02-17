import { useEffect, useState } from "react"
import { CompanyTenK } from "../../companydates"
import { GetTenK } from "../../api"
import TenKitem from "../TenKitem/TenKitem"
import Spinner from "../Spinner/Spinner"

type Props = {
    ticker: string
}



const TenKFinder = ({ ticker }: Props) => {

  const [TenK, SetTenK] = useState<CompanyTenK[]>();
  
  useEffect (() =>{

    const result = async() =>{

        const value = await GetTenK(ticker);
        SetTenK(value?.data)
    }

    result();

  },[ticker])
    
  return (
    <div className="inline-flex rounded-md shadow-sm m-4" role="group">
        {TenK ? (
            TenK?.slice(0,5).map((TenK) => {
                return <TenKitem TenK={TenK} />
            })
        ):(
            <Spinner />
        )}
    </div>
  )
}
export default TenKFinder