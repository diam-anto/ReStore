import { Grid } from "@mui/material";
import { Product } from "../../app/models/product";
import ProductCard from "./ProductCard";

interface Props {
    products: Product[];
}


export default function ProductList({products}: Props) {
    return (
        <Grid container spacing={4}>
            {/* we do not need porduct: any in this (props.products.map((product: any)) beacause we know that product is an Array */}
            {products.map((product) => (
                <Grid item xs={3} key={product.id}>
                    <ProductCard product={product} />
                </Grid>
                
            ))}
        </Grid>
    )
}