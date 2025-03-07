import { Component, OnInit } from '@angular/core';
import { InternetPackage } from 'src/app/_models/internetPackage';
import { Tariff } from 'src/app/_models/tariff';
import { InternetPackagesService } from 'src/app/_services/internet-packages.service';
import { TariffsService } from 'src/app/_services/tariffs.service';

@Component({
  selector: 'app-magenta1',
  templateUrl: './magenta1.component.html',
  styleUrls: ['./magenta1.component.css']
})
export class Magenta1Component implements OnInit {
  magenta1: Tariff[] = [];
  internet: InternetPackage[] = [];

  constructor(private tariffsService : TariffsService, 
              private internetPackageService : InternetPackagesService) { }

  ngOnInit(): void {
    this.getAllMagenta1Tariffs()
    this.getAllInternetPackages();
  }

  getAllMagenta1Tariffs(){
    this.tariffsService.getAllMagenta1Tariffs().subscribe({
      next: (response) => {
        this.magenta1 = response
      },
      error : (err) => console.log(err)
    })
  }

  getAllInternetPackages(){
    this.internetPackageService.getAllInternetPackages().subscribe({
      next: (response) => {
        this.internet = response
      },
      error : (err) => console.log(err)
    })
  }

}
