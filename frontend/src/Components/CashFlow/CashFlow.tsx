import { data, useOutletContext } from "react-router-dom";
import { CompanyCashFlow } from "../../companydates";
import { useEffect, useState } from "react";
import { GetCashFlow } from "../../api";
import Table from "../Table/Table";
import RatioList from "../RatioList/RatioList";

type Props = {}

const config = [
    {
        label: "Date",
        render: (company: CompanyCashFlow) => company.date,
      },
      {
        label: "Operating Cashflow",
        render: (company: CompanyCashFlow) =>company.operatingCashFlow,
      },
      {
        label: "Investing Cashflow",
        render: (company: CompanyCashFlow) => company.netCashUsedForInvestingActivites,
      },
      {
        label: "Financing Cashflow",
        render: (company: CompanyCashFlow) =>company.netCashUsedProvidedByFinancingActivities,
      },
      {
        label: "Cash At End of Period",
        render: (company: CompanyCashFlow) => company.cashAtEndOfPeriod,
      },
      {
        label: "CapEX",
        render: (company: CompanyCashFlow) => company.capitalExpenditure,
      },
      {
        label: "Issuance Of Stock",
        render: (company: CompanyCashFlow) => company.commonStockIssued,
      },
      {
        label: "Free Cash Flow",
        render: (company: CompanyCashFlow) => company.freeCashFlow,
      },
];

const CashFlow = (props: Props) => {

    const ticker = useOutletContext<string>();
    const [CashFlowStatement,setCashFlowStatement]=useState<CompanyCashFlow[]>();

    useEffect(() => {
        const result = async() =>{
            const value = await GetCashFlow(ticker);
            setCashFlowStatement(value!.data)
        }

        result();
    },[]);

  return (
    <>
    {CashFlowStatement ? (
      <>
            <Table configs={config} data={CashFlowStatement} />
      </>
    ) : (
      <>
      loading...
      </>
    )}
    </>
  )
}
export default CashFlow