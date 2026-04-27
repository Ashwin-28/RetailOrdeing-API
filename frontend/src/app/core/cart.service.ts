import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItemLocal, ProductResponseDto } from './models';

@Injectable({ providedIn: 'root' })
export class CartService {
  private storageKey = 'cart_items';
  cart$ = new BehaviorSubject<CartItemLocal[]>(this.loadCart());

  private loadCart(): CartItemLocal[] {
    const raw = localStorage.getItem(this.storageKey);
    return raw ? JSON.parse(raw) : [];
  }

  private saveCart(items: CartItemLocal[]) {
    localStorage.setItem(this.storageKey, JSON.stringify(items));
    this.cart$.next(items);
  }

  addToCart(product: ProductResponseDto, quantity: number = 1) {
    const items = this.cart$.value;
    const existing = items.find(i => i.product.id === product.id);
    if (existing) {
      existing.quantity += quantity;
    } else {
      items.push({ product, quantity });
    }
    this.saveCart([...items]);
  }

  updateQuantity(productId: number, quantity: number) {
    let items = this.cart$.value;
    if (quantity <= 0) {
      items = items.filter(i => i.product.id !== productId);
    } else {
      const item = items.find(i => i.product.id === productId);
      if (item) item.quantity = quantity;
    }
    this.saveCart([...items]);
  }

  removeFromCart(productId: number) {
    const items = this.cart$.value.filter(i => i.product.id !== productId);
    this.saveCart(items);
  }

  getTotal(): number {
    return this.cart$.value.reduce((sum, i) => sum + i.product.price * i.quantity, 0);
  }

  getCount(): number {
    return this.cart$.value.reduce((sum, i) => sum + i.quantity, 0);
  }

  clearCart() {
    localStorage.removeItem(this.storageKey);
    this.cart$.next([]);
  }
}
