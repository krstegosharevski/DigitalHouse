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
  budget: number = 0;
  selectedInternet: InternetPackage | null = null;
  selectedMobileLines: Tariff[] = [];

  constructor(
    private tariffsService: TariffsService,
    private internetPackageService: InternetPackagesService
  ) {}

  ngOnInit(): void {
    this.getAllMagenta1Tariffs();
    this.getAllInternetPackages();
  }

  getAllMagenta1Tariffs() {
    this.tariffsService.getAllMagenta1Tariffs().subscribe({
      next: (response) => {
        this.magenta1 = response;
      },
      error: (err) => console.log(err)
    });
  }

  getAllInternetPackages() {
    this.internetPackageService.getAllInternetPackages().subscribe({
      next: (response) => {
        this.internet = response;
      },
      error: (err) => console.log(err)
    });
  }

  selectMobileLine(tariff: Tariff) {
    if (this.selectedMobileLines.length >= 5) {
      return;
    }

    this.selectedMobileLines.push({...tariff});
    this.updateBudget();
  }

  unselectMobileLine(index: number) {
    this.selectedMobileLines.splice(index, 1);
    this.updateBudget();
  }

  selectInternetPackage(internetPkg: InternetPackage) {
    this.selectedInternet = internetPkg;
    this.updateBudget();
  }

  private getBudgetForTariff(name: string): number {
    switch (name) {
      case 'S':
        return 2000;
      case 'M':
        return 3000;
      case 'Ultimate':
        return 3500;
      case 'Ultra':
        return 5000;
      default:
        return 0;
    }
  }

  updateBudget() {
    this.budget = 0;

    this.selectedMobileLines.forEach(line => {
      this.budget += this.getBudgetForTariff(line.name);
    });

    if (this.selectedMobileLines.length > 3) {
      this.budget += 1000;
    }

    if (this.selectedInternet) {
      this.budget += this.selectedInternet.price;
    }
  }

  canAddMoreLines(): boolean {
    return this.selectedMobileLines.length < 5;
  }

  getSelectedLineCount(tariff: Tariff): number {
    return this.selectedMobileLines.filter(line => line.name === tariff.name).length;
  }

  isValidBundle(): boolean {
    return this.selectedMobileLines.length >= 1 && this.selectedInternet !== null;
  }

}
