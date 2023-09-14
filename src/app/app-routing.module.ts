import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'main', pathMatch: 'full'},
  { path: 'authorization', loadChildren: () => import('./modules/authorization/authorization.module').then(f => f.AuthorizationModule) },
  { path: 'main', loadChildren: () => import('./modules/main/main.module').then(f => f.MainModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
