import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormComponent } from './form/form.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';

const routes: Routes = [
  { path: '', component: FormComponent}
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    MatAutocompleteModule],
  exports: [RouterModule]
})
export class CoinSelectionRoutingModule { }
