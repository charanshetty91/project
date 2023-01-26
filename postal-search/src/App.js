import { useState } from 'react';
import './App.css';
import axios from 'axios';
//import _ from 'lodash';

function App() {
  const [value, setValue] = useState("");
  const [searchInCompleteData, setSearchInCompleteData] = useState([]);
  const [postCodeFullDetails, setPostCodeFullDetails] = useState('');
  
  const onChange = (event) => {   
     axios.get('https://65d6inxrkb.execute-api.eu-central-1.amazonaws.com/prod?partialId=' + event.target.value)
      .then(res => {console.log("result data:"+res.data);  setSearchInCompleteData(res.data) });
    setValue(event.target.value);
  }
  const onSearch = (searchText) => {
    console.log(searchText);
    try{
    axios.get('/stg?id=' + searchText)
      .then(res => {setPostCodeFullDetails(res.data); });
    }
    catch(error)
    {
      console.error(error);
    }  
  }

  const onSearchItemClick = (item) => {
    setValue(item);
    setSearchInCompleteData([]);

  }
const searchResult = searchInCompleteData.length ? 
searchInCompleteData.map((item,index)=>{
  return(<div key={index} onClick={() => onSearchItemClick(item)} className="dropdown-row">{item}</div>)
}) 
: ( <div className="center"> search record ...</div>)


  return (

    <div className="App">

      <nav>
        <div className="nav-wrapper">
          <ul id="nav-mobile" className="hide-on-med-and-down">
            <li>Postal code Search:</li>
          </ul>
        </div>
      </nav>
      <div className='search-container'>
        <div className='search-inner'>
          <input type="text" value={value} onChange={onChange} />
          <button onClick={() => onSearch(value)}>Search </button>
        </div>
        
        <div className='dropdown'>
          {
          searchResult          
          // _.isNull(searchInCompleteData) && searchInCompleteData.map((item, index) => (<div key={index} onClick={() => onSearchItemClick(item)} className="dropdown-row">{item}</div>))
          }
        </div>
        {postCodeFullDetails.country !== "" && (<div className='tbl-postcode'>
          <div>Post Details</div>
          <div>Country:{postCodeFullDetails.Country}</div>
          <div>Region:{postCodeFullDetails.Region}</div>
          <div>AdminDistrict:{postCodeFullDetails.AdminDistrict}</div>
          <div>ParliamentaryConstituency:{postCodeFullDetails.ParliamentaryConstituency}</div>
          <div>Area:{postCodeFullDetails.Area}</div>
        </div>)
        }

      </div>
     

    </div>
  );
}

export default App;
