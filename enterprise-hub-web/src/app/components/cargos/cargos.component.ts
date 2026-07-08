import { Component, OnInit } from '@angular/core';

import { Cargo } from '../../models';
import { ServicioApi } from '../../services/servicio-api.service';

@Component({
  selector: 'app-cargos',
  templateUrl: './cargos.component.html',
  standalone: false
})
export class CargosComponent implements OnInit {
  positions: Cargo[] = [];
  positionForm = this.createForm();
  feedback = '';

  constructor(private readonly servicioApi: ServicioApi) {}

  ngOnInit(): void {
    this.loadPositions();
  }

  savePosition(): void {
    this.servicioApi.saveJobPosition(this.positionForm).subscribe({
      next: () => {
        this.feedback = 'Cargo guardado correctamente.';
        this.positionForm = this.createForm();
        this.loadPositions();
      },
      error: () => (this.feedback = 'No fue posible guardar el cargo.')
    });
  }

  editPosition(position: Cargo): void {
    this.positionForm = { ...position };
    this.feedback = 'Editando cargo seleccionado.';
  }

  deletePosition(id: number): void {
    this.servicioApi.deleteJobPosition(id).subscribe({
      next: () => {
        this.feedback = 'Cargo eliminado.';
        this.loadPositions();
      },
      error: () => (this.feedback = 'No se pudo eliminar. Verifica si tiene empleados asociados.')
    });
  }

  resetForm(): void {
    this.positionForm = this.createForm();
    this.feedback = '';
  }

  private loadPositions(): void {
    this.servicioApi.getJobPositions().subscribe((data) => (this.positions = data));
  }

  private createForm(): Partial<Cargo> {
    return { title: '', level: '', baseSalary: 0 };
  }
}
