import { Component, OnInit } from '@angular/core';
import { map, take } from 'rxjs';
import { InternetPackage } from 'src/app/_models/internetPackage';
import { Tariff } from 'src/app/_models/tariff';
import { AccountService } from 'src/app/_services/account.service';
import { InternetPackagesService } from 'src/app/_services/internet-packages.service';
import { Magenta1Service } from 'src/app/_services/magenta1.service';
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

  currentUser$ = this.accountService.currentUser$;
  userId : number = 0;

  constructor(
    private tariffsService: TariffsService,
    private internetPackageService: InternetPackagesService,
    private accountService : AccountService,
    private magenta1Service: Magenta1Service
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

  //create this method to be added new magenta for the user.
  //also display some baner idk...
  orderNow() {
    this.currentUser$.subscribe(user => {
      if (!user || !this.selectedInternet) return;
  
      const magentaData = {
        userId: Number(user.id), // 💥 ова е клучно
        budget: this.budget,
        internetPackageId: this.selectedInternet.id,
        magenta1TariffsId: this.selectedMobileLines.map(t => t.id)
      };
  
      console.log('Sending:', magentaData); // Провери го ова!
  
      this.magenta1Service.createMagenta1(magentaData).subscribe({
        next: (res) => {
          console.log('Created successfully', res);
          // прикажи банер или пренасочи
        },
        error: (err) => {
          console.error('Error from backend:', err.error.errors);
        }
      });
    });
  }
  
  

}
