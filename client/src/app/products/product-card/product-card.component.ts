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

  rutiraj(id:number){
   // this.router.navigate([`admin/edit-product/${id}`], { state: { editMode: true } });
   this.router.navigate([`admin/edit-product/${id}`], { state: { editMode: true } }).then(() => {
    console.log('Navigation was successful');
  }).catch(err => {
    console.error('Navigation failed', err);
  });
  }

}
