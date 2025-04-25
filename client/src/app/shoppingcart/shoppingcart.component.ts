import { Component, OnInit } from '@angular/core';
import { ShoppingcartCartItem } from '../_models/shoppingCartItem';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-shoppingcart',
  templateUrl: './shoppingcart.component.html',
  styleUrls: ['./shoppingcart.component.css']
})
export class ShoppingcartComponent implements OnInit {
  shoppingCartItems: ShoppingcartCartItem[] = [];
  currentUser$ = this.accountService.currentUser$;
  username: string | undefined

  constructor(private shoppingCartService: ShoppingCartService,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.username = user.username;
          this.shoppingCartService.getAllShoppingCartItems(this.username).subscribe({
            next: (response) => {
              this.shoppingCartItems = response;
            },
            error: (err) => console.log(err)
          });
        }
      }
    });
  }

  removeFromCart(id: number) {
    this.shoppingCartService.deleteItemFromCart(id).subscribe({
      next: (res) => {
        this.ngOnInit()
      },
      error: (err) => console.log("error")
    })
  }

  cancelCart() {
    if (this.username) {
      this.shoppingCartService.cancelCart(this.username).subscribe({
        next: (res) => {
          this.ngOnInit()
        },
        error: (err) => console.log(err)
      })
    }
  }

}
