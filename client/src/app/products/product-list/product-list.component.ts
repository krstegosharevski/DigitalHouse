import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BrandDto } from 'src/app/_models/brandDto';
import { Pagination } from 'src/app/_models/pagination';
import { ProductDto } from 'src/app/_models/productDto';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  category: string = '';
  products: ProductDto[] = [];
  filteredProducts: ProductDto[] = [];
  priceFilter: string = '';
  selectedBrands: string[] = [];
  pagination: Pagination | undefined
  pageNumber = 1
  pageSize = 2

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
    this.productService.getAllProductsByCategory(this.category, this.pageNumber, this.pageSize).subscribe({
      next: (response) => {
        if(response.result && response.pagination){
          this.products = response.result;
          this.pagination = response.pagination;
          this.applyFilters();
        }
      },
      error: (err) => console.error("Error loading products", err)
    });
  }

  pageChanged(event: any){
    if(this.pageNumber !== event.page){
      this.pageNumber = event.page;
      this.loadProducts();
    }
  }

  onPriceFilterSelected(priceRange: string): void {
    this.priceFilter = priceRange;
    this.applyFilters();
  }

  onBrandFilterSelected(brands: string[]): void {
    this.selectedBrands = brands;
    this.applyFilters();
  }

  private applyFilters(): void {
    this.filteredProducts = this.products.filter(product => {
      const matchesPrice = !this.priceFilter || this.checkPriceRange(product.price);
      const matchesBrand = this.selectedBrands.length === 0 || this.selectedBrands.includes(product.brandName);
      return matchesPrice && matchesBrand;
    });
  }

  private checkPriceRange(price: number): boolean {
    switch (this.priceFilter) {
      case '0-6000':
        return price >= 0 && price <= 6000;
      case '6000-12000':
        return price > 6000 && price <= 12000;
      case '12000-24000':
        return price > 12000 && price <= 24000;
      case '24000+':
        return price > 24000;
      default:
        return true;
    }
  }
}
