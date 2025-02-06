import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { AddProductDto } from 'src/app/_models/addProductDto';
import { BrandDto } from 'src/app/_models/brandDto';
import { CategoryDto } from 'src/app/_models/categoryDto';
import { Color } from 'src/app/_models/color';
import { ProductDto } from 'src/app/_models/productDto';
import { BrandsService } from 'src/app/_services/brands.service';
import { CategoriesService } from 'src/app/_services/categories.service';
import { ColorsService } from 'src/app/_services/colors.service';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {

  categories: CategoryDto[] = []
  brands: BrandDto[] = []
  colors: Color[] = [];
  selectedColors: number[] = [];
  selectedFile: File | null = null;
  newProduct?: AddProductDto = undefined;

  showErrorBanner: boolean = false;
  showSuccessBanner: boolean = false;
  @ViewChild('productForm') productForm!: NgForm;

  @Input() isEditMode: boolean = false;
  id!: number;

  constructor(
    private colorsService: ColorsService,
    private brandService: BrandsService,
    private categoryService: CategoriesService,
    private productService: ProductsService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.isEditMode = false;
    
    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id'); // Земаме како стринг
      if (idParam) {
        this.id = +idParam; // Конвертираме во number
        console.log('ID from URL:', this.id);
      }
    });

    if (this.id != null) {
      this.isEditMode = true
      this.productService.getProductForEdit(this.id).subscribe({
        next : (product) =>{
          this.newProduct = {
            name: product.name,
            price: product.price,
            description: product.description,
            isPresent: product.isPresent,
            categoryId: product.category,
            brandId: product.brand,
            colorIds: product.colors
          }
          this.selectedColors = [...product.colors];
        }
      })
    } else {
      this.newProduct = {
        name: '',
        price: 0,
        description: '',
        isPresent: false,
        categoryId: 0,
        brandId: 0,
        colorIds: []
      };
    }
    this.getColors()
    this.getBrands()
    this.getCategories()
     console.log(this.isEditMode);
  }

  getColors() {
    this.colorsService.getAllColors().subscribe({
      next: (colors) => {
        this.colors = colors
      },
      error: (err) => console.error(err)
    })
  }

  getBrands() {
    this.brandService.getAllBrands().subscribe({
      next: (brands) => {
        this.brands = brands
      },
      error: (err) => console.error(err)
    })
  }

  getCategories() {
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories
      },
      error: (err) => console.error(err)
    })
  }

  toggleColorSelection(color: number) {
    const index = this.selectedColors.indexOf(color);
    if (index > -1) {
      this.selectedColors.splice(index, 1);
    } else {
      this.selectedColors.push(color);
    }
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  resetForm() {
    this.newProduct = {
      name: '',
      price: 0,
      description: '',
      isPresent: false,
      categoryId: 0,
      brandId: 0,
      colorIds: []
    };
    this.selectedColors = [];
    this.selectedFile = null;
    this.showErrorBanner = false;
  }

  onSubmit(form: NgForm) {
    if (form.invalid) {
      this.showErrorBanner = true; // Прикажи банер за грешка
      return;
    }

    if (this)
      this.newProduct!.colorIds = this.selectedColors;

   

    const formData = new FormData();
    formData.append('name', this.newProduct!.name);
    formData.append('price', this.newProduct!.price.toString());
    formData.append('description', this.newProduct!.description);
    formData.append('isPresent', this.newProduct!.isPresent.toString());
    formData.append('categoryId', this.newProduct!.categoryId.toString());
    formData.append('brandId', this.newProduct!.brandId.toString());
    this.newProduct!.colorIds.forEach((colorId, index) => {
      formData.append(`colorIds[${index}]`, colorId.toString());
    });
    if (this.selectedFile) {
      formData.append('file', this.selectedFile);
    }
   

    if (this.isEditMode) {
      
      formData.forEach(x => {
        console.log(x);
      })
      
      this.productService.updateProduct(this.id, formData).subscribe({
        next: (response) => {
          console.log('Product updated successfully', response);
          this.resetForm();
          this.productForm.resetForm();
          this.showSuccessBanner = true;
        },
        error: (err) => {
          console.error('Error updating product', err);
        }
      });
    } else {
      this.productService.addNewProduct(formData).subscribe({
        next: (response) => {
          console.log('Product added successfully', response);
          this.resetForm();
          this.productForm.resetForm();
          this.showSuccessBanner = true;
        },
        error: (err) => {
          console.error('Error adding product', err);
        }
      });
    }

    
  }


}
