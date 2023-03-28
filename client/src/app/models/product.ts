export interface Product {
    id: number
    name: string
    description: string
    price: number
    pictureUrl: string
    // we put ? to make property optional
    type?: string
    brand: string
    quantityInStock?: number
}