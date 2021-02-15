import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'coin-selection', loadChildren: './coin-selection/coin-selection.module#CoinSelectionModule'},
  { path: '', redirectTo: '/coin-selection', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
