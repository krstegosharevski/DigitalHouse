import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BrandDto } from 'src/app/_models/brandDto';
import { Pagination } from 'src/app/_models/pagination';
import { ProductDto } from 'src/app/_models/productDto';
import { ProductParams } from 'src/app/_models/productParams';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  category: string = '';
  products: ProductDto[] = [];
  pagination: Pagination | undefined
  productParams: ProductParams = new ProductParams();

  constructor(
    private route: ActivatedRoute,
    private productService: ProductsService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.category = params['category'];
      this.loadProducts();
    });
  }

  loadProducts(): void {
    this.productService.getAllProductsByCategory(this.category, this.productParams).subscribe({
      next: (response) => {
        if(response.result && response.pagination){
          this.products = response.result;
          this.pagination = response.pagination;
        }
      },
      error: (err) => console.error("Error loading products", err)
    });
  }

  pageChanged(event: any){
    if(this.productParams && this.productParams.pageNumber !== event.page){
      this.productParams.pageNumber = event.page;
      this.loadProducts();
    }
  }

  onSearchFiltersApplied(filters: { minPrice: number, maxPrice: number, brands: number[] }): void {
    this.productParams.MinPrice = filters.minPrice;
    this.productParams.MaxPrice = filters.maxPrice;
    this.productParams.BrandIds = filters.brands;
    this.loadProducts();
  }

}
