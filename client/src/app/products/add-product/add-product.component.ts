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
  

  constructor() { }

  ngOnInit(): void {
  }

}
