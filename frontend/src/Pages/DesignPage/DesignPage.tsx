import { CompanyKeyMetrics } from "../../companydates"
import RatioList from "../../Components/RatioList/RatioList"
import Table from "../../Components/Table/Table"
import { testIncomeStatementData } from "../../Components/Table/TestData"

type Props = {}

const tableConfig = [
  {
    label: "Market Cap",
    render: (company: CompanyKeyMetrics) => company.marketCapTTM,
  },
]

const DesignPage = (props: Props) => {
  return (
    <>
    <h1>
      Table - Table takes in a configuration object and company data as
      params. Use the config to style your table.
    </h1>
    <RatioList data={testIncomeStatementData} config={tableConfig} />
    <Table />
    <h3>
      Table - Table takes in a configuration object and company data as
      params. Use the config to style your table.
    </h3>
    </>
  )
}
export default DesignPage