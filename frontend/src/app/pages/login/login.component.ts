import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';
  name = '';
  isRegister = false;
  loading = false;
  error = '';

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    this.loading = true;
    this.error = '';

    if (this.isRegister) {
      this.auth.register({ name: this.name, email: this.email, password: this.password }).subscribe({
        next: res => {
          if (res.isSuccess) {
            this.auth.saveSession(res);
            this.navigateByRole(res.role);
          } else {
            this.error = res.message || 'Registration failed.';
          }
          this.loading = false;
        },
        error: err => { this.error = err.error?.message || 'Server error.'; this.loading = false; }
      });
    } else {
      this.auth.login({ email: this.email, password: this.password }).subscribe({
        next: res => {
          if (res.isSuccess) {
            this.auth.saveSession(res);
            this.navigateByRole(res.role);
          } else {
            this.error = res.message || 'Invalid credentials.';
          }
          this.loading = false;
        },
        error: err => { this.error = err.error?.message || 'Server error.'; this.loading = false; }
      });
    }
  }

  private navigateByRole(role: string) {
    this.router.navigate([role === 'Admin' ? '/admin' : '/user']);
  }
}
