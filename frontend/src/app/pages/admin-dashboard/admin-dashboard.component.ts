import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../../core/product.service';
import { AuthService } from '../../core/auth.service';
import { ProductResponseDto, CreateProductDto, UpdateProductDto } from '../../core/models';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  products: ProductResponseDto[] = [];
  showModal = false;
  editMode = false;
  editId = 0;
  form: any = {};

  constructor(private productSvc: ProductService, private authSvc: AuthService, private router: Router) {}

  ngOnInit() { this.load(); }

  load() {
    this.productSvc.getAll().subscribe(data => this.products = data);
  }

  openAdd() {
    this.editMode = false;
    this.form = { name: '', description: '', price: 0, imageUrl: '', stockQuantity: 0, lowStockThreshold: 5, categoryId: 1 };
    this.showModal = true;
  }

  openEdit(p: ProductResponseDto) {
    this.editMode = true;
    this.editId = p.id;
    this.form = { name: p.name, description: p.description, price: p.price, imageUrl: p.imageUrl, stockQuantity: p.stockQuantity, lowStockThreshold: 5, categoryId: p.categoryId };
    this.showModal = true;
  }

  save() {
    if (this.editMode) {
      this.productSvc.update(this.editId, this.form as UpdateProductDto).subscribe(() => { this.showModal = false; this.load(); });
    } else {
      this.productSvc.create(this.form as CreateProductDto).subscribe(() => { this.showModal = false; this.load(); });
    }
  }

  deleteProduct(id: number) {
    if (confirm('Delete this product?')) {
      this.productSvc.delete(id).subscribe(() => this.load());
    }
  }

  logout() { this.authSvc.logout(); this.router.navigate(['/login']); }
}
