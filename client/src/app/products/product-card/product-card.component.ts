import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AddToCart } from 'src/app/_models/addToCart';
import { ProductDto } from 'src/app/_models/productDto';
import { AccountService } from 'src/app/_services/account.service';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  
  @Input() product!: ProductDto;
  showDetails = false;
  currentUser$ = this.accountService.currentUser$;
  color : string = "";
  addCartDto: AddToCart | undefined
  username : string | undefined 
  

  constructor(private accountService : AccountService, private router : Router, private shoppingCartService : ShoppingCartService){}

  toggleDetails(): void {
    this.showDetails = !this.showDetails;
  }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.username = user?.username
      },
      error: (err) => console.log(err)
    })
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
    this.addCartDto = {
      productId: this.product.id,
      hexCode: color,
      username: this.username!
    }
    this.color = color;

    console.log("username:" + this.addCartDto.username + ", color:" + this.addCartDto.hexCode + ", id of prod: " +  this.addCartDto.productId)
  }

  addToCart(){
    console.log("username:" + this.addCartDto!.username + ", color:" + this.addCartDto!.hexCode + ", id of prod: " +  this.addCartDto!.productId)
    this.shoppingCartService.addToCart(this.addCartDto!).subscribe({
      next : (response) =>{
        console.log("succesfull added/created!")
      },
      error : (err) => console.log("you have error here!" + err)
    })
  }


}
