import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductDto } from 'src/app/_models/productDto';
import { AccountService } from 'src/app/_services/account.service';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-product-searched',
  templateUrl: './product-searched.component.html',
  styleUrls: ['./product-searched.component.css']
})
export class ProductSearchedComponent implements OnInit {
  productName : string = '';
  product?: ProductDto = undefined
  currentUser$ = this.accountService.currentUser$;

  constructor(private route: ActivatedRoute,
      private productService: ProductsService,
      private router : Router,
      private accountService : AccountService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productName = params['product'];
      this.loadProducts();
    });
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

}
