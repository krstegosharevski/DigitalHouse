import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProductDto } from '../_models/productDto';
import { map, Observable } from 'rxjs';
import { ProductEdit } from '../_models/productEdit';
import { PaginatedResult } from '../_models/pagination';
import { ProductParams } from '../_models/productParams';


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

  getAllProductsByCategory(category: string, productParams : ProductParams){
    let params = this.getPaginationHeaders(productParams.pageNumber, productParams.pageSize)
    params = params.append('MinPrice', productParams.MinPrice);
    params = params.append('MaxPrice', productParams.MaxPrice);
    if(productParams.BrandIds.length > 0){
      productParams.BrandIds.forEach(id => {
        params = params.append('brandIds', id);
      });
    }
    return this.getPaginatedResult<ProductDto[]>(this.baseUrl + `product/category?category=${category}` ,  params);
  }


  private getPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams;
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);
    return params;
  }


  private getPaginatedResult<T>(url: string, params: HttpParams){
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>;
    
    return this.http.get<T>(url, {observe: 'response', params}).pipe(
      map(response => {
        if(response.body){
          paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if(pagination){
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;
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
