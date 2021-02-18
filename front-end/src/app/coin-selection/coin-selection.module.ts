import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CoinSelectionRoutingModule } from './coin-selection.routing.module';
import { FormComponent } from './form/form.component';
import { ResultsComponent } from './results/results.component';
import { LineChartComponent } from './line-chart/line-chart.component';
import { ChartsModule } from 'ng2-charts';



@NgModule({
  declarations: [FormComponent, ResultsComponent,LineChartComponent],
  imports: [
    CommonModule,
    CoinSelectionRoutingModule,
    MatPaginatorModule,

    FormsModule ,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
    MatTableModule,
    MatSortModule,
    MatTooltipModule,
    ChartsModule
  ],
  providers:[
    
  ]
})
export class CoinSelectionModule { }
