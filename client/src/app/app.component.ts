import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router, NavigationEnd } from '@angular/router';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title:string = 'Testing app';
  showNavbar = true;
  showFooter = true;

  constructor(private http: HttpClient, private translateService:TranslateService, private router: Router){
    this.translateService.setDefaultLang('en');
    this.translateService.use(localStorage.getItem('lang') || 'en');
    
  }

  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        // Скриј го навигациониот бар само на страната за најава
        this.showNavbar = !event.url.includes('/login') && !event.url.includes('/register');
        this.showFooter = !event.url.includes('/login') && !event.url.includes('/register') &&  !event.url.includes('/tariffs/magenta1');
      }
    });
  }

 
  
}
