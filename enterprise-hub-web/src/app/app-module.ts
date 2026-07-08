import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { PanelComponent } from './components/panel/panel.component';
import { DepartamentosComponent } from './components/departamentos/departamentos.component';
import { CargosComponent } from './components/cargos/cargos.component';
import { EmpleadosComponent } from './components/empleados/empleados.component';
import { SolicitudesPermisoComponent } from './components/solicitudes-permiso/solicitudes-permiso.component';

@NgModule({
  declarations: [
    App,
    PanelComponent,
    DepartamentosComponent,
    CargosComponent,
    EmpleadosComponent,
    SolicitudesPermisoComponent
  ],
  imports: [BrowserModule, FormsModule, HttpClientModule, AppRoutingModule],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [App]
})
export class AppModule {}
