import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryDto } from 'src/app/_models/categoryDto';
import { CategoriesService } from 'src/app/_services/categories.service';

@Component({
  selector: 'app-product-categories',
  templateUrl: './product-categories.component.html',
  styleUrls: ['./product-categories.component.css']
})
export class ProductCategoriesComponent implements OnInit {
  categories: CategoryDto[] = [];
  //string[] = ['Smartphone', 'TV', 'Laptop'];

  constructor(private router: Router, private categoryService : CategoriesService) {}

  onCategoryClick(category: string): void {
    // Пренасочување кон рутата со категорија
    this.router.navigate(['/products', category]);
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(){
      this.categoryService.getAllCategories().subscribe({
        next: (categories) => {
          this.categories = categories
        }, 
        error: (error) => {
          console.error('Error loading brands:', error);
        }
      })
  }

}
