import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MypasswordsRoutingModule } from './mypasswords-routing.module';
import { MypasswordsComponent } from './mypasswords.component';
import {TableModule} from "primeng/table";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";
import {FormsModule} from "@angular/forms";
import {AutoFocusModule} from "primeng/autofocus";
import {SharedModule} from "../../../shared/shared.module";


@NgModule({
  declarations: [
    MypasswordsComponent
  ],
  imports: [
    CommonModule,
    MypasswordsRoutingModule,
    TableModule,
    InputTextModule,
    ButtonModule,
    RippleModule,
    FormsModule,
    AutoFocusModule,
    SharedModule
  ]
})
export class MypasswordsModule { }
