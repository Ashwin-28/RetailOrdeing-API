import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: `
    <div class="bg-glow"></div>
    <div class="bg-glow right"></div>
    <router-outlet />
  `,
  styles: []
})
export class AppComponent {
  title = 'Retail Ordering';
}
