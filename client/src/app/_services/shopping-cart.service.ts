import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShoppingcartCartItem } from '../_models/shoppingCartItem';
import { environment } from 'src/environments/environment';
import { AddToCart } from '../_models/addToCart';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
 
  baseUrl = environment.apiUrl;
  
  constructor(private http : HttpClient) { }

  getAllShoppingCartItems(username: string) {
    return this.http.get<ShoppingcartCartItem[]>(
      this.baseUrl + "shoppingcart/get-shoppingcart",
      {
        params: { username: username }
      }
    );
  }

  addToCart(addToCartDto : AddToCart){
    return this.http.post( this.baseUrl + "shoppingcart/add-to-cart", addToCartDto)
  }

  deleteItemFromCart(itemId: number) {
    const params = new HttpParams().set('itemId', itemId.toString());
    return this.http.delete(this.baseUrl + 'shoppingcart/remove-item', { params });
  }
  
}
