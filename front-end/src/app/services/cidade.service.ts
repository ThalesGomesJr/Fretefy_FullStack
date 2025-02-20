import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap, finalize } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Cidade } from '../Models/Cidade';

@Injectable({
  providedIn: 'root'
})
export class CidadeService {
  private apiUrl = environment.apiUrl + "cidade/";

  private cidadesSubject = new BehaviorSubject<Cidade[]>([]);
  cidades$: Observable<Cidade[]> = this.cidadesSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(true);
  isLoading$ = this.loadingSubject.asObservable();

  constructor(private http: HttpClient) { }

  carregarCidades() {
      this.http.get<Cidade[]>(this.apiUrl)
        .pipe(
          tap(cidades => this.cidadesSubject.next(cidades)),
          finalize(() => this.loadingSubject.next(false))
        ).subscribe();
    }
}
