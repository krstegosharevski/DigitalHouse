import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BrandDto } from 'src/app/_models/brandDto';
import { BrandsService } from 'src/app/_services/brands.service';

@Component({
  selector: 'app-product-filters',
  templateUrl: './product-filters.component.html',
  styleUrls: ['./product-filters.component.css']
})
export class ProductFiltersComponent implements OnInit {
  @Output() searchFilters = new EventEmitter<{ minPrice: number, maxPrice: number, brands: number[] }>();
  @Input() category!: string;

  brands: BrandDto[] = [];
  selectedBrands: number[] = []
  selectedPrice: string | null = null;
  minPrice: number = 0;
  maxPrice: number = 150000

  priceRanges = [
    { label: '0 - 6 000 MKD', value: '0-6000' },
    { label: '6 000 - 12 000 MKD', value: '6000-12000' },
    { label: '12 000 - 24 000 MKD', value: '12000-24000' },
    { label: '24 000 MKD +', value: '24000+' }
  ];

  constructor(private brandsService: BrandsService) { }

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands(): void {
    if(this.category){
      this.brandsService.getBrandsByCategory(this.category).subscribe({
        next: (brands) => {
          this.brands = brands;
        },
        error: (error) => {
          console.error('Error loading brands:', error);
        }
      });
    }else{
      this.brandsService.getAllBrands().subscribe({
        next: (brands) => {
          this.brands = brands;
        },
        error: (error) => {
          console.error('Error loading brands:', error);
        }
      });
    }
    
  }


  onPriceSelected(priceRange: string): void {
    this.selectedPrice = priceRange;
    const regex = /(\d[\d\s]*)/g;

    const matches = priceRange.match(regex);

    if (matches!.length === 1) {
      this.minPrice = parseInt(matches![0].replace(/\s/g, ''), 10);
      this.maxPrice = 150000; 
    } else if (matches!.length === 2) {
      this.minPrice = parseInt(matches![0].replace(/\s/g, ''), 10);
      this.maxPrice = parseInt(matches![1].replace(/\s/g, ''), 10);
    }
  }

  onBrandToggle(brandId: number, event: Event): void {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      this.selectedBrands.push(brandId);
    } else {
      const index = this.selectedBrands.indexOf(brandId);
      if (index !== -1) {
        this.selectedBrands.splice(index, 1);
      }
    }
  }

  applyFilters(): void {
    this.searchFilters.emit({
      minPrice: this.minPrice,
      maxPrice: this.maxPrice,
      brands: this.selectedBrands
    });
  }

}
