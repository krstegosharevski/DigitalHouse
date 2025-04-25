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
        },
        error: (err) => console.log(err)
      });
    }
  }

  removeFromCart(id: number) {
    this.shoppingCartService.deleteItemFromCart(id).subscribe({
      next: (res) => {
        this.getAllSHoppingCartItems()
      },
      error: (err) => console.log("error")
    })
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
        console.log('Откажано');
      }
    });
  }

}
