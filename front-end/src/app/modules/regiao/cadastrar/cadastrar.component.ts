import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Regiao } from 'src/app/models/Regiao';
import { RegiaoService } from 'src/app/services/regiao.service';

@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.component.html',
  styleUrls: ['./cadastrar.component.scss']
})
export class CadastrarComponent implements OnInit {

  regiao: Regiao = { id: '', nome: '', regiaoCidades: [], ativo: true };
  isLoading$!: Observable<boolean>;

  constructor(private regiaoService: RegiaoService, private router: Router) { }
  
  ngOnInit(): void {
  }

  salvarRegiao(regiao: any) {
    this.regiao = regiao;
    this.regiaoService.CreateRegiao(regiao)
    this.isLoading$ = this.regiaoService.isLoading$;
    
    this.regiaoService.sucesso$.subscribe(sucesso => {
      if (sucesso) {
        this.router.navigate(['/regiao']);
      }
    });
  }

}
