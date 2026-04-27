import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CartService } from '../../core/cart.service';
import { CartItemLocal } from '../../core/models';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  items: CartItemLocal[] = [];
  subtotal = 0;
  tax = 0;
  total = 0;

  constructor(private cartSvc: CartService, private router: Router) {}

  ngOnInit() {
    this.cartSvc.cart$.subscribe(items => {
      this.items = items;
      this.subtotal = this.cartSvc.getTotal();
      this.tax = this.subtotal * 0.05;
      this.total = this.subtotal + this.tax;
    });
  }

  changeQty(c: CartItemLocal, delta: number) {
    this.cartSvc.updateQuantity(c.product.id, c.quantity + delta);
  }

  remove(c: CartItemLocal) {
    this.cartSvc.removeFromCart(c.product.id);
  }

  goBack() { this.router.navigate(['/user']); }
  checkout() { this.router.navigate(['/checkout']); }
}
