import { Component } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';

import { MatInputModule } from '@angular/material/input';

import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
@Component({
  selector: 'app-home',
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  constructor(private http: HttpClient) {}
  actions: any[] = [];
  makeApiRequest() {
    const apiUrl = 'https://localhost:58405/api/arduino/bb35900c-0230-4929-a484-5113a126b214/log';
    this.http.post(apiUrl, this.actions).subscribe(response => {
      console.log('API response:', response);
    });
    this.actions = [];
  }

  addNewAction(action: string) {
    this.actions.push({
      status: action,
      timestamp: new Date(),
    });
  }
  convertActionToText() {
    return this.actions.map(action => {
      return `${action.status} at ${action.timestamp}\n`;
    });
  }

}
