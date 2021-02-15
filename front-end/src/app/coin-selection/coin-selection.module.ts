import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormComponent } from './form/form.component';
import { CoinSelectionRoutingModule } from './coin-selection.routing.module';



@NgModule({
  declarations: [FormComponent],
  imports: [
    CommonModule,
    CoinSelectionRoutingModule
  ]
})
export class CoinSelectionModule { }
