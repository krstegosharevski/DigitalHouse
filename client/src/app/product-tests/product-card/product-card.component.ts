import { Component, Input, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/_models/productDto';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  
  @Input() product!: ProductDto;
  showDetails = false;

  toggleDetails(): void {
    this.showDetails = !this.showDetails;
  }

  ngOnInit(): void {
    
  }

}
