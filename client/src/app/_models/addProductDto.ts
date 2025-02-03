export interface AddProductDto {
    name: string;
    price: number;
    description: string;
    isPresent: boolean;
    categoryId: number;
    brandId: number;
    colorIds: number[];
}