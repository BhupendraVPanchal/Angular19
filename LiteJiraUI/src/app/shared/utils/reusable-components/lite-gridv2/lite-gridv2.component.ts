import { Component, Input, ViewChild, OnInit, AfterViewInit, OnChanges, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lite-gridv2',
  imports: [],
  templateUrl: './lite-gridv2.component.html',
  styleUrl: './lite-gridv2.component.scss'
})
export class LiteGridv2Component implements OnInit, AfterViewInit, OnChanges {
  @Input() columns: any[] = [];
  @Input() data: any[] = [];
  @Input() pageSizeOptions: number[] = [20, 40, 60];

  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  searchFilters: { [key: string]: string } = {};

  ngOnInit(): void {
    this.displayedColumns = [...this.columns.map(c => c.key), 'actions'];
    this.dataSource = new MatTableDataSource(this.data);

    // Set custom filter predicate
    this.dataSource.filterPredicate = (data: any, filter: string): boolean => {
      const filters = JSON.parse(filter);
      return Object.keys(filters).every(column =>
        data[column]?.toString().toLowerCase().includes(filters[column])
      );
    };
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  /** ✅ Handle data changes dynamically */
  ngOnChanges(changes: SimpleChanges) {
    if (changes['data'] && changes['data'].currentValue) {
      this.dataSource.data = this.data;
    }
  }

  /** 🔍 Apply column-wise filter */
  applyFilter(column: string, event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.searchFilters[column] = filterValue;
    this.dataSource.filter = JSON.stringify(this.searchFilters);
  }

  /** ✏️ Handle row actions */
  onAction(action: string, row: any) {
    console.log(action, row);
  }
}
