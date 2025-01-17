import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BrandDto } from 'src/app/_models/brandDto';
import { BrandsService } from 'src/app/_services/brands.service';

@Component({
  selector: 'app-product-filters',
  templateUrl: './product-filters.component.html',
  styleUrls: ['./product-filters.component.css']
})
export class ProductFiltersComponent implements OnInit {
  @Output() priceFilter = new EventEmitter<string>();
  @Output() brandFilter = new EventEmitter<string[]>();

  brands: BrandDto[] = [];
  selectedBrands: Set<string> = new Set();

  priceRanges = [
    { label: '0 - 6 000 MKD', value: '0-6000' },
    { label: '6 000 - 12 000 MKD', value: '6000-12000' },
    { label: '12 000 - 24 000 MKD', value: '12000-24000' },
    { label: '24 000 MKD +', value: '24000+' }
  ];

  constructor(private brandsService: BrandsService) {}

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands(): void {
    this.brandsService.getAllBrands().subscribe({
      next: (brands) => {
        this.brands = brands;
      },
      error: (error) => {
        console.error('Error loading brands:', error);
      }
    });
  }

  onPriceSelected(priceRange: string): void {
    this.priceFilter.emit(priceRange);
  }

  onBrandToggle(brandName: string, event: Event): void {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      this.selectedBrands.add(brandName);
    } else {
      this.selectedBrands.delete(brandName);
    }
    this.brandFilter.emit(Array.from(this.selectedBrands));
  }

}
