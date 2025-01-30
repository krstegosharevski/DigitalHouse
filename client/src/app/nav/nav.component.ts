import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  lang: string = '';
  currentUser$ = this.accountService.currentUser$;
  router: any;
  isSearchVisible: boolean = false;
  
 
  constructor(private translateService:TranslateService, private accountService: AccountService) { 

  }

  ngOnInit(): void {
    this.lang = localStorage.getItem('lang') || 'en';
    console.log(this.currentUser$);
  }

  ChangeLang(lang:any){
    const selectedLanguage = lang.target.value;

    localStorage.setItem('lang', selectedLanguage);

    this.translateService.use(selectedLanguage);
  }

  toggleSearch() {
    this.isSearchVisible = !this.isSearchVisible;
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}



