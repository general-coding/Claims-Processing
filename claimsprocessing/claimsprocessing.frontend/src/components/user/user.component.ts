import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { User, UserColumns } from '../../models/user';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    RouterModule,
  ],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements AfterViewInit{
  userList: User[] = [];

  // MatTable related data
  dataSource = new MatTableDataSource<User>([]);

  // Do display all name fields and password
  doNotDisplayColumns: string[] = [
    'user_fname',
    'user_mname',
    'user_lname',
    'user_password',
  ];
  // displayedColumns: string[] = UserColumns.map((col) => col.name)
  //   .filter((name) => name !== 'user_password')
  //   .concat('actions');
  displayedColumns: string[] = UserColumns.map((col) => col.name)
    .filter((name) => !this.doNotDisplayColumns.includes(name))
    .concat('actions');

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  getUsers() {
    this.userService.getUsers().subscribe({
      next: (data: User[]) => {
        this.userList = data;
        this.dataSource.data = this.userList;
      },
      error: (error) => {
        this.dataSource = new MatTableDataSource<User>([]);
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
      user_password: 'password',
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
      },
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
