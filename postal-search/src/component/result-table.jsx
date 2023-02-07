function ResultTable({postCodeFullDetails}) {
    return(
        <table className='responsive-table'>
                <thead>
                  <tr>
                    <th>Country</th>
                    <th>Region</th>
                    <th>AdminDistrict</th>
                    <th>Parliamentary Constituency</th>
                    <th>Area</th>
                  </tr>
                </thead>

                <tbody>
                  <tr>
                    <td>{postCodeFullDetails.country}</td>
                    <td>{postCodeFullDetails.region}</td>
                    <td>{postCodeFullDetails.adminDistrict}</td>
                    <td>{postCodeFullDetails.parliamentaryConstituency}</td>
                    <td>{postCodeFullDetails.area}</td>
                  </tr>
                </tbody>
              </table> 
    );
} 
export default ResultTable;