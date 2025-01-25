import Card from "../Card/Card"


interface Props {}

const CardList: React.FC<Props> = (props: Props): JSX.Element => {
  return (
    <div>
        <Card companyName="Mercado Libre AR" ticker="MP" price={125}/>
        <Card companyName="Yacimientos Petroliferos Fiscales" ticker="YPF" price={80}/>
        <Card companyName="Globant" ticker="GLB" price={105}/>

    </div>
  )
}
export default CardList