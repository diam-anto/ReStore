import { Button } from "@mui/material";
import { Product } from "../../app/models/product";
import ProductList from "./ProductList";

interface Props {
    // products a type of Product array
    products: Product[];
    addProduct: () => void;
}
// Catalog(props: Props)
export default function Catalog({products, addProduct}: Props) {
    return (
        <>
        <ProductList products={products}/>
        <Button variant="contained" onClick={addProduct}>Add a product</Button>
      </>
    )
}