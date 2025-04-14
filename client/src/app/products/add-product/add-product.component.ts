import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AddProductDto } from 'src/app/_models/addProductDto';
import { BrandDto } from 'src/app/_models/brandDto';
import { CategoryDto } from 'src/app/_models/categoryDto';
import { Color } from 'src/app/_models/color';
import { ProductColorDto } from 'src/app/_models/productColorDto';
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
  categories: CategoryDto[] = [];
  brands: BrandDto[] = [];
  colors: Color[] = [];
  selectedColorQuantities: Map<number, number> = new Map<number, number>();
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
      const idParam = params.get('id'); 
      if (idParam) {
        this.id = +idParam;
      }
    });

    if (this.id != null) {
      this.isEditMode = true;
      this.productService.getProductForEdit(this.id).subscribe({
        next: (product) => {
          this.newProduct = {
            name: product.name,
            price: product.price,
            description: product.description,
            isPresent: product.isPresent,
            categoryId: product.category,
            brandId: product.brand,
            productColors: product.productColors || []
          };
          // Initialize selected colors with quantities
          product.productColors?.forEach(pc => {
            this.selectedColorQuantities.set(pc.colorId, pc.quantity);
          });
        }
      });
    } else {
      this.newProduct = {
        name: '',
        price: 0,
        description: '',
        isPresent: false,
        categoryId: 0,
        brandId: 0,
        productColors: []
      };
    }
    this.getColors();
    this.getBrands();
    this.getCategories();
  }

  getColors() {
    this.colorsService.getAllColors().subscribe({
      next: (colors) => {
        this.colors = colors;
      },
      error: (err) => console.error(err)
    });
  }

  getBrands() {
    this.brandService.getAllBrands().subscribe({
      next: (brands) => {
        this.brands = brands;
      },
      error: (err) => console.error(err)
    });
  }

  getCategories() {
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (err) => console.error(err)
    });
  }

  toggleColorSelection(colorId: number) {
    if (this.selectedColorQuantities.has(colorId)) {
      this.selectedColorQuantities.delete(colorId);
    } else {
      this.selectedColorQuantities.set(colorId, 0);
    }
  }

  onQuantityChange(event: Event, colorId: number) {
    const input = event.target as HTMLInputElement;
    const quantity = parseInt(input.value, 10);
    if (!isNaN(quantity) && quantity >= 0) {
      this.selectedColorQuantities.set(colorId, quantity);
    }
  }

  isColorSelected(colorId: number): boolean {
    return this.selectedColorQuantities.has(colorId);
  }

  getColorQuantity(colorId: number): number {
    return this.selectedColorQuantities.get(colorId) || 0;
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
      productColors: []
    };
    this.selectedColorQuantities.clear();
    this.selectedFile = null;
    this.showErrorBanner = false;
  }

  onSubmit(form: NgForm) {
    if (form.invalid) {
      this.showErrorBanner = true;
      return;
    }

    const productColors: ProductColorDto[] = Array.from(this.selectedColorQuantities.entries())
      .map(([colorId, quantity]) => ({
        colorId,
        quantity
      }));

    const formData = new FormData();
    formData.append('Name', this.newProduct!.name);
    formData.append('Price', this.newProduct!.price.toString());
    formData.append('Description', this.newProduct!.description);
    formData.append('IsPresent', this.newProduct!.isPresent.toString());
    formData.append('CategoryId', this.newProduct!.categoryId.toString());
    formData.append('BrandId', this.newProduct!.brandId.toString());
    formData.append('ProductColors', JSON.stringify(productColors));
    
    if (this.selectedFile) {
      formData.append('file', this.selectedFile);
    }

    // Log form data for debugging
    formData.forEach((value, key) => {
      console.log(`${key}: ${value}`);
    });

    if (this.isEditMode) {
      this.productService.updateProduct(this.id, formData).subscribe({
        next: (response) => {
          console.log('Product updated successfully', response);
          this.resetForm();
          this.productForm.resetForm();
          this.showSuccessBanner = true;
        },
        error: (err) => {
          console.error('Error updating product', err);
          this.showErrorBanner = true;
        }
      });
    } else {
      console.log("vleze tuka");
      this.productService.addNewProduct(formData).subscribe({
        next: (response) => {
          console.log('Product added successfully', response);
          this.resetForm();
          this.productForm.resetForm();
          this.showSuccessBanner = true;
        },
        error: (err) => {
          console.error('Error adding product', err);
          this.showErrorBanner = true;
        }
      });
    }
  }
}