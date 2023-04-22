import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MypasswordsComponent} from "./mypasswords.component";

const routes: Routes = [
  { path: '', component: MypasswordsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MypasswordsRoutingModule { }
