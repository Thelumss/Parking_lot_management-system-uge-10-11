import { Routes } from '@angular/router';
import { ParkingStructurcomponent } from './Tools/parking-structurcomponent/parking-structurcomponent';
import { Lotcomponent } from './Tools/lotcomponent/lotcomponent';
import { LotHistorycomponent } from './Tools/lot-historycomponent/lot-historycomponent';
import { UserLogincomponent } from './Tools/user-logincomponent/user-logincomponent';
import { UsersUserscomponent } from './Tools/users-userscomponent/users-userscomponent';

export const routes: Routes = [
    {path: "ParkingStructur", component: ParkingStructurcomponent},
    {path: "Lots", component: Lotcomponent},
    {path: "LotsHsitory", component: LotHistorycomponent},
    {path: "UserLogin", component: UserLogincomponent},
    {path: "Users", component: UsersUserscomponent},

];
