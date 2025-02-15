import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { CompanyProfile } from "../../companydates";
import { Get_company_info } from "../../api";
import SideBar from "../../Components/SideBar/SideBar";
import CompanyDashboard from "../../Components/CompanyDashboard/CompanyDashboard";
import Mosaics from "../../Components/Mosaics/Mosaics";

interface Props {
    
}

const CompanyPage = (props: Props) => {

    let { ticker } = useParams();
    const [company, setCompany] = useState<CompanyProfile>();
  
    useEffect(() => {

      const getProfileInit = async () => {
        const result = await Get_company_info(ticker!);
        setCompany(result?.data[0]);
      };

      getProfileInit();
    },[])
 
  return (
    <> 
    {company ?  (
      <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">

        <SideBar />

        <CompanyDashboard ticker={ticker!}>

            <Mosaics title="Company Name" content={company.companyName} />
            <Mosaics title="Price" content={"$" + company.price.toString()} />
            <Mosaics title="DCF" content={"$" + company.dcf.toString()} />
            <Mosaics title="Sector" content={company.sector} />

            <p className="bg-white shadow rounded text-medium font-medium text-gray-900 p-3 mt-1 m-4">
              {company.description}
            </p>
            
        </CompanyDashboard>
      </div>
      
        ): (
      <div>company not found!</div>
    )}
    </>
  )
}
export default CompanyPage           