import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BrandDto } from '../_models/brandDto';
@Injectable({
  providedIn: 'root'
})
export class BrandsService {
  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getAllBrands(){
      return this.http.get<BrandDto[]>(this.baseUrl + "brand");
    }

  getBrandsByCategory(categoryName: string){
    let params = new HttpParams().set('categoryName', categoryName);
  return this.http.get<BrandDto[]>(`${this.baseUrl}brand/by-category`, { params });
  }

}
