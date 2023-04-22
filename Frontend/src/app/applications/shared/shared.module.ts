import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {AddRowDirective} from "./add-row.directive";

@NgModule({
  declarations: [AddRowDirective],
  imports: [
    CommonModule
  ],
  exports: [
    AddRowDirective
  ]
})
export class SharedModule { }
