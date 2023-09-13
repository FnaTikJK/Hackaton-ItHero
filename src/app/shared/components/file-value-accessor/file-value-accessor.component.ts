import {ChangeDetectionStrategy, Component, forwardRef, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";

@Component({
  selector: 'app-file-value-accessor',
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  templateUrl: './file-value-accessor.component.html',
  styleUrls: ['./file-value-accessor.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => FileValueAccessorComponent),
    multi: true
  }],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FileValueAccessorComponent implements ControlValueAccessor{

  @Input() label: string = '';

  private onChange?: Function;
  registerOnChange(fn: Function): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: Function): void {}

  writeValue(obj: File): void {}

  emitValue(uploadEv: Event){
    // @ts-ignore
    this.onChange(uploadEv.target.files[0]);
  }
}
