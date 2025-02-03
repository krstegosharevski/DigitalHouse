import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AddProductDto } from 'src/app/_models/addProductDto';
import { BrandDto } from 'src/app/_models/brandDto';
import { CategoryDto } from 'src/app/_models/categoryDto';
import { Color } from 'src/app/_models/color';
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
  newProduct? : AddProductDto = undefined;
  
  showErrorBanner: boolean = false;
  showSuccessBanner: boolean = false;
  @ViewChild('productForm') productForm!: NgForm;

  constructor(
    private colorsService: ColorsService, 
    private brandService: BrandsService, 
    private categoryService: CategoriesService,
    private productService: ProductsService
  ) { }

  ngOnInit(): void {
    this.newProduct = {
      name: '',
      price: 0,
      description: '',
      isPresent: false,
      categoryId: 0,
      brandId: 0,
      colorIds: []
    };
    this.getColors()
    this.getBrands()
    this.getCategories()
  }

  getColors() {
    this.colorsService.getAllColors().subscribe({
      next: (colors) => {
        this.colors = colors
      },
      error: (err) => console.error(err)
    })
  }

  getBrands(){
    this.brandService.getAllBrands().subscribe({
      next: (brands) => {
        this.brands = brands
      },
      error: (err) => console.error(err)
    })
  }

  getCategories(){
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

    if(this)
    this.newProduct!.colorIds = this.selectedColors;

    if (!this.selectedFile) {
      console.error("No file selected!");
      return;
    }

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
    formData.append('file', this.selectedFile);

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
