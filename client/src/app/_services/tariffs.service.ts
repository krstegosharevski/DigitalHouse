import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Tariff } from '../_models/tariff';

@Injectable({
  providedIn: 'root'
})
export class TariffsService {

  baseUrl =  environment.apiUrl;

  constructor(private http : HttpClient) { }

  getAllPrepaidTariffs(){
    return this.http.get<Tariff[]>(this.baseUrl + "tariff/prepaid");
  }

  getAllTruste12Tariffs(){
    return this.http.get<Tariff[]>(this.baseUrl + "tariff/trust12");
  }

  getAllNoContractTariffs(){
    return this.http.get<Tariff[]>(this.baseUrl + "tariff/no-contract");
  }

  getAllMagenta1Tariffs(){
    return this.http.get<Tariff[]>(this.baseUrl + "tariff/magenta1");
  }
}
