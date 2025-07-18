import { Component } from '@angular/core';
import { User, UserService } from '../../../../api/user';
import { RouterModule } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Modal } from '../../components/modal/modal';

@Component({
  selector: 'app-list-users',
  imports: [RouterModule, MatDialogModule],
  templateUrl: './list-users.html',
  styleUrl: './list-users.scss',
})
export class ListUsers {
  allUser: User[] = [];
  selectedUserId: number = -1;

  constructor(private userService: UserService, public dialog: MatDialog) {}

  ngOnInit() {
    this.loadData();
  }

  confirmDelete(id: number | undefined): void {
    if (id === undefined) return;

    this.selectedUserId = id;
    const dialogRef = this.dialog.open(Modal, {
      width: '250px',
      data: {
        title: 'Confirm delete user',
        message:
          'Are you sure you want to delte user with id: ' + this.selectedUserId,
      },
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result === true) {
        this.userService.apiUserIdDelete(this.selectedUserId).subscribe(() => {
          this.loadData();
        });
      } else {
        console.log('Cancelled');
      }
    });
  }

  loadData() {
    this.userService.apiUserGet().subscribe((result) => {
      this.allUser = result;
    });
  }
}
