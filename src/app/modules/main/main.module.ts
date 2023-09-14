import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestsComponent } from './components/requests/requests.component';
import {Route, RouterModule, Routes} from "@angular/router";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {NgLetDirective} from "../../shared/directives/ng-let.directive";
import {MatIconModule} from "@angular/material/icon";
import { CreateRequestComponent } from './components/create-request/create-request.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {MatNativeDateModule, MatOptionModule} from "@angular/material/core";
import {MatSelectModule} from "@angular/material/select";
import {MatDatepickerModule} from "@angular/material/datepicker";
import { RequestComponent } from './components/request/request.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'requests'},
  { path: 'requests', component: RequestsComponent },
  { path: 'requests/:id', component: RequestComponent },
  { path: 'create-request', component: CreateRequestComponent }
]

@NgModule({
  declarations: [
    RequestsComponent,
    CreateRequestComponent,
    RequestComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatCardModule,
    MatButtonModule,
    NgLetDirective,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatOptionModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ]
})
export class MainModule { }
