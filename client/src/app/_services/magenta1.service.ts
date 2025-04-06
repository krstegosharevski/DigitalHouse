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

  // Implemetnt approve magenta1 user.
  approveMagenta1(id: number) {
    return this.http.put(this.baseUrl + `magenta1/approve/${id}`, null);
  }
  

  // Implement create magenta1 for user.
  createMagenta1(magentaData: {
    userId: number;
    budget: number;
    internetPackageId: number;
    magenta1TariffsId: number[];
  }) {
    return this.http.post(this.baseUrl + 'magenta1/create', magentaData);
  }
  

}
