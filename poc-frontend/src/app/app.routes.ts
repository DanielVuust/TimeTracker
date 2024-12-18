import { Routes } from '@angular/router';
import { HomeComponent } from './screens/home/home.component';
import { ListComponent } from './screens/list/list.component';

export const routes: Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full'},
    {path: 'home', component: HomeComponent},
    {path: 'list', component: ListComponent},
];
