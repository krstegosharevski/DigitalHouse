import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CategoryDto } from '../_models/categoryDto';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getAllBrands(){
      return this.http.get<CategoryDto[]>(this.baseUrl + "category");
    }
}
