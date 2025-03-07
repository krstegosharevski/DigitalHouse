import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { InternetPackage } from '../_models/internetPackage';

@Injectable({
  providedIn: 'root'
})
export class InternetPackagesService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllInternetPackages(){
    return this.http.get<InternetPackage[]>(this.baseUrl + "internetpackage");
  }
}
