import { Component, OnInit } from '@angular/core';
import { Tariff } from 'src/app/_models/tariff';
import { TariffsService } from 'src/app/_services/tariffs.service';

@Component({
  selector: 'app-prepaid',
  templateUrl: './prepaid.component.html',
  styleUrls: ['./prepaid.component.css']
})
export class PrepaidComponent implements OnInit {

  tariffs : Tariff[] = [] 

  constructor(private tariffsService: TariffsService) { }

  ngOnInit(): void {
    this.getAllTariffs();
  }

  getAllTariffs(){
    this.tariffsService.getAllPrepaidTariffs().subscribe({
      next : (result) => {
        this.tariffs = result
      },
      error: (err) => console.error(err)
    })
  }
}
