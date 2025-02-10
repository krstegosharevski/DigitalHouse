import { Component, OnInit } from '@angular/core';
import { CategoryDto } from 'src/app/_models/categoryDto';
import { CategoriesService } from 'src/app/_services/categories.service';

@Component({
  selector: 'app-product-category-navbar',
  templateUrl: './product-category-navbar.component.html',
  styleUrls: ['./product-category-navbar.component.css']
})
export class ProductCategoryNavbarComponent implements OnInit {

   categories: CategoryDto[] = [];
  
    constructor(private categoryService : CategoriesService) {}
  
    ngOnInit(): void {
      this.loadCategories();
    }
  
    loadCategories(){
        this.categoryService.getAllCategories().subscribe({
          next: (categories) => {
            this.categories = categories
          }, 
          error: (error) => {
            console.error('Error loading categories:', error);
          }
        })
    }
  

}
