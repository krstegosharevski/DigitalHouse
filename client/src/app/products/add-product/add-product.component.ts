import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {

  categories = [
    { id: 1, name: 'Смартфони' },
    { id: 2, name: 'Телевизори' },
    { id: 3, name: 'Тастатури' }
  ];
  selectedCategory: number = 0;
  colors = [
    { name: 'Red', hex: '#ff0000' },
    { name: 'Green', hex: '#00ff00' },
    { name: 'Blue', hex: '#0000ff' },
    { name: 'Black', hex: '#000000' },
    { name: 'White', hex: '#ffffff' }
  ];
  selectedColors: string[] = [];
  

  constructor() { }

  ngOnInit(): void {
  }

  toggleColorSelection(color: string) {
    const index = this.selectedColors.indexOf(color);
    if (index > -1) {
        this.selectedColors.splice(index, 1);
    } else {
        this.selectedColors.push(color);
    }
}

}
