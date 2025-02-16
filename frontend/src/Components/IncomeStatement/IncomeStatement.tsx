import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom"
import { CompanyIncomeStatement } from "../../companydates";
import { GetIncomeStatement } from "../../api";
import Table from "../Table/Table";
import Spinner from "../Spinner/Spinner";

type Props = {}

const configs = [
  {
    label: "Date",
    render: (company: CompanyIncomeStatement) => company.date,
  },
  {
    label: "Revenue",
    render: (company: CompanyIncomeStatement) => company.revenue,
  },
  {
    label: "Cost Of Revenue",
    render: (company: CompanyIncomeStatement) => company.costOfRevenue,
  },
  {
    label: "Depreciation",
    render: (company: CompanyIncomeStatement) =>
      company.depreciationAndAmortization,
  },
  {
    label: "Operating Income",
    render: (company: CompanyIncomeStatement) => company.operatingIncome,
  },
  {
    label: "Income Before Taxes",
    render: (company: CompanyIncomeStatement) => company.incomeBeforeTax,
  },
  {
    label: "Net Income",
    render: (company: CompanyIncomeStatement) => company.netIncome,
  },
  {
    label: "Net Income Ratio",
    render: (company: CompanyIncomeStatement) => company.netIncomeRatio,
  },
  {
    label: "Earnings Per Share",
    render: (company: CompanyIncomeStatement) => company.eps,
  },
  {
    label: "Earnings Per Diluted",
    render: (company: CompanyIncomeStatement) => company.epsdiluted,
  },
  {
    label: "Gross Profit Ratio",
    render: (company: CompanyIncomeStatement) => company.grossProfitRatio,
  },
  {
    label: "Opearting Income Ratio",
    render: (company: CompanyIncomeStatement) => company.operatingIncomeRatio,
  },
  {
    label: "Income Before Taxes Ratio",
    render: (company: CompanyIncomeStatement) => company.incomeBeforeTaxRatio,
  },
];

const IncomeStatement = (props: Props) => {

  const ticker = useOutletContext<string>();
  const [incomeStatement,setincomeStatement] = useState<CompanyIncomeStatement[]>();

  useEffect(()=> {
    const result = async() => {
      const value = await GetIncomeStatement(ticker);
      setincomeStatement(value!.data)
    }

    result();
  },[])

  return (
    <>
    {incomeStatement ? (
      <>
        <Table configs={configs} data={incomeStatement}/>
      </>
    ) : (
      <Spinner/>
    )}
  </>
  )
}
export default IncomeStatement