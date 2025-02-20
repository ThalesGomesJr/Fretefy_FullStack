import { RouterModule, Routes } from '@angular/router';
import { RegiaoComponent } from './regiao.component';
import { CadastrarComponent } from './cadastrar/cadastrar.component';
import { EditarComponent } from './editar/editar.component';

const routes: Routes = [
  { 
    path: '',
    component: RegiaoComponent
  },
  { path: 'cadastrar', 
    component: CadastrarComponent 
  },
  { path: 'editar/:id', 
    component: EditarComponent 
  }
];

export const  RegiaoRoutingModule = RouterModule.forChild(routes);