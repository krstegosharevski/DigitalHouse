import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

interface CategoryItem {
  name: string;
  route: string;
  subcategories: { name: string; route: string; }[];
}

@Component({
  selector: 'app-product-brand-nav',
  templateUrl: './product-brand-nav.component.html',
  styleUrls: ['./product-brand-nav.component.css']
})
export class ProductBrandNavComponent implements OnInit {

  
  categories: CategoryItem[] = [
    {
      name: 'Mobile Phones',
      route: '/mobile',
      subcategories: [
        { name: 'Smartphones', route: '/products/Smartphone' },
        { name: 'Feature Phones', route: '/products/feature-phones' },
        { name: 'Accessories', route: '/products/mobile-accessories' }
      ]
    },
    {
      name: 'Televisions',
      route: '/tv',
      subcategories: [
        { name: 'LED TVs', route: '/products/led-tvs' },
        { name: 'Smart TVs', route: '/products/smart-tvs' },
        { name: 'TV Accessories', route: '/products/tv-accessories' }
      ]
    },
    {
      name: 'Computers',
      route: '/computers',
      subcategories: [
        { name: 'Laptops', route: '/products/laptops' },
        { name: 'Desktops', route: '/products/desktops' },
        { name: 'Monitors', route: '/products/monitors' }
      ]
    }
  ];
  activeCategory: string = '';

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // Subscribe to route parameter changes
    this.route.params.subscribe(params => {
      this.activeCategory = params['category'] || '';
    });
  }

  // This method i dont use for now. I am using Angular routing to simplify
  // onCategorySelect(route: string) {
  //   this.router.navigate([route]);
  // }

}
