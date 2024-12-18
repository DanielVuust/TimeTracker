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

  makeApiRequest(inputValue: string) {
    console.log('Input value:', inputValue);
    const apiUrl = 'https://api.example.com/endpoint';
    this.http.post(apiUrl, { data: inputValue }).subscribe(response => {
      console.log('API response:', response);
    });
  }
}
