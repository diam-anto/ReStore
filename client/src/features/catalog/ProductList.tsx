import { List } from "@mui/material";
import { Product } from "../../app/models/product";
import ProductCard from "../ProductCard";

interface Props {
    products: Product[];
}


export default function ProductList({products}: Props) {
    return (
        <List>
            {/* we do not need porduct: any in this (props.products.map((product: any)) beacause we know that product is an Array */}
            {products.map((product) => (
                <ProductCard key={product.id} product={product} />
            ))}
        </List>
    )
}