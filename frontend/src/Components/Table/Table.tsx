
import { testIncomeStatementData } from "./TestData"


const data = testIncomeStatementData

type Props = {
  configs: any,
  data: any
}

const Table = ({ configs, data }: Props) => {

  const renderrow = data.map((company: any) => {
    return(
      <tr key={company.cik}>
        {configs.map((val:any) => {
          return  <td className="p-4">{val.render(company)}</td>
        })}
      </tr>
    )});
  const renderheaders = configs.map((configs:any) => {
    return(
      <th className="p-3 text-left text-xs font-medium text-fray-500 uppercase tracking-wider" key={configs.label}>{configs.label}</th>
    )});

  return (
    <div className="bg-white shadow rounded-lg p-4 sm:p-6 xl:p-8 ">
    <table className="min-w-full divide-y divide-gray-200 m-5">
      <thead className="bg-gray-50">{renderheaders}</thead>
      <tbody>{renderrow}</tbody>
    </table>
  </div>
  )
}
export default Table