import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FooterComponent } from './footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { AboutUsComponent } from './about-us/about-us.component';
import { ProblemsComponent } from './problems/problems.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProductCategoriesComponent } from './products/product-categories/product-categories.component';
import { ProductFiltersComponent } from './products/product-filters/product-filters.component';
import { ProductCardComponent } from './products/product-card/product-card.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ShoppingcartComponent } from './shoppingcart/shoppingcart.component';
import { SearchComponent } from './search/search.component';
import { ProductSearchedComponent } from './products/product-searched/product-searched.component';
import { AddProductComponent } from './products/add-product/add-product.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { HomeComponent } from './home/home.component';
import { ProductCategoryNavbarComponent } from './products/product-category-navbar/product-category-navbar.component';
import { ProblemsListComponent } from './problems-list/problems-list.component';
import { ChooseTariffComponent } from './tariffs/choose-tariff/choose-tariff.component';
import { Magenta1Component } from './tariffs/magenta1/magenta1.component';
import { MobileTariffComponent } from './tariffs/mobile-tariff/mobile-tariff.component';
import { PrepaidComponent } from './tariffs/prepaid/prepaid.component';
import { MagentaApproveComponent } from './magenta-approve/magenta-approve.component';

export function HttpLoaderFactory(http:HttpClient){
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    AboutUsComponent,
    ProblemsComponent,
    LoginComponent,
    RegisterComponent,
    ProductCategoriesComponent,
    ProductFiltersComponent,
    ProductCardComponent,
    ProductListComponent,
    ShoppingcartComponent,
    SearchComponent,
    ProductSearchedComponent,
    AddProductComponent,
    HasRoleDirective,
    HomeComponent,
    ProductCategoryNavbarComponent,
    ProblemsListComponent,
    ChooseTariffComponent,
    Magenta1Component,
    MobileTariffComponent,
    PrepaidComponent,
    MagentaApproveComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CarouselModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    PaginationModule.forRoot(),
    TranslateModule.forRoot(
      {
      loader:{
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps:[HttpClient]
      }
    }
    )
  ],
  providers: [HttpClient,
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
