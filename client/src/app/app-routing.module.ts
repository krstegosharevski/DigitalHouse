import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsComponent } from './about-us/about-us.component';
import { ProblemsComponent } from './problems/problems.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProductCategoriesComponent } from './products/product-categories/product-categories.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ShoppingcartComponent } from './shoppingcart/shoppingcart.component';
import { SearchComponent } from './search/search.component';
import { ProductSearchedComponent } from './products/product-searched/product-searched.component';
import { AddProductComponent } from './products/add-product/add-product.component';
import { AdminGuard } from './_guards/admin.guard';
import { HomeComponent } from './home/home.component';
import { ProblemsListComponent } from './problems-list/problems-list.component';
import { ChooseTariffComponent } from './tariffs/choose-tariff/choose-tariff.component';
import { Magenta1Component } from './tariffs/magenta1/magenta1.component';
import { MobileTariffComponent } from './tariffs/mobile-tariff/mobile-tariff.component';

const routes: Routes = [
  {path: 'aboutus', component: AboutUsComponent},
  {path: 'problems', component: ProblemsComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'product-categories', component: ProductCategoriesComponent},
  {path: 'products/:category', component: ProductListComponent},
  {path: 'shoppingcart', component: ShoppingcartComponent},
  {path: 'search', component: SearchComponent},
  {path: 'tariffs', component: ChooseTariffComponent},
  {path: 'tariffs/magenta1', component: Magenta1Component},
  {path: 'tariffs/mtariff', component: MobileTariffComponent},
  {path: 'searched-product/:product', component: ProductSearchedComponent},
  {path: 'admin/add-product', component: AddProductComponent, canActivate: [AdminGuard]},
  {path: 'admin/edit-product/:id', component: AddProductComponent, canActivate: [AdminGuard]},
  {path: 'admin/problem-list', component: ProblemsListComponent, canActivate: [AdminGuard]}, 
  {path: '', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
