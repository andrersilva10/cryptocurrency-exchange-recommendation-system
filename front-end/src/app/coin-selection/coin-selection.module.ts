import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CurrenciesService } from '../shared/services/currencies.service';

import { CoinSelectionRoutingModule } from './coin-selection.routing.module';
import { FormComponent } from './form/form.component';



@NgModule({
  declarations: [FormComponent],
  imports: [
    CommonModule,
    CoinSelectionRoutingModule,
    FormsModule ,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule
  ],
  providers:[
    
  ]
})
export class CoinSelectionModule { }
