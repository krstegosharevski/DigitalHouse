import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AddToCart } from 'src/app/_models/addToCart';
import { ProductDto } from 'src/app/_models/productDto';
import { AccountService } from 'src/app/_services/account.service';
import { ProductsService } from 'src/app/_services/products.service';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: 'app-product-searched',
  templateUrl: './product-searched.component.html',
  styleUrls: ['./product-searched.component.css']
})
export class ProductSearchedComponent implements OnInit {
  productName : string = '';
  product?: ProductDto = undefined
  currentUser$ = this.accountService.currentUser$;

  color : string = "";
  addCartDto: AddToCart | undefined
  username : string | undefined 

  constructor(private route: ActivatedRoute,
      private productService: ProductsService,
      private router : Router,
      private accountService : AccountService,
      private shoppingCartService : ShoppingCartService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productName = params['product'];
      this.loadProducts();
    });

    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.username = user?.username
      },
      error: (err) => console.log(err)
    })
  }

  loadProducts(){
    this.productService.getSearchedNameProduct(this.productName).subscribe({
      next: (productDto) => {
        this.product = productDto
      },
      error: (err) => console.error("Error loading the product", err)
    })
  }

  routeToEditPage(id:number){
    this.router.navigate([`admin/edit-product/${id}`], { state: { editMode: true } }).then(() => {
   }).catch(err => {
     console.error('Navigation failed', err);
   });
   }

   saveColor( color : string){
    this.addCartDto = {
      productId: this.product!.id,
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
