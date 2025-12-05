import { NgIf } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../Services/auth-service';
import { Subject, takeUntil } from 'rxjs';
import { UsersService } from '../../../Services/users-service';
import { SimpleDynamicTableComponet } from "../../Shared/simple-dynamic-table-componet/simple-dynamic-table-componet";

export interface Users {
  userID: number;
  name: string;
}
export interface CreateUsers{
  name: string;
  password: string;
  email: string;
  userTypeID: number;
  organisationId: number;
}


@Component({
  selector: 'app-users-userscomponent',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf, SimpleDynamicTableComponet],
  templateUrl: './users-userscomponent.html',
  styleUrl: './users-userscomponent.css',
})
export class UsersUserscomponent implements OnInit, OnDestroy {

  // displayedColumns sets up how many collumns there is needed and what they should contatin
  displayedColumns: string[] = ['name'];
  users: Users[] = [];


  createAdminForm!: FormGroup;
  // isLoggedIn simply tells the system that they are log in or not
  isLoggedIn: boolean = false;

  private destroy$ = new Subject<void>();

  user = {
    name: 'not_working',
    userID: -1
  };


  // the constructor createing AuthService and AdminApiCallServices for use 
  constructor(
    private authService: AuthService,
    private userService: UsersService
  ) { }

  // CreateNewUser creates a user by caling the CreateNewUser function on adminService that makes a API call
  CreateNewUser() {
    const token = localStorage.getItem('JWT_Token');
    var decoded;
    if(token != null){
      decoded = this.decodeToken(token)
    }

    const CreateUsers: CreateUsers = {
      name: this.createAdminForm.get('name')?.value,
      password: this.createAdminForm.get('password')?.value,
      email: this.createAdminForm.get('email')?.value,
      userTypeID: decoded.userTypeID,
      organisationId: decoded.organisationId
    };
    
    // for API it gets the values from createAdminForm
    (this.authService.createUser(CreateUsers)).subscribe(() => {
      this.createAdminForm.reset();
    });

    this.tableRefresh();
  }
  // ngOnInit does things on init
  ngOnInit(): void {
    // calls the createFrom funtion
    this.createForm();

    // here we subscribe this.isLoggedIn with the status from the authService.loggedIn to always know the status of of login acros components
    this.authService.loggedIn
      .pipe(takeUntil(this.destroy$))
      .subscribe(status => {
        this.isLoggedIn = status;
      });

    this.isLoggedIn = !!this.authService.getToken();

    if (this.isLoggedIn) {
      this.tableRefresh();
      this.CurrentUserupdate();
    }
  }

  // this should Refresh table that shows all of the admins 
  tableRefresh() {
    this.userService.getUsersbyOrganisation().subscribe({
      next: (res) => {
        this.users = res;
      },
      error: (err) => {
        console.error('Error fetching profiles:', err);
      },
    });
  }

  // this changes the this.user to show the actucal information from the admin
  CurrentUserupdate() {
    this.userService.getMeProfile().subscribe({
      next: (res) => {
        this.user = res;
      },
      error: (err) => {
        console.error('Error fetching meprofile:', err);
      },
    });
  }

  // creates the froms and there requriments
  private createForm() {
    this.createAdminForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      password: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required]),
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  decodeToken(token: string) {
    const payload = token.split('.')[1];
    const decodedPayload = atob(payload);
    return JSON.parse(decodedPayload);
  }

}