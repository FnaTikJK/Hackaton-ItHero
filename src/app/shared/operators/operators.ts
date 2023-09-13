import {Observable, tap} from "rxjs";

export const isFirst = <T>(predicate: (val: T) => void) => {
  let first = true;
  return (source: Observable<T>) => {
    return source.pipe(
      tap({
        next: _ => {
          if (first) {
            predicate(_);
            first = false;
          }
        }
      })
    );
  };
};
