import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OrderDto, PlaceOrderDto } from './models';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private baseUrl = 'http://localhost:5053/api/Orders';

  constructor(private http: HttpClient) { }

  getUserOrders(): Observable<OrderDto[]> {
    return this.http.get<OrderDto[]>(this.baseUrl);
  }

  getOrderById(id: number): Observable<OrderDto> {
    return this.http.get<OrderDto>(`${this.baseUrl}/${id}`);
  }

  getAllOrders(): Observable<OrderDto[]> {
    return this.http.get<OrderDto[]>(`${this.baseUrl}/all`);
  }

  placeOrder(dto: PlaceOrderDto): Observable<any> {
    return this.http.post(`${this.baseUrl}`, dto);
  }

  updateStatus(id: number, status: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}/status`, { status });
  }
}
