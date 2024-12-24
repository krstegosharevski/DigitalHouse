import { Photo } from "./photo";

export interface Product{
    id: number;
    name: string;
    price: number;
    description: string;
    photo: Photo
}