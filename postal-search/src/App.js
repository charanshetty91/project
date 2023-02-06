import { useState } from 'react';
import './App.css';
import axios from 'axios';
import _ from 'lodash';
import Header from './component/header';
import ResultTable from './component/result-table'

function App() {
  const [value, setValue] = useState("");
  const [searchInCompleteData, setSearchInCompleteData] = useState([]);
  const [postCodeFullDetails, setPostCodeFullDetails] = useState('');
  const [resultLabel,setResultLabel]=useState('');
  const [details, setDetails] = useState('Details');

  const onChange = (event) => {
    axios.get('https://65d6inxrkb.execute-api.eu-central-1.amazonaws.com/prod?partialId=' + event.target.value)
      .then(res => { console.log("result data:" + res.data); setSearchInCompleteData(res.data) }).catch((er)=>{
        setSearchInCompleteData([]);
      });
    setResultLabel("loading...")
    setValue(event.target.value);
  }

  const onSearch = (searchText) => {
    setSearchInCompleteData([]);
    setResultLabel("")    
    try {
      axios.get('https://vg8zvzdlo5.execute-api.eu-central-1.amazonaws.com/prod?id=' + searchText)
        .then(res => { setDetails('Details'); setPostCodeFullDetails(res.data); }).catch((err)=>{
          setDetails('No data found')
          setPostCodeFullDetails('');
          
        });
    }
    catch (error) {
      setPostCodeFullDetails('');
      console.error(error);
    }
  }

  const onSearchItemClick = (item) => {
    setResultLabel("")
    setValue(item);
    setSearchInCompleteData([]);

  }
  const searchResult = searchInCompleteData.length ?
    searchInCompleteData.map((item, index) => {
      return (<div className="collection-item"  key={index} onClick={() => onSearchItemClick(item)} >{item}</div>)
    })
    : <label>{resultLabel}</label>

  return (

    <div className="App">
      <Header/>
      <div className='search-container'>
        <div className="row">
          <div className="input-field col s7">
            <input value={value} onChange={onChange} id="postal_code" type="text" className="validate" />
            <label className="active">Enter postal code here...</label>
            <div className='col s3 collection'>
            {
              searchResult
            }
          </div>

          <div className='left col s9 table-resposive'>
            {!_.isEmpty(postCodeFullDetails.Country) && (<div className='left tbl-postcode'>
              <div className='card-panel teal '>{details}</div>
              <ResultTable postCodeFullDetails ={postCodeFullDetails}/>
                    
            </div>)
            }
          </div>
          </div>
          <div className='input-field col s5'>
            <button className='left search-btn waves-effect waves-light btn' onClick={() => onSearch(value)}>Search details </button>
          </div>

        </div>
       
      </div>
    </div>
  );
}

export default App;
