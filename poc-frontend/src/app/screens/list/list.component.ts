import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
@Component({
  selector: 'app-list',
  imports: [CommonModule, MatTableModule, MatFormFieldModule, MatInputModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent {
  dataSource: MatTableDataSource<any> = new MatTableDataSource<any>([]);
  displayedColumns: string[] = ['Timestamp', 'Status'];
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:58405/api/arduino/bb35900c-0230-4929-a484-5113a126b214/logs')
      .subscribe(response => {
        console.log('API response:', response);
        this.dataSource = new MatTableDataSource<any>(response as any[]);
      });
  }
  formatDate(timestamp: string): string {
    return new Date(timestamp ).toLocaleString();
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
