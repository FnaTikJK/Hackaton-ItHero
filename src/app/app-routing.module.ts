import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'authorization', pathMatch: 'full'},
  { path: 'authorization', loadChildren: () => import('./modules/authorization/authorization.module').then(f => f.AuthorizationModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
