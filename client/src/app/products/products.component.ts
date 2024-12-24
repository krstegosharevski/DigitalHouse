import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../_services/products.service';
import { ProductDto } from '../_models/productDto';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products : ProductDto[] = []

  constructor(private productService : ProductsService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(){
    this.productService.getAllProducts().subscribe({
      next: (data) => {
        this.products = data
      },
      error: (err) => console.log("Error loading products", err)
    })
  } 

}
