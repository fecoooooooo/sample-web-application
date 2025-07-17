import { Component } from '@angular/core';
import { User, UserService } from '../../../../api/user';

@Component({
  selector: 'app-list-users',
  imports: [],
  templateUrl: './list-users.html',
  styleUrl: './list-users.scss',
})
export class ListUsers {
  allUser: User[] = [];

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.userService.apiUserGet().subscribe((result) => {
      this.allUser = result;
    });
  }
}
