import { ProductColorDto } from "./productColorDto";

export interface AddProductDto {
    name: string;
    price: number;
    description: string;
    isPresent: boolean;
    categoryId: number;
    brandId: number;
    productColors: ProductColorDto[];
}