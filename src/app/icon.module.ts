import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DomSanitizer} from "@angular/platform-browser";
import {MatIconRegistry} from "@angular/material/icon";

const icons: string[] = [
  'profile'
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class IconModule {
  constructor(
    private domS: DomSanitizer,
    private matIconsS: MatIconRegistry
  ) {
    icons.forEach(i => this.matIconsS.addSvgIcon(i, this.domS.bypassSecurityTrustResourceUrl(`/assets/img/svg/${i}.svg`)));
  }
}
