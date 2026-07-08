import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PanelComponent } from './components/panel/panel.component';
import { DepartamentosComponent } from './components/departamentos/departamentos.component';
import { CargosComponent } from './components/cargos/cargos.component';
import { EmpleadosComponent } from './components/empleados/empleados.component';
import { SolicitudesPermisoComponent } from './components/solicitudes-permiso/solicitudes-permiso.component';

const routes: Routes = [
  { path: '', component: PanelComponent },
  { path: 'departments', component: DepartamentosComponent },
  { path: 'positions', component: CargosComponent },
  { path: 'employees', component: EmpleadosComponent },
  { path: 'leave-requests', component: SolicitudesPermisoComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
