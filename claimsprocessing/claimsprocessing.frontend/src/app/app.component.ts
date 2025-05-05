import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserComponent } from '../components/user/user.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [UserComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'claimsprocessing.frontend';
}
