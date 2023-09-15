import { Pipe, PipeTransform } from '@angular/core';
import {IProfileData} from "../services/entities/profile.service";
import {ExecutorFullNamePipe} from "./executor-full-name.pipe";

@Pipe({
  name: 'executorNameFilter',
  standalone: true
})
export class ExecutorNameFilterPipe implements PipeTransform {

  constructor(
  ) {}
  transform(executors: IProfileData[] | null, nameFilter: string | null): IProfileData[] {
    return executors?.filter(e => `${e.secondName} ${e.firstName} ${e.thirdName}`.toLowerCase().includes((nameFilter as string).toLowerCase())) as IProfileData[];
  }

}
