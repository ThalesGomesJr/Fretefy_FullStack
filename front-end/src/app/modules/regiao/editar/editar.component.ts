import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Regiao } from 'src/app/models/Regiao';
import { RegiaoService } from 'src/app/services/regiao.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.scss']
})
export class EditarComponent implements OnInit {
  regiao: Regiao = { id: '', nome: '', regiaoCidades: [], ativo: true };
  
  regiaoEdicao$!: Observable<Regiao>;
  isLoading$!: Observable<boolean>;

  constructor(private regiaoService: RegiaoService, private routeActive: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const id = this.routeActive.snapshot.paramMap.get('id');
    this.regiaoService.carregarRegiao(id);
    this.isLoading$ = this.regiaoService.isLoading$
    this.regiaoService.regioaoEdicao$.subscribe(r => {
      this.regiao = r;
    });
  }

}
