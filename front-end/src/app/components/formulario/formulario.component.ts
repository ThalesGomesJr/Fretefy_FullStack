import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Cidade } from 'src/app/Models/Cidade';
import { Regiao } from 'src/app/models/Regiao';
import { RegiaoCidade } from 'src/app/models/RegiaoCidade';
import { CidadeService } from 'src/app/services/cidade.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html',
  styleUrls: ['./formulario.component.scss']
})
export class FormularioComponent implements OnInit {
  @Input() regiao: Regiao;
  @Output() salvar = new EventEmitter<Regiao>();

  form!: FormGroup;
  
  todasCidades: Cidade[] = [];
  isLoading$!: Observable<boolean>;
  cidadesDisponiveis: Cidade[] = [];
  cidadesAdicionadas: RegiaoCidade[] = [];
  statusDescricao: string = "";
  constructor(private fb: FormBuilder, private router: Router, private cidadesService: CidadeService, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.cidadesService.carregarCidadesDisponiveis() 
    this.cidadesService.cidades$.subscribe((cidades: Cidade[]) => {
      cidades.forEach(cidade => {
        if (!this.todasCidades.some(c => c.id === cidade.id)) {
          this.todasCidades.push(cidade);
        }
      });

      this.atualizarCidadesDisponiveis();
    });
    this.isLoading$ = this.cidadesService.isLoading$;

    this.inicializarFormulario();
  }

  inicializarFormulario(): void {
    this.form = this.fb.group({
      nome: [this.regiao.nome, Validators.required],
      regiaoCidades: this.fb.array([]),
      ativo: [this.regiao.ativo],
      novaCidade: [null]
    });

    if (this.regiao?.regiaoCidades) {
      this.regiao.regiaoCidades.forEach(rc => {
        this.cidadesAdicionadas.push(rc);
        this.todasCidades.push(rc.cidade);
        this.cidades.push(this.criarGrupoCidade(rc));
      });
    }
  }

  get cidades(): FormArray {
    return this.form.get('regiaoCidades') as FormArray;
  }

  criarGrupoCidade(regiaoCidade: RegiaoCidade): FormGroup {
    return this.fb.group({ 
      regiaoCidade: [regiaoCidade, Validators.required],
      cidade: [regiaoCidade.cidade, Validators.required]
    });
  }

  adicionarCidade(): void {
    const novaCidade = this.form.get('novaCidade')?.value;
    if (novaCidade && !this.cidadesAdicionadas.some(rc => rc.cidade.id === novaCidade.id)) {
      let novaRegiaoCidade: RegiaoCidade = { id: null, cidade: novaCidade };
      this.cidades.push(this.criarGrupoCidade(novaRegiaoCidade));      
      this.cidadesAdicionadas.push(novaRegiaoCidade);
      
      this.atualizarCidadesDisponiveis();
    }
  }

  removerCidade(index: number): void {
    const cidadeRemovida = this.cidades.at(index).value.regiaoCidade;
    this.cidades.removeAt(index);
    this.cidadesAdicionadas = this.cidadesAdicionadas.filter(c => c !== cidadeRemovida);
    this.atualizarCidadesDisponiveis();
  }

  atualizarCidadesDisponiveis() {
    this.cidadesDisponiveis = this.todasCidades
      .filter(cidade => !this.cidadesAdicionadas.some(rc => rc.cidade.id === cidade.id))
      .sort((a, b) => a.nome.localeCompare(b.nome));
  }

  toggleAtivo(event: MatCheckboxChange) {
    this.form.get('ativo')?.setValue(event.checked);
  }

  compararCidades(c1: Cidade, c2: Cidade): boolean {
    return c1 && c2 ? c1.id === c2.id : c1 === c2;
  }

  salvarRegiao(): void {
    if (this.form.valid) {
      const regiaoAtualizada: Regiao = {
        id: this.regiao.id || '',
        nome: this.form.value.nome,
        regiaoCidades: this.form.value.regiaoCidades.map((item: any) => item.regiaoCidade),
        ativo: this.form.value.ativo
      };
      
      if(regiaoAtualizada.regiaoCidades.length === 0){
        this.snackBar.open('Adicione pelo menos uma cidade.', 'Fechar', {
          duration: 3000,
          panelClass: ['alert-snackbar']
        });
      }
      else
        this.salvar.emit(regiaoAtualizada);
    }
  }

  cancelar(): void {
    this.router.navigate(['/regiao']);
  }
}
