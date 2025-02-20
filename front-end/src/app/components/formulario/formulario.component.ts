import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Cidade } from 'src/app/Models/Cidades';
import { Regiao } from 'src/app/models/Regiao';
import { RegiaoCidade } from 'src/app/models/RegiaoCidade';
import { CidadeService } from 'src/app/services/cidade.service';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html',
  styleUrls: ['./formulario.component.scss']
})
export class FormularioComponent implements OnInit {
  @Input() regiao: Regiao = { id: '', nome: '', regiaoCidades: [], ativo: true };
  @Output() salvar = new EventEmitter<Regiao>();

  form!: FormGroup;
  // todasCidades: Cidade[] = [
  //   {
  //     id: "f399761f-600f-4616-9021-00739846110c",
  //     nome: "Rio Branco",
  //     uf: "AC"
  //   },
  //   {
  //     id: "666caac1-443a-4a18-8ab3-bdc18ec063d6",
  //     nome: "São Paulo",
  //     uf: "SP"
  //   },
  //   {
  //     id: "0ef783a5-79ef-4e47-98f6-11111a8ca395",
  //     nome: "Florianópolis",
  //     uf: "SC"
  //   },
  //   {
  //     id: "7ebef894-0cab-46f4-b9a5-ef1d12df17f4",
  //     nome: "Boa Vista",
  //     uf: "RR"
  //   },
  //   {
  //     id: "7fec79b1-6fc1-45d9-a836-67f7ff36d30c",
  //     nome: "Porto Velho",
  //     uf: "RO"
  //   },
  //   {
  //     id: "9b9e9b69-475b-4ba8-86d1-8369d08fdd53",
  //     nome: "Porto Alegre",
  //     uf: "RS"
  //   },
  //   {
  //     id: "b67794c3-3ef0-407f-ab96-7dbce67a6756",
  //     nome: "Natal",
  //     uf: "RN"
  //   },
  //   {
  //     id: "0a8e2ccf-6846-4e11-aafe-455b2a6c88cb",
  //     nome: "Rio de Janeiro",
  //     uf: "RJ"
  //   },
  //   {
  //     id: "35963c8a-8381-4517-bdcc-627312fec76a",
  //     nome: "Teresina",
  //     uf: "PI"
  //   },
  //   {
  //     id: "02f4cc32-eaf2-47da-a8e7-95adb3567899",
  //     nome: "Recife",
  //     uf: "PE"
  //   },
  //   {
  //     id: "baf219fe-e907-4b6e-80ea-3eb6e11e98f0",
  //     nome: "Curitiba",
  //     uf: "PR"
  //   },
  //   {
  //     id: "cf8aba20-291e-4c70-844f-af48bf4a5aa3",
  //     nome: "João Pessoa",
  //     uf: "PB"
  //   },
  //   {
  //     id: "5e14833a-d2bf-4e1e-a0ee-df8d885d85f8",
  //     nome: "Aracaju",
  //     uf: "SE"
  //   },
  //   {
  //     id: "5361609b-1f0f-4f3a-923d-5acbfffe322a",
  //     nome: "Belém",
  //     uf: "PA"
  //   },
  //   {
  //     id: "6c59f6a1-5d6f-41e0-8480-d428df67e941",
  //     nome: "Campo Grande",
  //     uf: "MS"
  //   },
  //   {
  //     id: "2b2c5779-a72c-4f7d-af4e-9ea9e0dc1c77",
  //     nome: "Cuiabá",
  //     uf: "MT"
  //   },
  //   {
  //     id: "d901f51d-f512-4e38-9013-530e07e2dc14",
  //     nome: "São Luís",
  //     uf: "MA"
  //   },
  //   {
  //     id: "ddac05e4-f08e-4ded-b97e-fe1e120d127b",
  //     nome: "Goiânia",
  //     uf: "GO"
  //   },
  //   {
  //     id: "5609137d-60a4-42c1-80cd-95442c8e4cd3",
  //     nome: "Vitória",
  //     uf: "ES"
  //   },
  //   {
  //     id: "752ad469-4686-43a9-a1a3-d8b4e031f065",
  //     nome: "Brasília",
  //     uf: "DF"
  //   },
  //   {
  //     id: "3b65ac5c-6230-4220-82f5-b7144f84c4ec",
  //     nome: "Fortaleza",
  //     uf: " CE"
  //   },
  //   {
  //     id: "bdd23b70-787e-44aa-97ab-a04bbf5b9b1a",
  //     nome: "Salvador",
  //     uf: "BA"
  //   },
  //   {
  //     id: "236f52f2-9705-4c35-b75b-9fd0572e02f0",
  //     nome: "Manaus",
  //     uf: "AM"
  //   },
  //   {
  //     id: "445066f0-ca54-4315-90d0-224abb29b0a7",
  //     nome: "Macapá",
  //     uf: "AP"
  //   },
  //   {
  //     id: "b847fecb-a9da-4138-852a-a3a88085baea",
  //     nome: "Maceió",
  //     uf: "AL"
  //   },
  //   {
  //     id: "a08eda78-899e-401d-a4a9-c5f8e6e4fe6b",
  //     nome: "Belo Horizonte",
  //     uf: "MG"
  //   },
  //   {
  //     id: "d06c6968-c64b-420f-a9ec-bb8769e7b00c",
  //     nome: "Palmas",
  //     uf: "TO"
  //   }
  // ]

  // todasCidades:  Observable<Cidade[]>;
  
  todasCidades: Cidade[] = [];
  isLoading$!: Observable<boolean>;
  cidadesDisponiveis: Cidade[] = [];
  cidadesAdicionadas: RegiaoCidade[] = [];

  constructor(private fb: FormBuilder, private router: Router, private cidadesService: CidadeService ) { }

  ngOnInit() {
    this.cidadesService.carregarCidades() 
    this.cidadesService.cidades$.subscribe((cidades: Cidade[]) => {
      this.todasCidades = cidades;
      this.atualizarCidadesDisponiveis();
    });
    this.isLoading$ = this.cidadesService.isLoading$;

    this.inicializarFormulario();
  }

  inicializarFormulario(): void {
    this.form = this.fb.group({
      nome: [this.regiao.nome, Validators.required],
      regiaoCidades: this.fb.array(this.regiao.regiaoCidades.map(rc => this.criarGrupoCidade(rc))),
      novaCidade: [null, Validators.required]
    });
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
      let novaRegiaoCidade: RegiaoCidade = { id: "", cidade: novaCidade };
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
    this.cidadesDisponiveis = this.todasCidades.filter(
      cidade => !this.cidadesAdicionadas.some(rc => rc.cidade.id === cidade.id)
    );
  }

  salvarRegiao(): void {
    if (this.form.valid) {
      const regiaoAtualizada: Regiao = {
        id: this.regiao.id || '',
        nome: this.form.value.nome,
        regiaoCidades: this.form.value.regiaoCidades.map((item: any) => item.regiaoCidade),
        ativo: this.regiao.ativo
      };
      this.salvar.emit(regiaoAtualizada);
    }
  }

  cancelar(): void {
    this.router.navigate(['/regiao']);
  }
}
