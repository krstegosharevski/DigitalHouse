import { Component, HostListener, Input, OnInit } from '@angular/core';
import { ProductsService } from '../_services/products.service';
import { ProductDto } from '../_models/productDto';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  products : ProductDto[] = []
  searchControl = new FormControl('');
  @Input() isActive: boolean = false;


  constructor(private productService: ProductsService) { 
  }

  ngOnInit(): void {
  }

  onSearch(search: string) : void{
    this.productService.getSearchedProducts(search).subscribe({
      next: (prods) =>{
        this.products = prods;
      },
      error: (error) => {
        console.error('Error loading brands:', error);
      }
    });
  }

  @HostListener('document:click', ['$event'])
  onClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.search-container') && this.isActive) {
      this.isActive = false;
    }
  }

}
