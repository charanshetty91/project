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
                    <td>{postCodeFullDetails.Country}</td>
                    <td>{postCodeFullDetails.Region}</td>
                    <td>{postCodeFullDetails.AdminDistrict}</td>
                    <td>{postCodeFullDetails.ParliamentaryConstituency}</td>
                    <td>{postCodeFullDetails.Area}</td>
                  </tr>
                </tbody>
              </table> 
    );
} 
export default ResultTable;