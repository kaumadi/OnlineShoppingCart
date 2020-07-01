import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductViewComponent } from './product-view/product-view.component';
import { ProductCartComponent } from './product-cart/product-cart.component';


const routes: Routes = [ {
   path: '', component: ProductViewComponent, pathMatch: 'full' },
   { path: 'productCart', component: ProductCartComponent },
   { path: '**', redirectTo: '/' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

