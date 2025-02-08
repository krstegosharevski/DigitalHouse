import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProductDto } from '../_models/productDto';
import { map, Observable } from 'rxjs';
import { ProductEdit } from '../_models/productEdit';
import { PaginatedResult } from '../_models/pagination';


@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<ProductDto[]> = new PaginatedResult<ProductDto[]>;

  constructor(private http : HttpClient) { }

  getAllProducts(){
    return this.http.get<ProductDto[]>(this.baseUrl + "product/get-all");
  }

  getAllProductsByCategory(category: string, page? : number, itemsPerPage?: number){
    let params = new HttpParams;

    if(page && itemsPerPage){
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage)
    }


    return this.http.get<ProductDto[]>(this.baseUrl + `product/category?category=${category}`, {observe: 'response', params}).pipe(
      map(response => {
        if(response.body){
          this.paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if(pagination){
          this.paginatedResult.pagination = JSON.parse(pagination);
        }
        return this.paginatedResult;
      })
    )
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
