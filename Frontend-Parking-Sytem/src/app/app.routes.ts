import { Routes } from '@angular/router';
import { ParkingStructurcomponent } from './Tools/parking-structurcomponent/parking-structurcomponent';
import { Lotcomponent } from './Tools/lotcomponent/lotcomponent';
import { LotHistorycomponent } from './Tools/lot-historycomponent/lot-historycomponent';
import { UserLogincomponent } from './Tools/user-logincomponent/user-logincomponent';
import { UsersUserscomponent } from './Tools/users-userscomponent/users-userscomponent';
import { CarIncomponent } from './CarIn_CarsOut/car-incomponent/car-incomponent';
import { CarOutcomponent } from './CarIn_CarsOut/car-outcomponent/car-outcomponent';
import { ParkingListComponent } from './parking-list-component/parking-list-component';

export const routes: Routes = [
    {path: "parkingstructur", component: ParkingStructurcomponent},
    {path: "lotshsitory", component: LotHistorycomponent},
    {path: "userlogin", component: UserLogincomponent},
    {path: "users", component: UsersUserscomponent},

    { path: 'carin', component: ParkingListComponent },

    {path: "form/:id", component: CarIncomponent},
    {path: "carout", component: CarOutcomponent},

    {
        path: 'lots/:id',
        loadComponent: () =>
        import('./Tools/lotcomponent/lotcomponent')
            .then(c => c.Lotcomponent)
    },


    {path: '**', redirectTo: 'parkingstructur'},

];
