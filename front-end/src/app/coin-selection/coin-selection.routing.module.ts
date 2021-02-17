import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormComponent } from './form/form.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { ResultsComponent } from './results/results.component';

const routes: Routes = [
  { path: '', component: FormComponent, children: [
    { path: ':trade/:to/results', component: ResultsComponent}
  ]},

];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
    ],
  exports: [RouterModule]
})
export class CoinSelectionRoutingModule { }
