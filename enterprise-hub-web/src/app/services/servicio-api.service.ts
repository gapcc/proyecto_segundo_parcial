import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Departamento, Empleado, Cargo, SolicitudPermiso } from '../models';

@Injectable({ providedIn: 'root' })
export class ServicioApi {
  private readonly baseUrl = this.obtenerUrlApi();

  constructor(private readonly http: HttpClient) {}

  getDepartments(): Observable<Departamento[]> {
    return this.http.get<Departamento[]>(`${this.baseUrl}/departments`);
  }

  saveDepartment(payload: Partial<Departamento>): Observable<Departamento> {
    if (payload.id) {
      return this.http.put<Departamento>(`${this.baseUrl}/departments/${payload.id}`, payload);
    }

    return this.http.post<Departamento>(`${this.baseUrl}/departments`, payload);
  }

  deleteDepartment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/departments/${id}`);
  }

  getJobPositions(): Observable<Cargo[]> {
    return this.http.get<Cargo[]>(`${this.baseUrl}/jobpositions`);
  }

  saveJobPosition(payload: Partial<Cargo>): Observable<Cargo> {
    if (payload.id) {
      return this.http.put<Cargo>(`${this.baseUrl}/jobpositions/${payload.id}`, payload);
    }

    return this.http.post<Cargo>(`${this.baseUrl}/jobpositions`, payload);
  }

  deleteJobPosition(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/jobpositions/${id}`);
  }

  getEmployees(): Observable<Empleado[]> {
    return this.http.get<Empleado[]>(`${this.baseUrl}/employees`);
  }

  saveEmployee(payload: Partial<Empleado>): Observable<Empleado> {
    if (payload.id) {
      return this.http.put<Empleado>(`${this.baseUrl}/employees/${payload.id}`, payload);
    }

    return this.http.post<Empleado>(`${this.baseUrl}/employees`, payload);
  }

  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/employees/${id}`);
  }

  getLeaveRequests(): Observable<SolicitudPermiso[]> {
    return this.http.get<SolicitudPermiso[]>(`${this.baseUrl}/leaverequests`);
  }

  saveLeaveRequest(payload: Partial<SolicitudPermiso>): Observable<SolicitudPermiso> {
    if (payload.id) {
      return this.http.put<SolicitudPermiso>(`${this.baseUrl}/leaverequests/${payload.id}`, payload);
    }

    return this.http.post<SolicitudPermiso>(`${this.baseUrl}/leaverequests`, payload);
  }

  deleteLeaveRequest(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/leaverequests/${id}`);
  }
  private obtenerUrlApi(): string {
    const config = (
      globalThis as typeof globalThis & {
        ENTERPRISE_HUB_CONFIG?: { apiBaseUrl?: string };
      }
    ).ENTERPRISE_HUB_CONFIG;

    return config?.apiBaseUrl?.replace(/\/$/, '') || 'http://localhost:5156/api';
  }
}