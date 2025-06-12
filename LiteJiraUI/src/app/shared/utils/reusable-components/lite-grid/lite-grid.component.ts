import {
  Component, Input, ViewChild, OnInit, AfterViewInit, OnChanges,
  SimpleChanges, Output, EventEmitter, OnDestroy
} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';
import { CustomizerSettingsService } from '../../../customizer-settings/customizer-settings.service';
import { MatPaginatorModule } from '@angular/material/paginator';
@Component({
  selector: 'lite-grid',
  standalone: true,
  imports: [MaterialModule, CommonModule, MatPaginatorModule],
  templateUrl: './lite-grid.component.html',
  styleUrl: './lite-grid.component.scss'
})
export class LiteGridComponent implements OnInit, AfterViewInit, OnChanges, OnDestroy {
  @Input() title: string = '';
  @Input() columns: any[] = [];
  @Input() data: any[] = [];
  @Input() pageSizeOptions: number[] = [20, 40, 60];
  @Input() config: any = {};
  @Input() rawdata_count: number = 0;

  current_page_index: number = 0;

  @Output() customEvents = new EventEmitter<{ action: string, data: any }>();
  @Output() pageChangeEvents = new EventEmitter<any>();

  displayedColumns: string[] = [];
  globalFilter: string = '';
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  searchFilters: { [key: string]: string } = {};

  // For Pagination
  masters_count: number = 0;        // total records count from server
  pageSize: number = 100;           // default page size
  pageIndex: number = 0;            // default page index

  constructor(public themeService: CustomizerSettingsService) { }

  

  ngOnDestroy(): void {
    // Clean-up logic if needed
  }

  ngOnInit(): void {
    this.title = this.config?.title;
    this.displayedColumns = [...this.columns
      .filter(c => c.IsVisible)
      .sort((a, b) => a.ColumnOrder - b.ColumnOrder)
      .map(c => c.ColumnName), 'actions'];
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
    // If you need custom sorting
    this.dataSource.sortingDataAccessor = (item, property) => {
      // Custom sorting logic if needed
      return item[property];
    };
  }

  /** ✅ Handle data changes dynamically */
  ngOnChanges(changes: SimpleChanges) {
    console.log("sort", this.sort);
    console.log("changes",this.dataSource.paginator);
    this.ngOnInit();
    if (changes['data'] && changes['data'].currentValue) {
      this.dataSource.data = this.data;

    }
    
  }

  /** 🔍 Apply column-wise or global filter */
  applyFilter(column: string, event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();

    if (column === 'All') {
      this.globalFilter = filterValue; // Store global filter separately
    } else {
      this.searchFilters[column] = filterValue;
    }

    this.dataSource.filterPredicate = (data, filter) => {
      const filters = JSON.parse(filter);
      const globalSearch = this.globalFilter?.toLowerCase() || '';

      return this.columns
        .filter(c => c.IsVisible) // 🔍 Only filter visible columns
        .every(c => {
          const colValue = data[c.ColumnName]?.toString().toLowerCase() || '';

          if (globalSearch) {
            // 🌍 Apply global search: Match at least one visible column
            return this.columns.some(c => {
              const colText = data[c.ColumnName]?.toString().toLowerCase() || '';
              return colText.includes(globalSearch);
            });
          } else {
            // 🔍 Apply column-wise filtering
            return filters[c.ColumnName] ? colValue.includes(filters[c.ColumnName]) : true;
          }
        });
    };

    this.dataSource.filter = JSON.stringify(this.searchFilters);
    console.log("this.dataSource.paginator", this.dataSource.paginator);
    // Reset paginator to first page after filtering
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  /** ✏️ Handle row actions */
  onAction(action: string, row: any) {
    this.customEvents.emit({ action, data: row });
  }

  pageChangeHandler(event: any): void {
    this.pageChangeEvents.emit(event);
    
  }

  
  
}
