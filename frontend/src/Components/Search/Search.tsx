import { ChangeEvent, useState, SyntheticEvent } from "react"

type Props = {}

const Search: React.FC<Props> = (props: Props): JSX.Element => {

    const [Search, setSearch] = useState<string>("");

    const handlechange = (e: ChangeEvent<HTMLInputElement>) =>{
        setSearch(e.target.value);
        console.log(e);
    }
    
    const onClick = (e: SyntheticEvent) => {
        console.log(e)
    }

  return (
    <div>
        <input value={Search} onChange={(e) => handlechange(e)}></input>
        <button onClick={(e) => onClick(e)} /> 
    </div>
  )
}
export default Search

