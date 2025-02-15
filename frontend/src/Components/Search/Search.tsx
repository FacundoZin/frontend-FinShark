import { SyntheticEvent } from "react"
import { FaSearch } from "react-icons/fa";

interface props{
  onSearchSubmit: (e:SyntheticEvent) => void;
  search: string | undefined;
  handleSearchChange:  (e: React.ChangeEvent<HTMLInputElement>) => void;
}


const Search: React.FC<props> = ({onSearchSubmit, search, handleSearchChange}: props): JSX.Element => {
  return (
    <section className="relative bg-black">
    <div className="max-w-4xl mx-auto p-6 space-y-6">
      <form
        className="form relative flex flex-col w-full p-10 space-y-4  rounded-lg md:flex-row md:space-y-0 md:space-x-3"
        onSubmit={onSearchSubmit}
      >
        <input
          className="flex-1 p-3 border-2 rounded-lg placeholder-black focus:outline-none"
          id="search-input"
          placeholder="Search companies"
          value={search}
          onChange={handleSearchChange}
        ></input>
          <FaSearch className="absolute right-14 top-1/2 transform -translate-y-1/2 text-gray-500" />
      </form>
    </div>
  </section>
  )
}
export default Search;

