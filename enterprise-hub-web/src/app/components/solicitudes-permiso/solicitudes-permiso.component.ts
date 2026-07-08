import { Component, OnInit } from '@angular/core';

import { ServicioApi } from '../../services/servicio-api.service';
import { Empleado, SolicitudPermiso } from '../../models';

@Component({
  selector: 'app-solicitudes-permiso',
  templateUrl: './solicitudes-permiso.component.html',
  standalone: false
})
export class SolicitudesPermisoComponent implements OnInit {
  leaveRequests: SolicitudPermiso[] = [];
  employees: Empleado[] = [];
  leaveRequestForm = this.createForm();
  feedback = '';

  constructor(private readonly servicioApi: ServicioApi) {}

  ngOnInit(): void {
    this.servicioApi.getEmployees().subscribe((data) => (this.employees = data));
    this.loadRequests();
  }

  saveLeaveRequest(): void {
    this.servicioApi.saveLeaveRequest(this.leaveRequestForm).subscribe({
      next: () => {
        this.feedback = 'Solicitud guardada correctamente.';
        this.leaveRequestForm = this.createForm();
        this.loadRequests();
      },
      error: () => (this.feedback = 'No fue posible guardar la solicitud.')
    });
  }

  editLeaveRequest(request: SolicitudPermiso): void {
    this.leaveRequestForm = {
      ...request,
      startDate: request.startDate.slice(0, 10),
      endDate: request.endDate.slice(0, 10)
    };
    this.feedback = 'Editando solicitud seleccionada.';
  }

  deleteLeaveRequest(id: number): void {
    this.servicioApi.deleteLeaveRequest(id).subscribe(() => {
      this.feedback = 'Solicitud eliminada.';
      this.loadRequests();
    });
  }

  resetForm(): void {
    this.leaveRequestForm = this.createForm();
    this.feedback = '';
  }

  private loadRequests(): void {
    this.servicioApi.getLeaveRequests().subscribe((data) => (this.leaveRequests = data));
  }

  private createForm(): Partial<SolicitudPermiso> {
    return {
      employeeId: 0,
      startDate: new Date().toISOString().slice(0, 10),
      endDate: new Date().toISOString().slice(0, 10),
      reason: '',
      status: 'Pendiente'
    };
  }
}
