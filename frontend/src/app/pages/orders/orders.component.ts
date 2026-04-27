import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { OrderService } from '../../core/order.service';
import { AuthService } from '../../core/auth.service';
import { OrderDto } from '../../core/models';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders: OrderDto[] = [];

  constructor(private orderSvc: OrderService, private authSvc: AuthService, private router: Router) {}

  ngOnInit() {
    this.orderSvc.getUserOrders().subscribe({
      next: data => this.orders = data,
      error: () => {}
    });
  }

  statusLabel(s: number): string {
    return ['Pending', 'Completed', 'Cancelled'][s] || 'Unknown';
  }

  goBack() { this.router.navigate(['/user']); }
}
