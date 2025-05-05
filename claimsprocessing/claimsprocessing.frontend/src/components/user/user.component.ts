import { Component } from '@angular/core';
import { User, UserColumns } from '../../models/user';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
  ],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent {
  userList: User[] = [];

  // MatTable related properties
  dataSource = new MatTableDataSource<User>([]);
  displayedColumns: string[] = UserColumns.map((col) => col.name).concat('actions');

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe({
      next: (data: User[]) => {
        this.userList = data;
        this.dataSource.data = this.userList;
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      },
      complete: () => {
        console.log('User fetching completed');
      },
    });
  }

  createUser() {
    const newUser: User = {
      user_id: this.userList.length + 1,
      user_fname: 'a',
      user_mname: 'b',
      user_lname: 'c',
      user_fullname: 'a b c',
      user_email: 'def@ghi.jkl',
      user_password:
        '6badc0765cffa15ed4ce7436f990f13d8ff81a8f60114b3297c2305a54dd15a8',
      created_on: new Date(Date.now()),
    };

    this.userService.createUser(newUser).subscribe({
      next: (user: User) => {
        console.log('User created:', user);
        this.userList.push(newUser);
        this.dataSource.data = this.userList;
      },
      error: (error) => {
        console.error('Error creating user:', error);
      },
      complete: () => {
        console.log('User creation completed');
      },
    });
  }

  deleteUserById(user: User) {
    console.log('Deleting user:', user);

    this.userService.deleteUserById(user.user_id).subscribe({
      next: () => {
        console.log('User deleted:', user.user_id);
        this.getUsers();
      },
      error: (error) => {
        console.error('Error deleting user:', error);
      },
      complete: () => {
        console.log('User deletion completed');
      }
    });
  }

  updateUserById(user: User) {
    console.log('Editing user:', user);
    const index = this.userList.findIndex((u) => u.user_id === user.user_id);
    if (index !== -1) {
      this.userList.splice(index, 1, user);
      this.dataSource.data = this.userList;
    }
  }
}
