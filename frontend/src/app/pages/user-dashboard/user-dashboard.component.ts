import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ProductService } from '../../core/product.service';
import { CartService } from '../../core/cart.service';
import { AuthService } from '../../core/auth.service';
import { ProductResponseDto } from '../../core/models';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  products: ProductResponseDto[] = [];
  filtered: ProductResponseDto[] = [];
  categories: string[] = ['All'];
  activeCategory = 'All';
  cartCount = 0;
  userName = '';
  loading = true;

  constructor(
    private productSvc: ProductService,
    private cartSvc: CartService,
    private authSvc: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.userName = this.authSvc.getUser()?.name || 'User';
    this.cartSvc.cart$.subscribe(c => this.cartCount = this.cartSvc.getCount());

    this.productSvc.getAll().subscribe({
      next: data => {
        this.products = data;
        this.filtered = data;
        const cats = new Set(data.map(p => p.categoryName).filter(Boolean));
        this.categories = ['All', ...cats];
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  filterBy(cat: string) {
    this.activeCategory = cat;
    this.filtered = cat === 'All' ? this.products : this.products.filter(p => p.categoryName === cat);
  }

  addToCart(p: ProductResponseDto) { this.cartSvc.addToCart(p); }
  goToCart() { this.router.navigate(['/cart']); }
  goToOrders() { this.router.navigate(['/orders']); }
  logout() { this.authSvc.logout(); this.router.navigate(['/login']); }
}
