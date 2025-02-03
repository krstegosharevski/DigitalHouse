import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Color } from '../_models/color';

@Injectable({
  providedIn: 'root'
})
export class ColorsService {
  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getAllColors(){
    return this.http.get<Color[]>(this.baseUrl + "color");
  }

}
