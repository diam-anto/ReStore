import { useEffect, useState } from "react";
import { Product } from "../models/product";

// const products = [
//   {name: 'product1', price: 100.00},
//   {name: 'product2', price: 200.00},
//   {name: 'product2', price: 200.00}
// ]


function App() {
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    fetch("http://localhost:5000/api/products")
    .then(response => response.json())
    .then(data => setProducts(data))
  }, []) 

  function addProduct() {
    setProducts(prevState => [...prevState, 
      {
        id: prevState.length + 101,
        name: 'product' + (prevState.length + 1), 
        price: (prevState.length * 100) + 100,
        brand: 'some brand',
        description: 'some description',
        pictureUrl: 'http://picsum.photos/200'
      }])
      // setProducts([...products, {name: 'product3', price: 300.00}])
  }

  return (
    <div className="app">
      <h1>Re-Store</h1>
      <ul>
        {products.map(product => (
          <li key={product.id}>{product.name} - {product.price}</li>
        ))}
      </ul>
      <button onClick={addProduct}>Add a product</button>
    </div>
  );
}

export default App;
