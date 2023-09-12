import { Directive, Input, TemplateRef, ViewContainerRef} from '@angular/core';


type ngLetContext<T> = {
  ngLet: T
}
@Directive({
  selector: '[ngLet]',
  standalone: true
})
export class NgLetDirective<T>{
  constructor(
    private vcr: ViewContainerRef,
    private templateRef: TemplateRef<ngLetContext<T>>
  ) {
    this.vcr.createEmbeddedView(this.templateRef, this.ngLetContext);
  }

  @Input() set ngLet(value: T){
    this.ngLetContext!.ngLet = value;
  }

  private ngLetContext?: ngLetContext<T> = {
    ngLet: null as T
  };

  static ngTemplateContextGuard<T>(dir: NgLetDirective<T>, ctx: unknown): ctx is ngLetContext<T> { return true; };
}
