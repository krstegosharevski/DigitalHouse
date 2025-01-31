import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from 'src/app/_models/productDto';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-product-searched',
  templateUrl: './product-searched.component.html',
  styleUrls: ['./product-searched.component.css']
})
export class ProductSearchedComponent implements OnInit {
  productName : string = '';
  product?: ProductDto = undefined

  constructor(private route: ActivatedRoute,private productService: ProductsService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productName = params['product'];
      this.loadProducts();
    });

    console.log(this.productName);
    console.log(this.product?.name);
  }

  loadProducts(){
    this.productService.getSearchedNameProduct(this.productName).subscribe({
      next: (productDto) => {
        this.product = productDto
      },
      error: (err) => console.error("Error loading the product", err)
    })
  }

}
