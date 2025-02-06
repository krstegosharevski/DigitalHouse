export interface ProductEdit{
    id: number;
    name: string;
    price: number;
    description: string;
    isPresent: boolean;
    photoUrl: string;
    category: number;
    brand: number;
    colors: number[]
}