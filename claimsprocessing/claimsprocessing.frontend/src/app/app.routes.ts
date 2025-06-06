import { Routes } from '@angular/router';
import { LoginComponent } from '../components/login/login.component';
import { UserComponent } from '../components/user/user.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'user', component: UserComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];
