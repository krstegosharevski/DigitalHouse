import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductDto } from 'src/app/_models/productDto';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  
  @Input() product!: ProductDto;
  showDetails = false;
  currentUser$ = this.accountService.currentUser$;

  

  constructor(private accountService : AccountService, private router : Router){}

  toggleDetails(): void {
    this.showDetails = !this.showDetails;
  }

  ngOnInit(): void {
    
  }

  routeToEditPage(id:number){
   this.router.navigate([`admin/edit-product/${id}`], { state: { editMode: true } }).then(() => {
  }).catch(err => {
    console.error('Navigation failed', err);
  });
  }

  formatDescription(description: string): string {
    return description.replace(/ (?=[A-Za-z]+:)/g, '<br><span><br>');
  }

  // ovdeka gi imash glavnite raboti shto treba da mu gi pushtish na bekendot za da go dodade vo
  // koshnickata... Od koga ke go selektira
  saveColor( color : string){
    console.log(color + " " + this.product.id);
  }


}
