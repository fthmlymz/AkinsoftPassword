import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppLayoutComponent} from "./layout/app-layout/app-layout.component";

const routes: Routes = [
  { path: '', component: AppLayoutComponent},
  { path: 'auth', loadChildren: () => import('./applications/components/auth/auth.module').then(m => m.AuthModule)},
  { path: 'passwords', loadChildren: () => import('./applications/components/passwords/passwords.module').then(m => m.PasswordsModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
