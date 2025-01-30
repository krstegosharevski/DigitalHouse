import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportsComponent } from './reports/reports.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ProblemsComponent } from './problems/problems.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProductCategoriesComponent } from './products/product-categories/product-categories.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ShoppingcartComponent } from './shoppingcart/shoppingcart.component';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
  {path: 'aboutus', component: AboutUsComponent},
  {path: 'problems', component: ProblemsComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'product-categories', component: ProductCategoriesComponent},
  {path: 'products/:category', component: ProductListComponent},
  {path: 'shoppingcart', component: ShoppingcartComponent},
  {path: 'search', component: SearchComponent},
  {path: '', component: ReportsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
