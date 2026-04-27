import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CartService } from '../../core/cart.service';
import { OrderService } from '../../core/order.service';
import { PlaceOrderDto } from '../../core/models';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent {
  form: PlaceOrderDto = { customerName: '', customerPhone: '', customerAddress: '', notes: '' };
  loading = false;
  error = '';
  done = false;

  constructor(private orderSvc: OrderService, private cartSvc: CartService, private router: Router) {}

  confirm() {
    this.loading = true;
    this.error = '';
    this.orderSvc.placeOrder(this.form).subscribe({
      next: () => {
        this.cartSvc.clearCart();
        this.done = true;
        this.loading = false;
      },
      error: err => {
        this.error = err.error?.message || 'Failed to place order. Make sure the backend is running.';
        this.loading = false;
      }
    });
  }

  goBack() { this.router.navigate(['/cart']); }
  goDashboard() { this.router.navigate(['/user']); }
}
