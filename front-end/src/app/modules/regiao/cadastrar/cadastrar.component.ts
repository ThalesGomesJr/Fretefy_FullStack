import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.component.html',
  styleUrls: ['./cadastrar.component.scss']
})
export class CadastrarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  salvarRegiao(regiao: any) {
    console.log('Nova Região:', regiao);
    // Aqui você pode chamar o serviço para salvar a nova região na API
  }

}
