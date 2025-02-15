import { data, useOutletContext } from "react-router-dom"
import { CompanyBalanceSheet } from "../../companydates"
import { useEffect, useState } from "react"
import { Getbalancesheet } from "../../api"
import RatioList from "../RatioList/RatioList"

type Props = {}

const configs = [
    {
        label: <div className="font-bold">Total Assets</div>,
        render: (company: CompanyBalanceSheet) => company.totalAssets,
      },
      {
        label: "Current Assets",
        render: (company: CompanyBalanceSheet) => company.totalCurrentAssets,
      },
      {
        label: "Total Cash",
        render: (company: CompanyBalanceSheet) => company.cashAndCashEquivalents,
      },
      {
        label: "Property & equipment",
        render: (company: CompanyBalanceSheet) => company.propertyPlantEquipmentNet,
      },
      {
        label: "Intangible Assets",
        render: (company: CompanyBalanceSheet) => company.intangibleAssets,
      },
      {
        label: "Long Term Debt",
        render: (company: CompanyBalanceSheet) => company.longTermDebt,
      },
      {
        label: "Total Debt",
        render: (company: CompanyBalanceSheet) => company.otherCurrentLiabilities,
      },
      {
        label: <div className="font-bold">Total Liabilites</div>,
        render: (company: CompanyBalanceSheet) => company.totalLiabilities,
      },
      {
        label: "Current Liabilities",
        render: (company: CompanyBalanceSheet) => company.totalCurrentLiabilities,
      },
      {
        label: "Long-Term Debt",
        render: (company: CompanyBalanceSheet) => company.longTermDebt,
      },
      {
        label: "Long-Term Income Taxes",
        render: (company: CompanyBalanceSheet) => company.otherLiabilities,
      },
      {
        label: "Stakeholder's Equity",
        render: (company: CompanyBalanceSheet) => company.totalStockholdersEquity,
      },
      {
        label: "Retained Earnings",
        render: (company: CompanyBalanceSheet) => company.retainedEarnings
    },
]

const BalanceSheet = (props: Props) => {
    
    const ticker = useOutletContext<string>();
    const [balancesheet, setbalancesheet] = useState<CompanyBalanceSheet>();

    useEffect(()=> {

        const result = async() => {
            const value = await Getbalancesheet(ticker);
            setbalancesheet(value!.data[0])
        }

        result();
    },[])

  return (
    <>
    {balancesheet ? (
        <>        
        <RatioList config={configs} data={balancesheet}/>
        </>
    ):(
        <>loading...</>
    )}
    </>
  )
}
export default BalanceSheet