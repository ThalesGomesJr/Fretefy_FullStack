import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap, finalize } from 'rxjs/operators';
import { Regiao } from '../models/Regiao';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegiaoService {
  
  private apiUrl = environment.apiUrl + "regiao/";

  private regioesSubject = new BehaviorSubject<Regiao[]>([]);
  regioes$: Observable<Regiao[]> = this.regioesSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(true);
  isLoading$ = this.loadingSubject.asObservable();

  constructor(private http: HttpClient) {}

  carregarRegioes() {
    this.http.get<Regiao[]>(this.apiUrl + "list" )
      .pipe(
        tap(regioes => this.regioesSubject.next(regioes)),
        finalize(() => this.loadingSubject.next(false))
      ).subscribe();
  }
  
  activateRegiao(id: string){
    const url = `${this.apiUrl}activate?id=${id}`;
    this.http.put<true>(url, null)
      .pipe(tap(() => this.carregarRegioes()))
      .subscribe();
  }

  desativarRegiao(id: string){
    const url = `${this.apiUrl}deactivate?id=${id}`;
    this.http.put<true>(url, null)
      .pipe(tap(() => this.carregarRegioes()))
      .subscribe();
  }

}
