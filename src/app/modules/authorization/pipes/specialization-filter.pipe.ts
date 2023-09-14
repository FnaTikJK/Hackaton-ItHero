import { Pipe, PipeTransform } from '@angular/core';
import {ISpecialization} from "../../../shared/services/entities/company-entities.service";
import {map, Observable, of} from "rxjs";

@Pipe({
  name: 'specializationFilter'
})
export class SpecializationFilterPipe implements PipeTransform {
  transform(specializations: ISpecialization[] | null, selectedSpecializations: ISpecialization[] | null, nameFilter$: Observable<string | null>): Observable<ISpecialization[]> {
    const selectedSpecializationsIDs = selectedSpecializations?.map(spec => spec.id) ?? [];
    return specializations ? nameFilter$.pipe(
      map(v => {
        return specializations.filter(spec => spec.name.toLowerCase().includes(v?.toLowerCase() ?? '')
          && !selectedSpecializationsIDs.includes(spec.id));
      })
    ) : of([]);
  }
}
