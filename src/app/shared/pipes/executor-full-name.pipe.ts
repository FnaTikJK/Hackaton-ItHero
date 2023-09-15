import { Pipe, PipeTransform } from '@angular/core';
import {IProfileData} from "../services/entities/profile.service";

@Pipe({
  name: 'executorFullName',
  standalone: true
})
export class ExecutorFullNamePipe implements PipeTransform {
  transform(executor: IProfileData | null | undefined): string {
    if (!executor) {
      return  '-';
    }
    return `${executor.secondName} ${executor.firstName} ${executor.thirdName}`;
  }
}
