import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Magenta1 } from '../_models/magenta1';

@Injectable({
  providedIn: 'root'
})
export class Magenta1Service {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }
  
  getAllMagenta1(){
    return this.http.get<Magenta1[]>(this.baseUrl + "magenta1");
  }

}
