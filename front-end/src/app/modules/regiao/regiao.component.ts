import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Arquivo } from 'src/app/Models/Arquivo';
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
  arquivo$: Observable<Arquivo>;

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

  gerarArquivoXlsx() {
    this.regiaoService.carregarArquivo();
    this.regiaoService.arquivo$.subscribe((arq: Arquivo) => {
      if (arq?.file) {
        this.downloadBase64AsXlsx(arq.file, 'listagem_regioes.xlsx');
      }
    });

    this.isLoading$ = this.regiaoService.isLoading$;
  }

  downloadBase64AsXlsx(base64: string, fileName: string) {
    const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length);
  
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
  
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
  
    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}
