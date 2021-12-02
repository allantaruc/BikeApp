import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';



function App() {

  const [bikes, setBikes] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5000/api/bikes').then(response => {
      console.log(response);
      setBikes(response.data);
    })
  }, [])

  return (
    <div>
      <Header as='h2' icon='users' content='Bikes'/>
        <List>
        {bikes.map((bike: any) => ( 
          <List.Item key={bike.id}>
            {bike.customerName}
          </List.Item>
          ))}
        </List>
    </div>
  );
}

export default App;
