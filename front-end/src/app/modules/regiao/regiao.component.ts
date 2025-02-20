import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Regiao } from 'src/app/models/Regiao';
import { RegiaoService } from 'src/app/services/regiao.service';

@Component({
  selector: 'app-regiao',
  templateUrl: './regiao.component.html',
  styleUrls: ['./regiao.component.scss']
})
export class RegiaoComponent implements OnInit {

  displayedColumns: string[] = ['nome', 'status', 'acoes'];
  regioes$!: Observable<Regiao[]>;
  isLoading$!: Observable<boolean>;

  constructor(private regiaoService: RegiaoService) { }

  ngOnInit() {
    this.regiaoService.carregarRegioes();
    this.regioes$ = this.regiaoService.regioes$;
    this.isLoading$ = this.regiaoService.isLoading$;
  }
  
  toggleStatus(regiao: any) {
    if(regiao.ativo)
      this.regiaoService.desativarRegiao(regiao.id);
    else
    this.regiaoService.activateRegiao(regiao.id);
  }
}
