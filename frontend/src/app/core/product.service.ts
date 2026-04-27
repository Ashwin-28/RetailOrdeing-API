import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductResponseDto, CreateProductDto, UpdateProductDto } from './models';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = 'http://localhost:5052/api/products';

  constructor(private http: HttpClient) {}

  getAll(categoryId?: number): Observable<ProductResponseDto[]> {
    const url = categoryId ? `${this.baseUrl}?categoryId=${categoryId}` : this.baseUrl;
    return this.http.get<ProductResponseDto[]>(url);
  }

  getById(id: number): Observable<ProductResponseDto> {
    return this.http.get<ProductResponseDto>(`${this.baseUrl}/${id}`);
  }

  create(dto: CreateProductDto): Observable<ProductResponseDto> {
    return this.http.post<ProductResponseDto>(this.baseUrl, dto);
  }

  update(id: number, dto: UpdateProductDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
