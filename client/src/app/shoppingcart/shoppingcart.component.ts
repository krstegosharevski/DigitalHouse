import { Component, OnInit } from '@angular/core';
import { ShoppingcartCartItem } from '../_models/shoppingCartItem';
import { ShoppingCartService } from '../_services/shopping-cart.service';

@Component({
  selector: 'app-shoppingcart',
  templateUrl: './shoppingcart.component.html',
  styleUrls: ['./shoppingcart.component.css']
})
export class ShoppingcartComponent implements OnInit {
  shoppingCartItems: ShoppingcartCartItem[] = [];
  username:string = "krste";

  constructor(private shoppingCartService : ShoppingCartService ) { }

  ngOnInit(): void {
    this.shoppingCartService.getAllShoppingCartItems(this.username).subscribe({
      next : (response) => {
          this.shoppingCartItems = response;
      },
      error : (err) => console.log(err)
    })
  }

}
