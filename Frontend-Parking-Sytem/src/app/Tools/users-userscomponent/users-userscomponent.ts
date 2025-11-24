import { NgIf } from '@angular/common';
import { Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth-service';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Subject, takeUntil } from 'rxjs';
import { UsersService } from '../../Services/users-service';

export interface admins {
  id: number;
  name: string;
}


@Component({
  selector: 'app-users-userscomponent',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf,MatTableModule, MatPaginatorModule,MatSortModule],
  templateUrl: './users-userscomponent.html',
  styleUrl: './users-userscomponent.css',
})
export class UsersUserscomponent implements OnInit, OnDestroy  {

  // displayedColumns sets up how many collumns there is needed and what they should contatin
  displayedColumns: string[] = ['id', 'name'];
  // dataSource is what holds the data that displayedColumns shows
  dataSource = new MatTableDataSource<admins>([]);
  // admins holds the admins for when they should go over to dataSource
  users: admins[] = [];

  // loginfrom and createadminfrom are both froms the setup of the froms and and how to get the information out of them
  loginForm!: FormGroup;
  createAdminForm!: FormGroup;
  // isLoggedIn simply tells the system that they are log in or not
  isLoggedIn: boolean = false;

  private destroy$ = new Subject<void>();
  private _liveAnnouncer = inject(LiveAnnouncer);
  // a temp holder for the curret admin
  user={
    name: 'not_working',
    id: -1
  };
  
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  
  // the constructor createing AuthService and AdminApiCallServices for use 
  constructor(
    private authService: AuthService,
    private userService: UsersService
  ) {}

  // CreateNewUser creates a user by caling the CreateNewUser function on adminService that makes a API call
  CreateNewUser() {
    // for API it gets the values from createAdminForm
    (this.authService.createUser(this.createAdminForm.value)).subscribe(()=>{
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

    if (this.isLoggedIn){
      this.tableRefresh();
      this.CurrentUserupdate();
    }
  }
  
  // this should Refresh table that shows all of the admins 
  tableRefresh(){
      this.userService.getUsersbyOrganisation().subscribe({
        next: (res) => {
          this.users = res;
          this.dataSource.data = this.users;
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        },
        error: (err) => {
          console.error('Error fetching profiles:', err); 
        },
      });
  }

  // this changes the this.user to show the actucal information from the admin
  CurrentUserupdate(){
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
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', Validators.required),
    });
  }

  
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  
}