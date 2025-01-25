import React from "react";
import "./Card.css"

interface Props {
  companyName : string;
  ticker : string;
  price : number;
  }

const card: React.FC<Props>= ({companyName, ticker, price}: Props): JSX.Element=> {
  return (
    <div className="card">
        <img src="https://i.pinimg.com/736x/cf/cc/7b/cfcc7b6cc0fd117801987a793da9f284.jpg" alt="Image" />
        <div className="details">
            <h2>
              {companyName} ({ticker})
            </h2>
            <p>${price}</p>
        </div>
        <p className="infon">Lorem ipsum dolor sit amet consectetur adipisicing elit. Delectus porro distinctio quibusdam velit exercitationem, ea aperiam quod optio expedita rerum laboriosam soluta, dicta possimus repellendus? Accusamus facilis nisi id consequuntur!</p>
    </div>

  )
}
export default card