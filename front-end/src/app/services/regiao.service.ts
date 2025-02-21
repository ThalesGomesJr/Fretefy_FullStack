import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { tap, finalize, catchError } from 'rxjs/operators';
import { Regiao } from '../models/Regiao';
import { environment } from '../../environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Arquivo } from '../Models/Arquivo';

@Injectable({
  providedIn: 'root'
})
export class RegiaoService {
  
  private apiUrl = environment.apiUrl + "regiao/";

  private regioesSubject = new BehaviorSubject<Regiao[]>([]);
  regioes$: Observable<Regiao[]> = this.regioesSubject.asObservable();

  private regioaoEdicaoSubject = new BehaviorSubject<Regiao>(null);
  regioaoEdicao$: Observable<Regiao> = this.regioaoEdicaoSubject.asObservable();

  private arquivoSubject = new BehaviorSubject<Arquivo>(null);
  arquivo$: Observable<Arquivo> = this.arquivoSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(true);
  isLoading$ = this.loadingSubject.asObservable();

  private sucessoSubject = new BehaviorSubject<boolean>(false);
  sucesso$ = this.sucessoSubject.asObservable();

  constructor(private http: HttpClient, private snackBar: MatSnackBar) {}

  carregarRegioes() {
    this.loadingSubject.next(true);
    this.http.get<Regiao[]>(this.apiUrl + "list" )
      .pipe(
        tap(regioes => this.regioesSubject.next(regioes)),
        finalize(() => this.loadingSubject.next(false))
      ).subscribe();
  }

  carregarRegiao(id: string) {
    this.loadingSubject.next(true);
    const url = `${this.apiUrl}${id}`;
    this.http.get<Regiao>(url)
      .pipe(
        tap(regiao => this.regioaoEdicaoSubject.next(regiao)),
        finalize(() => this.loadingSubject.next(false))
      ).subscribe();
  }
  
  CreateRegiao(regiao: Regiao){
    this.loadingSubject.next(true);
    this.sucessoSubject.next(false);
    this.http.post<true>(this.apiUrl, regiao)
      .pipe(
        tap(
          () => this.sucessoSubject.next(true),
          () => this.carregarRegioes()),
        catchError(error => {
          () => this.sucessoSubject.next(false);
          
          this.snackBar.open(error.error, 'Fechar', {
            duration: 3000,
            panelClass: ['error-snackbar']
          });
          
          return throwError(() => error);
        }),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe();
  }

  UpdateRegiao(regiao: Regiao){
    this.loadingSubject.next(true);
    this.sucessoSubject.next(false);
    this.http.put<true>(this.apiUrl, regiao)
      .pipe(
        tap(
          () => this.sucessoSubject.next(true),
          () => this.carregarRegioes()),
        catchError(error => {
          () => this.sucessoSubject.next(false);
          
          this.snackBar.open(error.error, 'Fechar', {
            duration: 3000,
            panelClass: ['error-snackbar']
          });
          
          return throwError(() => error);
        }),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe();
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

  carregarArquivo() {
    this.loadingSubject.next(true);
    this.http.get<Arquivo>(this.apiUrl + "export-list" )
      .pipe(
        tap(arq => this.arquivoSubject.next(arq)),
        finalize(() => this.loadingSubject.next(false))
      ).subscribe();
  }

}
