import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProductDto } from '../_models/productDto';
import { Observable } from 'rxjs';
import { ProductEdit } from '../_models/productEdit';


@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getAllProducts(){
    return this.http.get<ProductDto[]>(this.baseUrl + "product/get-all");
  }

  getAllProductsByCategory(category: string){
    return this.http.get<ProductDto[]>(this.baseUrl + `product/category?category=${category}`);
  }

  getSearchedProducts(search: string){
    return this.http.get<ProductDto[]>(this.baseUrl + `product/search?search=${search}`)
  }

  getSearchedNameProduct(name: string){
    return this.http.get<ProductDto>(this.baseUrl+ `product/search-name?name=${name}`)
  }

  addNewProduct(formData: FormData): Observable<any>{
    return this.http.post(`${this.baseUrl}product/add-product`, formData)
  }

  updateProduct(id : number, formData: FormData): Observable<any>{
    return this.http.put(`${this.baseUrl}product/edit-product?productId=${id}`, formData)
  }

  getProductForEdit(id: number){
    return this.http.get<ProductEdit>(`${this.baseUrl}product/get-product-edit?id=${id}`);
  }
}
