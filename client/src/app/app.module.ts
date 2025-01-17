import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClient, HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FooterComponent } from './footer/footer.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ReportsComponent } from './reports/reports.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { AboutUsComponent } from './about-us/about-us.component';
import { ProductsComponent } from './products/products.component';
import { ProblemsComponent } from './problems/problems.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProductCategoriesComponent } from './product-tests/product-categories/product-categories.component';
import { ProductBrandNavComponent } from './product-tests/product-brand-nav/product-brand-nav.component';
import { ProductFiltersComponent } from './product-tests/product-filters/product-filters.component';
import { ProductCardComponent } from './product-tests/product-card/product-card.component';
import { ProductListComponent } from './product-tests/product-list/product-list.component';

export function HttpLoaderFactory(http:HttpClient){
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    ReportsComponent,
    AboutUsComponent,
    ProductsComponent,
    ProblemsComponent,
    LoginComponent,
    RegisterComponent,
    ProductCategoriesComponent,
    ProductBrandNavComponent,
    ProductFiltersComponent,
    ProductCardComponent,
    ProductListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CarouselModule,
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
  providers: [HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
