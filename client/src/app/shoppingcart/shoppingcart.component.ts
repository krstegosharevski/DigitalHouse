import { Component, OnInit } from '@angular/core';
import { ShoppingcartCartItem } from '../_models/shoppingCartItem';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { AccountService } from '../_services/account.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmationDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-shoppingcart',
  templateUrl: './shoppingcart.component.html',
  styleUrls: ['./shoppingcart.component.css']
})
export class ShoppingcartComponent implements OnInit {
  shoppingCartItems: ShoppingcartCartItem[] = [];
  currentUser$ = this.accountService.currentUser$;
  username: string | undefined
  totalPrice: number = 0;

  constructor(private shoppingCartService: ShoppingCartService,
    private accountService: AccountService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.username = user.username;
          this.shoppingCartService.getAllShoppingCartItems(this.username).subscribe({
            next: (response) => {
              this.shoppingCartItems = response;
              this.countPrice();
            },
            error: (err) => console.log(err)
          });
        }
      }
    });
  }

  getAllSHoppingCartItems() {
    if (this.username) {
      this.shoppingCartService.getAllShoppingCartItems(this.username).subscribe({
        next: (response) => {
          this.shoppingCartItems = response;
          this.countPrice();
        },
        error: (err) => console.log(err)
      });
    }
  }

  countPrice() {
    this.totalPrice = 0;
    this.shoppingCartItems.forEach(x => this.totalPrice += x.price);
  }


  removeFromCart(id: number) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.shoppingCartService.deleteItemFromCart(id).subscribe({
          next: (res) => {
            this.getAllSHoppingCartItems()
            this.countPrice()
          },
          error: (err) => console.log("error")
        })
      } else {
        console.log('Cancaled');
      }
    });
  }


  cancelCart() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (this.username) {
          this.shoppingCartService.cancelCart(this.username).subscribe({
            next: (res) => {
              this.getAllSHoppingCartItems();
              this.router.navigate(['/']);
            },
            error: (err) => console.log(err)
          });
        }
      } else {
        console.log('Cancaled');
      }
    });
  }

  buyNow() {
    this.shoppingCartService.createPayment(this.username!).subscribe((res: any) => {
      window.location.href = res.url; // редиректирај го корисникот кон Lemon Squeezy
    });
  }

}
