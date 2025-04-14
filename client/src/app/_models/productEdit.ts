import { ProductColorDto } from "./productColorDto";

export interface ProductEdit{
    id: number;
    name: string;
    price: number;
    description: string;
    isPresent: boolean;
    photoUrl: string;
    category: number;
    brand: number;
    productColors: ProductColorDto[];
}