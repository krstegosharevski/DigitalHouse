import { Component, OnInit } from '@angular/core';
import { Tariff } from 'src/app/_models/tariff';
import { TariffsService } from 'src/app/_services/tariffs.service';

@Component({
  selector: 'app-mobile-tariff',
  templateUrl: './mobile-tariff.component.html',
  styleUrls: ['./mobile-tariff.component.css']
})
export class MobileTariffComponent implements OnInit {
  
  trust12 : Tariff[] = []
  noContract : Tariff[] = []

  constructor(private tariffsService: TariffsService) { }

  ngOnInit(): void {
    this.getAllTrust12Tariffs();
    this.getAllNoContractTariffs();
  }

  getAllTrust12Tariffs(){
    this.tariffsService.getAllTruste12Tariffs().subscribe({
      next: (result) => {
        this.trust12 = result
      },
      error: (err) => console.log(err)
    })
  }

  getAllNoContractTariffs(){
    this.tariffsService.getAllNoContractTariffs().subscribe({
      next: (result) => {
        this.noContract = result
      },
      error: (err) => console.log(err)
    })
  }

}
