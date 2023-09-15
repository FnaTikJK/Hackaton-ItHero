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
import { ExecutorsComponent } from './components/executors/executors.component';
import {ExecutorFullNamePipe} from "../../shared/pipes/executor-full-name.pipe";
import {MatChipsModule} from "@angular/material/chips";
import {ExecutorNameFilterPipe} from "../../shared/pipes/executor-name-filter.pipe";
import {NgxPaginationModule} from "ngx-pagination";
import { ProfileComponent } from './components/profile/profile.component';
import { CompanyComponent } from './components/company/company.component';
import {FileValueAccessorComponent} from "../../shared/components/file-value-accessor/file-value-accessor.component";
import {MatDialogModule} from "@angular/material/dialog";
import {MatExpansionModule} from "@angular/material/expansion";
import { ChatComponent } from './components/chat/chat.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'requests'},
  { path: 'requests', component: RequestsComponent },
  { path: 'requests/:id', component: RequestComponent },
  { path: 'create-request', component: CreateRequestComponent },
  { path: 'executors', component: ExecutorsComponent },
  { path: 'profiles/:id', component: ProfileComponent },
  { path: 'company/:id', component: CompanyComponent },
]

@NgModule({
  declarations: [
    RequestsComponent,
    CreateRequestComponent,
    RequestComponent,
    ExecutorsComponent,
    ProfileComponent,
    CompanyComponent,
    ChatComponent
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
    MatNativeDateModule,
    ExecutorFullNamePipe,
    MatChipsModule,
    ExecutorNameFilterPipe,
    NgxPaginationModule,
    FileValueAccessorComponent,
    MatDialogModule,
    MatExpansionModule
  ]
})
export class MainModule { }
