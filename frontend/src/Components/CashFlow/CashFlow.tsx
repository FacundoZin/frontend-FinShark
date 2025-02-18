import { data, useOutletContext } from "react-router-dom";
import { CompanyCashFlow } from "../../companydates";
import { useEffect, useState } from "react";
import { GetCashFlow } from "../../api";
import Table from "../Table/Table";
import Spinner from "../Spinner/Spinner";
import { formatLargeMonetaryNumber } from "../../Helpers/NumberFormat";

type Props = {}

const config = [
  {
    label: "Date",
    render: (company: CompanyCashFlow) => company.date,
  },
  {
    label: "Operating Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.operatingCashFlow),
  },
  {
    label: "Investing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.netCashUsedForInvestingActivites),
  },
  {
    label: "Financing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(
        company.netCashUsedProvidedByFinancingActivities
      ),
  },
  {
    label: "Cash At End of Period",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.cashAtEndOfPeriod),
  },
  {
    label: "CapEX",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.capitalExpenditure),
  },
  {
    label: "Issuance Of Stock",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.commonStockIssued),
  },
  {
    label: "Free Cash Flow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.freeCashFlow),
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
      <Spinner />
    )}
    </>
  )
}
export default CashFlow