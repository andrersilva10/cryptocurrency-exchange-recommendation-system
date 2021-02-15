import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'coin-selection', loadChildren: () => import('./coin-selection/coin-selection.module').then(m => m.CoinSelectionModule)},
  { path: '', redirectTo: '/coin-selection', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
