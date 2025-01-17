import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportsComponent } from './reports/reports.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ProductsComponent } from './products/products.component';
import { ProblemsComponent } from './problems/problems.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProductCategoriesComponent } from './product-tests/product-categories/product-categories.component';
import { ProductListComponent } from './product-tests/product-list/product-list.component';

const routes: Routes = [
  {path: 'aboutus', component: AboutUsComponent},
  {path: 'products', component: ProductsComponent},
  {path: 'problems', component: ProblemsComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'product-categories', component: ProductCategoriesComponent},
  {path: 'products/:category', component: ProductListComponent},
  {path: '', component: ReportsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
