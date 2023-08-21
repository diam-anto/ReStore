import ProductList from "./ProductList";
import { useEffect } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { fetchProductsAsync, productSelectors } from "./catalogSlice";

// Catalog(props: Props)
export default function Catalog() {
    //const [products, setProducts] = useState<Product[]>([]);
    const products = useAppSelector(productSelectors.selectAll);
    const dispatch = useAppDispatch();
    const {productsLoaded, status} = useAppSelector(state => state.catalog);
    //const [loading, setLoading] = useState(true);

  useEffect(() => {
    // agent.Catalog.list()
    //   .then(products => setProducts(products))
    //   .catch(error => console.log(error))
    //   .finally(() => setLoading(false))
    if (!productsLoaded) dispatch(fetchProductsAsync());
  }, [productsLoaded, dispatch]) 


    if (status.includes('pending')) return <LoadingComponent message="Loading products"/>


    return (
        <>
        <ProductList products={products}/>
      </>
    )
}