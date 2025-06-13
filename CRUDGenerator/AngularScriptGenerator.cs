using System.Data;
using System.Text;

namespace OTAGenerator
{
    public class AngularScriptGenerator
    {

        public AngularScriptGenerator() { }



        public void generate_component_listing_html()
        {
            try
            {
                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.AppendLine("<div id = \"qm_container\" tabindex = \"1\" (keydown) = \"handleKeyPlus($event)\" style = \"border:none;outline:none\" >");
                htmlBuilder.AppendLine("<div class=\" page-header\" >");
                htmlBuilder.AppendLine("<div class=\"row align-items-center\">");
                htmlBuilder.AppendLine("<div class=\"col\">");
                htmlBuilder.AppendLine("<h2>Common Masters List</h2>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<div class=\"d-flex align-items-center gap-2 col-12 col-md-6 col-lg-5 col-xl-5 col-xxl-4\">");
                htmlBuilder.AppendLine("<div class=\"w-50 \">");
                htmlBuilder.AppendLine("<mat-form-field appearance = \"outline\" class=\"w-100\">");
                htmlBuilder.AppendLine("<mat-select[(value)]=\"query.search_column\">");
                htmlBuilder.AppendLine("<mat-option* ngFor = \"let opt of search_options\"[value] = \"opt.name\" >");
                htmlBuilder.AppendLine("{{opt.name}}");
                htmlBuilder.AppendLine("</ mat - option >");
                htmlBuilder.AppendLine("</ mat - select >");
                htmlBuilder.AppendLine("</ mat - form - field >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("<div class= \"input-group search-box shadow-sm w-50\" >");
                htmlBuilder.AppendLine("<input type = \"text\" class= \"form-control\" #search_field id=\"search_field\" placeholder=\"Press Enter to search keyword\" [(ngModel)]=\"searched_text\" (keyup)=\"searchChangeHandler()\" />");
                htmlBuilder.AppendLine("<button class= \"btn\" id = \"btnsearchsubmit\"(click) = \"searchChangeHandler()\" ><i class= \"bi bi-search\" ></ i ></ button >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("<div class= \"body-container\" >");
                htmlBuilder.AppendLine("<div class= \"data-table child-table-row\" * ngIf = \"commonmastertypes_list.length > 0\" >");
                htmlBuilder.AppendLine("<div class= \"table-responsive table_fullHeight\" >");
                htmlBuilder.AppendLine("<table class= \"table table-bordered mb-0\" >");
                htmlBuilder.AppendLine("<thead class= \"sticky-top\" >");
                htmlBuilder.AppendLine("<tr >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MasterName')\"[ngClass] = \"{ 'sorting': isSorting('MasterName'), 'sorting_asc': isSortAsc('MasterName'), 'sorting_desc': isSortDesc('MasterName') }\" ><div class= \"d-flex align-items-center justify-content-between\" ><span > Master Name </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MasterTitle')\"[ngClass] = \"{ 'sorting': isSorting('MasterTitle'), 'sorting_asc': isSortAsc('MasterTitle'), 'sorting_desc': isSortDesc('MasterTitle') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Master Title </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MasterTypeDescription')\"[ngClass] = \"{ 'sorting': isSorting('MasterTypeDescription'), 'sorting_asc': isSortAsc('MasterTypeDescription'), 'sorting_desc': isSortDesc('MasterTypeDescription') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Master Description </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('Locked')\"[ngClass] = \"{ 'sorting': isSorting('Locked'), 'sorting_asc': isSortAsc('Locked'), 'sorting_desc': isSortDesc('Locked') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Locked </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('Heirarchical')\"[ngClass] = \"{ 'sorting': isSorting('Heirarchical'), 'sorting_asc': isSortAsc('Heirarchical'), 'sorting_desc': isSortDesc('Heirarchical') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Has Parent </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('ParentName')\"[ngClass] = \"{ 'sorting': isSorting('ParentName'), 'sorting_asc': isSortAsc('ParentName'), 'sorting_desc': isSortDesc('ParentName') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Parent Name </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('NeedOrdering')\"[ngClass] = \"{ 'sorting': isSorting('NeedOrdering'), 'sorting_asc': isSortAsc('NeedOrdering'), 'sorting_desc': isSortDesc('NeedOrdering') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Need Sequencing </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MenuName')\"[ngClass] = \"{ 'sorting': isSorting('MenuName'), 'sorting_asc': isSortAsc('MenuName'), 'sorting_desc': isSortDesc('MenuName') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Menu Name </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('BeforeSaveSp')\"[ngClass] = \"{ 'sorting': isSorting('BeforeSaveSp'), 'sorting_asc': isSortAsc('BeforeSaveSp'), 'sorting_desc': isSortDesc('BeforeSaveSp') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Before Save Sp</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('AfterSaveSp')\"[ngClass] = \"{ 'sorting': isSorting('AfterSaveSp'), 'sorting_asc': isSortAsc('AfterSaveSp'), 'sorting_desc': isSortDesc('AfterSaveSp') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > After Save Sp</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('CreatedBy')\"[ngClass] = \"{ 'sorting': isSorting('CreatedBy'), 'sorting_asc': isSortAsc('CreatedBy'), 'sorting_desc': isSortDesc('CreatedBy') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Created By </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('CreatedDate')\"[ngClass] = \"{ 'sorting': isSorting('CreatedDate'), 'sorting_asc': isSortAsc('CreatedDate'), 'sorting_desc': isSortDesc('CreatedDate') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Created On </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('UpdatedBy')\"[ngClass] = \"{ 'sorting': isSorting('UpdatedBy'), 'sorting_asc': isSortAsc('UpdatedBy'), 'sorting_desc': isSortDesc('UpdatedBy') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Updated By </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('UpdatedDate')\"[ngClass] = \"{ 'sorting': isSorting('UpdatedDate'), 'sorting_asc': isSortAsc('UpdatedDate'), 'sorting_desc': isSortDesc('UpdatedDate') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Updated On </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('RecordCount')\"[ngClass] = \"{ 'sorting': isSorting('RecordCount'), 'sorting_asc': isSortAsc('RecordCount'), 'sorting_desc': isSortDesc('RecordCount') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Record Count </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MailToEmployees')\"[ngClass] = \"{ 'sorting': isSorting('MailToEmployees'), 'sorting_asc': isSortAsc('MailToEmployees'), 'sorting_desc': isSortDesc('MailToEmployees') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Mail To Employees</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MailToOthers')\"[ngClass] = \"{ 'sorting': isSorting('MailToOthers'), 'sorting_asc': isSortAsc('MailToOthers'), 'sorting_desc': isSortDesc('MailToOthers') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Mail To Others</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th class= \"action-btn\" > Action </ th >");
                htmlBuilder.AppendLine("</ tr >");
                htmlBuilder.AppendLine("</ thead >");
                htmlBuilder.AppendLine("<tbody * ngFor = \"let item of commonmastertypes_list\" >");
                htmlBuilder.AppendLine("<tr style = \"cursor:pointer\"(click) = \"show_custom_fields_of_selected_masters(item)\"(dblclick) = \"edit_common_master(item.code)\" >");
                htmlBuilder.AppendLine("<td><p >{ { item.MasterName} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MasterTitle} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MasterTypeDescription} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.Locked} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.Heirarchical} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.ParentName} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.NeedOrdering} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MenuName} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.BeforeSaveSp} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.AfterSaveSp} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.CreatedBy} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.CreatedDate | date:'dd/MM/yyyy'} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.UpdatedBy} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.UpdatedDate | date:'dd/MM/yyyy'} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.RecordCount } }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MailToEmployees } }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MailToOthers } }</ p ></ td >");
                htmlBuilder.AppendLine("<td class= \"action-btn\" >");
                htmlBuilder.AppendLine("<button type = \"button\"(click) = \"edit_common_master(item.code)\" class= \"btn edit\" ><i class= \"bi bi-pencil-square\" ></ i ></ button >");
                htmlBuilder.AppendLine("<button type = \"button\"(click) = \"delete_common_master(item.code)\" class= \"btn delete\" ><i class= \"bi bi-trash\" ></ i ></ button >");
                htmlBuilder.AppendLine("</ td >");
                htmlBuilder.AppendLine("</ tr >");

                htmlBuilder.AppendLine("</ tbody>");
                htmlBuilder.AppendLine("</table>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<div class= \"table-footer page-content-head d-flex align-items-center justify-content-end mt-2\" >");
                htmlBuilder.AppendLine("<mat - paginator #master_paginator");


                htmlBuilder.AppendLine("[length] = \"masters_count\"");
                htmlBuilder.AppendLine("[pageSize] = \"1000\"");
                htmlBuilder.AppendLine("[pageIndex] = \"0\"");
                htmlBuilder.AppendLine("[pageSizeOptions] = \"[100, 300, 600, 1000]\"");
                htmlBuilder.AppendLine("aria - label = \"Select page\"(page) = \"pageChangeHandler($event)\" >");
                htmlBuilder.AppendLine("</ mat - paginator >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("<a class= \"btn add-btn shadow\"(click) = \"add_button_click()\" cdkDragBoundary = \".page-body\" cdkDrag ><i class= \"bi bi-plus\" ></ i ></ a >");
                htmlBuilder.AppendLine("</ div >");




            }
            catch (Exception)
            {

                throw;
            }

        }

        public void generate_component_listing_ts()
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { AfterViewInit, Component, ElementRef, HostListener, OnDestroy, OnInit, ViewChild }  from '@angular/core';");
            tsBuilder.AppendLine("import { QuickMasterService } from './quick-master.service';");
            tsBuilder.AppendLine("import { NavigationStart, Router } from '@angular/router';");
            tsBuilder.AppendLine("import { MatDialog } from '@angular/material/dialog';");
            tsBuilder.AppendLine("import { QuickMasterEntryComponent } from '../quick-master-entry/quick-master-entry.component';");
            tsBuilder.AppendLine("import { ToastrService } from 'ngx-toastr';");
            tsBuilder.AppendLine("import { MatPaginator } from '@angular/material/paginator';");
            tsBuilder.AppendLine("import { DynamicAlertService } from '../../../shared/components/dynamic-alert/dynamic-alert.service';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("@Component({");
            tsBuilder.AppendLine("  selector: 'app-quickmaster',");
            tsBuilder.AppendLine("  templateUrl: './quickmaster.component.html',");
            tsBuilder.AppendLine("  styleUrls: ['./quickmaster.component.scss']");
            tsBuilder.AppendLine("})");
            tsBuilder.AppendLine("export class QuickmasterComponent implements OnInit, AfterViewInit {");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public commonmastertypes_list: any = [];");
            tsBuilder.AppendLine("  public custom_fields_of_commonmaster_list: any = [];");
            tsBuilder.AppendLine("  public custom_field_parent_code: any = '';");
            tsBuilder.AppendLine("  public masters_count: number;");
            tsBuilder.AppendLine("  public query: any;");
            tsBuilder.AppendLine("  public selected_master: string;");
            tsBuilder.AppendLine("  public searched_text: string = '';");
            tsBuilder.AppendLine("  public search_options: any[] = [];");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  @ViewChild('search_field') myInputField: ElementRef;");
            tsBuilder.AppendLine("  @ViewChild('master_paginator') paginator: MatPaginator;");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  constructor(private _quickmasterService: QuickMasterService, private router: Router,");
            tsBuilder.AppendLine("    private dialog: MatDialog, private toastr: ToastrService,");
            tsBuilder.AppendLine("    private m2_alert: DynamicAlertService, private elementRef: ElementRef) {");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  ngOnInit(): void {");
            tsBuilder.AppendLine("    this.oninit();");
            tsBuilder.AppendLine("    this.get_search_options();");
            tsBuilder.AppendLine("    this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    let ele : HTMLElement = document.getElementById('qm_container');");
            tsBuilder.AppendLine("    ele.focus();");
            tsBuilder.AppendLine("    /*console.log((ele[0] as HTMLElement).focus())*/");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  ngAfterViewInit(): void {");
            tsBuilder.AppendLine("    setTimeout(() => {");
            tsBuilder.AppendLine("      var elem = document.getElementById('search_field');");
            tsBuilder.AppendLine("      elem.focus();");
            tsBuilder.AppendLine("    }, 300);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  oninit(): void {");
            tsBuilder.AppendLine("    this.query = {");
            tsBuilder.AppendLine("      page_index: 0,");
            tsBuilder.AppendLine("      page_size: 1000,");
            tsBuilder.AppendLine("      search_text: '',");
            tsBuilder.AppendLine("      search_column: 'MasterName',");
            tsBuilder.AppendLine("      sort_col_name: 'MasterName',");
            tsBuilder.AppendLine("      sort_type: 'desc'");
            tsBuilder.AppendLine("    };");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  get_search_options(): void {");
            tsBuilder.AppendLine("    this._quickmasterService.get_common_master_type_info()");
            tsBuilder.AppendLine("      .subscribe(data => {");
            tsBuilder.AppendLine("        this.search_options = data;");
            tsBuilder.AppendLine("      })");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  get_allcommonmastertypes_list(): void {");
            tsBuilder.AppendLine("    this._quickmasterService.get_commonmastertypes_list(this.query)");
            tsBuilder.AppendLine("      .subscribe(data => {");
            tsBuilder.AppendLine("        this.commonmastertypes_list = data.data.CommonMasterTypes;");
            tsBuilder.AppendLine("        this.masters_count = data.MasterCount[0].totalRowsCount;");
            tsBuilder.AppendLine("      })");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  show_custom_fields_of_selected_masters(item: any): void {");
            tsBuilder.AppendLine("    this.commonmastertypes_list.forEach((obj: any) => {");
            tsBuilder.AppendLine("      if (obj.code != item.code) {");
            tsBuilder.AppendLine("        obj.ShowFieldConfig = false;");
            tsBuilder.AppendLine("      }");
            tsBuilder.AppendLine("    });");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("    if (item.ShowFieldConfig) {");
            tsBuilder.AppendLine("      item.ShowFieldConfig = false;");
            tsBuilder.AppendLine("    } else {");
            tsBuilder.AppendLine("      item.ShowFieldConfig = true;");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("    //if (this.custom_field_parent_code == _code) {");
            tsBuilder.AppendLine("    //  this.custom_field_parent_code = '';");
            tsBuilder.AppendLine("    //} else {");
            tsBuilder.AppendLine("    //  this._quickmasterService.get_cmnMst_CustomFieldConfig_list({ CmnMst_Code: _code })");
            tsBuilder.AppendLine("    //    .subscribe(data => {");
            tsBuilder.AppendLine("    //      this.selected_master = name;");
            tsBuilder.AppendLine("    //      this.custom_fields_of_commonmaster_list = data;");
            tsBuilder.AppendLine("    //      this.custom_field_parent_code = _code;");
            tsBuilder.AppendLine("    //    })");
            tsBuilder.AppendLine("    //}");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  add_button_click(): void {");
            tsBuilder.AppendLine("    this.open_entry_page(null);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  edit_common_master(code: number): void {");
            tsBuilder.AppendLine("    this.open_entry_page(code);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  delete_common_master(code: number): void {");
            tsBuilder.AppendLine("    this.m2_alert.fire({");
            tsBuilder.AppendLine("      title: 'Are you sure you want to delete this common master ?',");
            tsBuilder.AppendLine("      text: 'The data present for this master will also be deleted.',");
            tsBuilder.AppendLine("      icon: 'warning',");
            tsBuilder.AppendLine("      showCancelButton: true,");
            tsBuilder.AppendLine("      confirmButtonText: 'yes',");
            tsBuilder.AppendLine("      cancelButtonText: 'no'");
            tsBuilder.AppendLine("    }).then((r) => {");
            tsBuilder.AppendLine("      if (r.isConfirmed) {");
            tsBuilder.AppendLine("        this._quickmasterService.delete_common_master_type({ code: code }).subscribe({");
            tsBuilder.AppendLine("          next: (data) => {");
            tsBuilder.AppendLine("            this.toastr.success(data[0]['message']);");
            tsBuilder.AppendLine("            this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("            this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("          },");
            tsBuilder.AppendLine("          error: (err_response) => {");
            tsBuilder.AppendLine("            this.toastr.error(err_response['error']['message']);");
            tsBuilder.AppendLine("          }");
            tsBuilder.AppendLine("        })");
            tsBuilder.AppendLine("      }");
            tsBuilder.AppendLine("    })");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  open_entry_page(code: number): void {");
            tsBuilder.AppendLine("    let dialogRef = this.dialog.open(QuickMasterEntryComponent, { data: { code: code }, disableClose: true, panelClass: 'full-width' });");
            tsBuilder.AppendLine("    dialogRef.afterClosed().subscribe(() => {");
            tsBuilder.AppendLine("      this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("      this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("    });");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  handleKeyPlus(event: KeyboardEvent) {");
            tsBuilder.AppendLine("    if (event.key == '+') {");
            tsBuilder.AppendLine("      this.add_button_click();");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("    if (event.ctrlKey && event.shiftKey && event.key === 'F') {");
            tsBuilder.AppendLine("      event.preventDefault();");
            tsBuilder.AppendLine("      this.myInputField.nativeElement.focus();");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  //handle_row_keydown(event: KeyboardEvent, code: any) {");
            tsBuilder.AppendLine("  //  if (event.ctrlKey && event.key === 'E') {");
            tsBuilder.AppendLine("  //    event.preventDefault();");
            tsBuilder.AppendLine("  //    event.stopPropagation();");
            tsBuilder.AppendLine("  //    this.edit_common_master(code);");
            tsBuilder.AppendLine("  //  }");
            tsBuilder.AppendLine("  //}");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  pageChangeHandler(event: any): void {");
            tsBuilder.AppendLine("    this.query.page_index = event.pageIndex;");
            tsBuilder.AppendLine("    this.query.page_size = event.pageSize;");
            tsBuilder.AppendLine("    this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  searchChangeHandler(): void {");
            tsBuilder.AppendLine("    this.query.search_text = this.searched_text;");
            tsBuilder.AppendLine("    this.query.page_index = 0;");
            tsBuilder.AppendLine("    if (this.paginator) {");
            tsBuilder.AppendLine("      this.paginator.pageIndex = 0;");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("    this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  sortClick(headerName: string) {");
            tsBuilder.AppendLine("    if (headerName) {");
            tsBuilder.AppendLine("      if (this.query.sort_col_name === headerName) {");
            tsBuilder.AppendLine("        //this.query.IsFreeSearch = false;");
            tsBuilder.AppendLine("        this.query.sort_type = this.query.sort_type === 'asc' ? 'desc' : 'asc';");
            tsBuilder.AppendLine("      }");
            tsBuilder.AppendLine("      this.query.sort_col_name = headerName;");
            tsBuilder.AppendLine("      //this.query.search_column = headerName;");
            tsBuilder.AppendLine("      this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  isSorting(name: string) {");
            tsBuilder.AppendLine("    return this.query.sort_col_name !== name && name !== '';");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  isSortAsc(name: string) {");
            tsBuilder.AppendLine("    return this.query.sort_col_name === name && this.query.sort_type === 'asc';");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  isSortDesc(name: string) {");
            tsBuilder.AppendLine("    return this.query.sort_col_name === name && this.query.sort_type === 'desc';");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("}");



        }

        public void generate_component_service_ts()
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { Injectable } from '@angular/core';");
            tsBuilder.AppendLine("import { Observable, of } from 'rxjs';");
            tsBuilder.AppendLine("import { HttpClient } from '@angular/common/http';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("import { environment } from '../../../../environments/environment';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("@Injectable({");
            tsBuilder.AppendLine("  providedIn: 'root'");
            tsBuilder.AppendLine("})");
            tsBuilder.AppendLine("export class QuickMasterService {");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public controller_name: string = 'quickmaster';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  constructor(public http: HttpClient) { }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region to get CommonMasterTypes list */");
            tsBuilder.AppendLine("  //public get_commonmastertypes_list(): Observable<any> {");
            tsBuilder.AppendLine("  //  return this.http.post(environment.apiUrl + this.controller_name + '/get_commonmastertypes_list', null);");
            tsBuilder.AppendLine("  //}");
            tsBuilder.AppendLine("  public get_commonmastertypes_list(query: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/get_commonmastertypes_list', query);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion CommonMasterTypes */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region To Remove CommonMasterTypes */");
            tsBuilder.AppendLine("  public remove_commonMasterTypes(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/remove_commonMasterTypes', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion To Remove CommonMasterTypes */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region to get CustomFieldConfig List */");
            tsBuilder.AppendLine("  public get_cmnMst_CustomFieldConfig_list(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/get_cmnMst_CustomFieldConfig_list', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion to get CustomFieldConfig List */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region  To Add Or Update  CustomFieldConfig */");
            tsBuilder.AppendLine("  public addorupdate_cmnMst_CustomFieldConfig(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/addorupdate_cmnMst_CustomFieldConfig', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion  To Add Or Update  CustomFieldConfig */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region To Remove CustomFieldConfig */");
            tsBuilder.AppendLine("  public remove_cmnMst_CustomFieldConfig(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/remove_cmnMst_CustomFieldConfig', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion To Remove CommonMasterTypes */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public delete_common_master_type(query: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/delete_common_master_type', query);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public get_common_master_type_info(): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/get_common_master_type_info', null);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("}");

        }


        /// <summary>
        /// Step 1 Create Service ts
        /// </summary>
        /// <param name="entity_name"></param>
        /// <param name="_entityservice"></param>
        /// <returns></returns>


        public static Dictionary<string, string> SystemReserveKeyValue()
        {
            var result = new Dictionary<string, string>();
            result.Add("created_on", "Created On");
            result.Add("created_by", "Created By");
            result.Add("updated_on", "Updated On");
            result.Add("updated_by", "Updated By");
            result.Add("update_count", "Update Count");
            result.Add("deleted_on", "Deleted On");
            result.Add("deleted_by", "Deleted By");
            result.Add("is_locked", "Is Active");
            return result;
        }

        // {entity_name}.service.ts
        public static Tuple<string, string, Dictionary<string, string>> generate_component_service_ts(string entity_name, string controller_name, string _entityservice, Dictionary<string, string> dic_apiendpoints)
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { Injectable } from '@angular/core';");
            tsBuilder.AppendLine("import { Observable } from 'rxjs';");
            tsBuilder.AppendLine("import { HttpClient } from '@angular/common/http';");
            tsBuilder.AppendLine("import { environment } from '../../../../environments/environment';");
            tsBuilder.AppendLine("import { Router } from '@angular/router';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("@Injectable({");
            tsBuilder.AppendLine("providedIn: 'root'");
            tsBuilder.AppendLine("})");

            tsBuilder.AppendLine($"export class {_entityservice}");
            tsBuilder.AppendLine("{");

            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("public controller_name: string = \"" + controller_name + "\";");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("constructor(public http: HttpClient, private router: Router) { }");
            tsBuilder.AppendLine("\n");

            // For To Get Data For Listing View
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.view_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(environment.apiUrl + this.controller_name + '/" + dic_apiendpoints[UtilityScript.view_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            // For Create & Update
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.create_update_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(environment.apiUrl + this.controller_name + '/" + dic_apiendpoints[UtilityScript.create_update_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            // For Get Perticular Record
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.read_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(environment.apiUrl + this.controller_name + '/" + dic_apiendpoints[UtilityScript.read_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            // For Delete Record
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.delete_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(environment.apiUrl + this.controller_name + '/" + dic_apiendpoints[UtilityScript.delete_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            tsBuilder.AppendLine("}");
            return new Tuple<string, string, Dictionary<string, string>>(_entityservice, tsBuilder.ToString(), dic_apiendpoints);
        }

        // {entity_name}.service.spec.ts
        public static string generate_component_service_spec_ts(string entity_name, string _entityservice)
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { TestBed }  from '@angular/core/testing';");
            tsBuilder.AppendLine("import { " + _entityservice + " } from './" + entity_name.ToLower() + ".service';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("describe('" + _entityservice + "', () => {");
            tsBuilder.AppendLine("  let service: " + _entityservice + ";");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("beforeEach(() => {");
            tsBuilder.AppendLine("TestBed.configureTestingModule({ });");
            tsBuilder.AppendLine("service = TestBed.inject(" + _entityservice + ");");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("it('should be created', () => {");
            tsBuilder.AppendLine("expect(service).toBeTruthy();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("});");

            return tsBuilder.ToString();
        }


        /// <summary>
        /// To Generate List Component
        /// </summary>
        /// <param name="entity_name"></param>
        /// <param name="title"></param>
        /// <returns></returns>

        // {entity_name}.component.html
        public static string generate_list_component_html(string entity_name, string title)
        {


            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine($"<div id=\"{entity_name}_container\" tabindex=\"1\" (keydown)=\"handleKeyPlus($event)\" style=\"border:none;outline:none\">");
            htmlBuilder.AppendLine($"<div class=\"page-header\">");
            htmlBuilder.AppendLine($"<div class=\"row align-items-center\">");
            htmlBuilder.AppendLine($"<div class=\"col\">");
            htmlBuilder.AppendLine("<h2>{{_title}} - {{masters_count}} Records</h2>");
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"<div class=\"col-12 col-md-6 col-lg-4 col-xl-3 col-xxl-2\">");
            htmlBuilder.AppendLine($"<div class=\"input-group search-box shadow-sm\">");
            htmlBuilder.AppendLine("<input type=\"text\" class=\"form-control\" #search_qmd_field placeholder=\"Search in {{_title}}\" [(ngModel)]=\"searched_text\" (keyup)=\"searchChangeHandler()\" id=\"search_qmd_field\" autocomplete=\"off\" />");
            htmlBuilder.AppendLine($"<button class=\"btn\" id=\"btnsearchsubmit\" (click)=\"searchChangeHandler()\"><i class=\"bi bi-search\"></i></button>");
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"</div>");

            htmlBuilder.AppendLine($"<div>");
            htmlBuilder.AppendLine($"<m2-grid [config]=\"grid_config\" ");
            htmlBuilder.AppendLine($"[column_config]=\"field_config\" ");
            htmlBuilder.AppendLine($"[data]=\"items\" ");
            htmlBuilder.AppendLine($"(custom_events)=\"custom_events($event)\" ");
            htmlBuilder.AppendLine($"(cell_value_change)=\"change_cell_value($event)\" ");
            htmlBuilder.AppendLine($" >");
            htmlBuilder.AppendLine($"</m2-grid>");
            htmlBuilder.AppendLine($"<div class=\"table-footer page-content-head d-flex align-items-center justify-content-between mt-2 px-2\"> ");
            htmlBuilder.AppendLine($"<div class=\"report-btn-group my-1\"> ");
            //htmlBuilder.AppendLine($"<div class=\"doc-btn\"> ");
            //htmlBuilder.AppendLine($"<button matTooltip=\"Export to Excel\" matTooltipClass=\"primary-tooltip\" class=\"btn icon-btn excel\" value=\"EXCEL\" (click)=\"export_to_excel()\"><svg class=\"w-100 h-100\"><use xlink:href=\"./assets/images/excel-ic.svg#excel\"></use></svg></button>");
            //htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"<mat-paginator #master_paginator [length]=\"masters_count\" ");
            htmlBuilder.AppendLine($"[pageSize]=\"100\" ");
            htmlBuilder.AppendLine($"[pageIndex]=\"0\" ");
            htmlBuilder.AppendLine($"[pageSizeOptions]=\"[100, 300, 600, 1000]\" ");
            htmlBuilder.AppendLine($"aria-label=\"Select page\" (page)=\"pageChangeHandler($event)\"> ");
            htmlBuilder.AppendLine($"</mat-paginator>");
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"<a class=\"btn add-btn shadow\" (click)=\"add_edit_record('0')\" cdkDragBoundary=\".page-body\" cdkDrag><i class=\"bi bi-plus\" ></i></a>");
            htmlBuilder.AppendLine($"</div>");

            return htmlBuilder.ToString();



        }

        // {entity_name}.component.scss
        public static string generate_list_component_scss(string entity_name, string title)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            return htmlBuilder.ToString();
        }


        // {entity_name}.component.ts
        public static Tuple<string, string> generate_list_component_ts(string entity_name, string title, string _entityservice, string primary_key_column, Dictionary<string, string> entityServiceMethod, string addeditcomponent_name, DataTable control_config)
        {
            string _entityname = UtilityScript.ConvertToTitleCase(entity_name);
            string add_edit_component = addeditcomponent_name;
            string list_component_name = $"{UtilityScript.ConvertToTitleCase(entity_name)}Component";
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { AfterViewInit, Component, ElementRef, OnInit, ViewChild }  from '@angular/core';");
            tsBuilder.AppendLine("import { MatDialog, MatDialogRef }  from '@angular/material/dialog';");
            tsBuilder.AppendLine("import { HttpResponse }   from '@angular/common/http';");
            tsBuilder.AppendLine("import { ActivatedRoute, Router }  from '@angular/router';");
            tsBuilder.AppendLine("import { MatPaginator }  from '@angular/material/paginator';");
            tsBuilder.AppendLine("//import { ToastrService }  from 'ngx-toastr';");
            tsBuilder.AppendLine("import { DynamicAlertService }  from '../../../shared/components/dynamic-alert/dynamic-alert.service';");
            tsBuilder.AppendLine("//import { SharedService }  from './../../../shared/shared.service';");
            tsBuilder.AppendLine("import { " + entity_name + "Service }  from './" + entity_name + ".service';");
            string add_edit_component_name = _entityname + "AddEditComponent";
            tsBuilder.AppendLine("import { " + add_edit_component_name + "}  from './" + add_edit_component + "/" + add_edit_component + ".component';");
            tsBuilder.AppendLine("import { CommonService } from '../../common.service';");

            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("@Component({");
            tsBuilder.AppendLine($"selector: 'app-{entity_name}',");
            tsBuilder.AppendLine($"templateUrl: './{entity_name}.component.html',");
            tsBuilder.AppendLine($"styleUrls: ['./{entity_name}.component.scss']");
            tsBuilder.AppendLine("})");
            tsBuilder.AppendLine("export class " + list_component_name + " implements OnInit, AfterViewInit {");
            // Component Start
            tsBuilder.AppendLine("public _title: string;");
            tsBuilder.AppendLine("public items: any[];");
            tsBuilder.AppendLine($"public {primary_key_column}: string;");
            tsBuilder.AppendLine("public column_names: any[];");
            tsBuilder.AppendLine("public searched_text: string = '';");
            tsBuilder.AppendLine("public query: any;");
            tsBuilder.AppendLine("public masters_count: number;");
            tsBuilder.AppendLine("@ViewChild('master_paginator') paginator: MatPaginator;");
            tsBuilder.AppendLine("@ViewChild('search_qmd_field') myInputField: ElementRef;");
            tsBuilder.AppendLine("public field_config: any[];");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("grid_config: any = {");
            tsBuilder.AppendLine($"section: '{entity_name}',");
            tsBuilder.AppendLine("allow_add: false,");
            tsBuilder.AppendLine("allow_edit: false,");
            tsBuilder.AppendLine("allow_delete: false,");
            tsBuilder.AppendLine("full_width: true");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("constructor(");
            tsBuilder.AppendLine("private dialog: MatDialog,");
            tsBuilder.AppendLine($"private objDataService: {_entityservice},");
            tsBuilder.AppendLine("private activatedRoute: ActivatedRoute,");
            tsBuilder.AppendLine("private router: Router,");
            tsBuilder.AppendLine("//private toastr: ToastrService,");
            tsBuilder.AppendLine("private m2_alert: DynamicAlertService,");
            tsBuilder.AppendLine("//private shared_service: SharedService");
            tsBuilder.AppendLine("private common_service: CommonService");
            tsBuilder.AppendLine(") { }");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("ngOnInit(): void {");
            tsBuilder.AppendLine("this.init();");
            tsBuilder.AppendLine("this.get_data();");
            tsBuilder.AppendLine($"let ele: HTMLElement = document.getElementById('{entity_name}_container');");
            tsBuilder.AppendLine("ele.focus();");
            tsBuilder.AppendLine("}");

            tsBuilder.AppendLine("ngAfterViewInit(): void {");
            tsBuilder.AppendLine("setTimeout(() => {");
            tsBuilder.AppendLine("var elem = document.getElementById('search_qmd_field');");
            tsBuilder.AppendLine("elem.focus();");
            tsBuilder.AppendLine("}, 300);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("public get_data()");
            tsBuilder.AppendLine("{");
            tsBuilder.AppendLine("this.get_master_data();");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("public init()");
            tsBuilder.AppendLine("{");
            tsBuilder.AppendLine("this.searched_text = '';");
            tsBuilder.AppendLine("this.query = {");
            tsBuilder.AppendLine("page_number: 1,");
            tsBuilder.AppendLine("page_size: 10,");
            tsBuilder.AppendLine($"search_column: '{primary_key_column}',");
            tsBuilder.AppendLine("search_text: '',");
            tsBuilder.AppendLine($"sort_col_name: '{primary_key_column}',");
            tsBuilder.AppendLine("sort_type: 'Desc'");
            tsBuilder.AppendLine(" };");
            tsBuilder.AppendLine("this._title = null;");
            tsBuilder.AppendLine($"this.{primary_key_column} = null;");
            tsBuilder.AppendLine("this.items = [];");
            tsBuilder.AppendLine("this.column_names = [];");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");
            // For Get All Listing Data
            tsBuilder.AppendLine("public get_master_data(): void {");

            // Start
            tsBuilder.AppendLine($"this.objDataService.{entityServiceMethod[UtilityScript.view_key]}(this.query)");
            tsBuilder.AppendLine(".subscribe({");
            tsBuilder.AppendLine("next: (RtnData) => {");
            tsBuilder.AppendLine("this.field_config = RtnData.Data[0].Table;");
            tsBuilder.AppendLine("this.items = RtnData.Data[1].Table1;");
            tsBuilder.AppendLine("this.masters_count = RtnData.DataCount;");
            tsBuilder.AppendLine($"this._title = '{title}'");
            ////tsBuilder.AppendLine($"if (false)");
            ////tsBuilder.AppendLine("{");
            ////tsBuilder.AppendLine("this.column_names = this.column_names.filter((x, i) => i > 1); ");
            ////tsBuilder.AppendLine("this.field_config = [];");

            //////tsBuilder.AppendLine("for (var i = 0; i < this.column_names.length; i++)");
            //////tsBuilder.AppendLine("{");
            //////For Loop

            ////int ColumnOrder = 0;
            ////tsBuilder.AppendLine("\n");
            ////var SystemReserveKey = SystemReserveKeyValue();
            ////for (var b = 0; b < control_config.Rows.Count; b++)
            ////{

            ////    string COLUMN_NAME = Convert.ToString(control_config.Rows[b]["Column Name"]).ToLower();
            ////    if (!SystemReserveKey.Keys.Contains(COLUMN_NAME))
            ////    {
            ////        tsBuilder.AppendLine("this.field_config.push({");
            ////        tsBuilder.AppendLine($"ColumnOrder: {ColumnOrder},");
            ////        tsBuilder.AppendLine($"ColumnName: '{COLUMN_NAME}'");
            ////        tsBuilder.AppendLine($", ColumnCaption: '{COLUMN_NAME}'");
            ////        tsBuilder.AppendLine($", width: {(COLUMN_NAME.Equals("description") ? "400" : "null")}");
            ////        tsBuilder.AppendLine($", control: '{(COLUMN_NAME.Equals("is_locked") ? "checkbox" : "default")}' ");

            ////        if (COLUMN_NAME.Equals(primary_key_column) || COLUMN_NAME.Equals("row_no") || COLUMN_NAME.Equals("deleted_on") || COLUMN_NAME.Equals("deleted_by"))
            ////        {

            ////            tsBuilder.AppendLine($", IsVisible: false");
            ////        }
            ////        else
            ////        {
            ////            tsBuilder.AppendLine($", IsVisible: true");
            ////        }
            ////        //tsBuilder.Append($"this.column_names[i]['COLUMN_NAME'].toLowerCase() == '{primary_key_column}' || this.column_names[i]['COLUMN_NAME'].toLowerCase() == 'row_no' || ");
            ////        //tsBuilder.Append($"this.column_names[i]['COLUMN_NAME'].toLowerCase() == 'deleted_on' || this.column_names[i]['COLUMN_NAME'].toLowerCase() == 'deleted_by' ");

            ////        //tsBuilder.Append(")");
            ////        if (COLUMN_NAME.Equals(primary_key_column))
            ////        {
            ////            tsBuilder.AppendLine($", is_primary: true");
            ////        }
            ////        else
            ////        {
            ////            tsBuilder.AppendLine($", is_primary: false");
            ////        }

            ////        tsBuilder.AppendLine(", sorting: true");
            ////        tsBuilder.AppendLine("});");
            ////        tsBuilder.AppendLine("\n");
            ////        ColumnOrder++;
            ////    }
            ////}
            ////foreach (var b in SystemReserveKey)
            ////{

            ////    string COLUMN_NAME = Convert.ToString(b.Key).ToLower();
            ////    tsBuilder.AppendLine("this.field_config.push({");
            ////    tsBuilder.AppendLine($"ColumnOrder: {ColumnOrder},");
            ////    tsBuilder.AppendLine($"ColumnName: '{COLUMN_NAME}'");
            ////    tsBuilder.AppendLine($", ColumnCaption: '{b.Value}'");
            ////    tsBuilder.AppendLine($", width: {(COLUMN_NAME.Equals("description") ? "400" : "null")}");
            ////    tsBuilder.AppendLine($", control: '{(COLUMN_NAME.Equals("is_locked") ? "checkbox" : "default")}' ");
            ////    if (COLUMN_NAME.Equals(primary_key_column) || COLUMN_NAME.Equals("row_no") || COLUMN_NAME.Equals("deleted_on") || COLUMN_NAME.Equals("deleted_by"))
            ////    {

            ////        tsBuilder.AppendLine($", IsVisible: false");
            ////    }
            ////    else
            ////    {
            ////        tsBuilder.AppendLine($", IsVisible: true");
            ////    }
            ////    if (COLUMN_NAME.Equals(primary_key_column))
            ////    {
            ////        tsBuilder.AppendLine($", is_primary: true");
            ////    }
            ////    else
            ////    {
            ////        tsBuilder.AppendLine($", is_primary: false");
            ////    }
            ////    tsBuilder.AppendLine(", sorting: true");
            ////    tsBuilder.AppendLine("});");
            ////    tsBuilder.AppendLine("\n");
            ////    ColumnOrder++;

            ////}
            //////For Loop
            ////// tsBuilder.AppendLine("}");


            ////// Add Edit & Delete btn

            ////tsBuilder.AppendLine("this.field_config.push({");
            ////tsBuilder.AppendLine("ColumnOrder: this.column_names.length + 2, ColumnCaption: 'Edit', ColumnName: 'edit_action', IsVisible: true, IsEnable: true, IsMust: true, control: 'button',");
            ////tsBuilder.AppendLine("control_content: '<i class=\"bi bi-pencil-square\"></i>', control_tooltip: 'Edit Record', shortcut_key: 'e', width: \"60\" ");
            ////tsBuilder.AppendLine("});");
            ////tsBuilder.AppendLine("this.field_config.push({");
            ////tsBuilder.AppendLine("ColumnOrder: this.column_names.length + 3, ColumnCaption: 'Delete', ColumnName: 'delete_action', IsVisible: true, IsEnable: true, IsMust: true, control: 'button',");
            ////tsBuilder.AppendLine("control_content: '<i class=\"bi bi-trash\"></i>', control_tooltip: 'Edit Record', shortcut_key: 'd', width: \"60\" ");
            ////tsBuilder.AppendLine("});");
            ////tsBuilder.AppendLine("console.log(this.field_config)");
            ////tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("//this.shared_service.header_tab('open', this._title, this.activatedRoute.snapshot['_routerState']['url'], null)");
            tsBuilder.AppendLine("},");
            tsBuilder.AppendLine("error: (err_response) => {");
            tsBuilder.AppendLine("this.m2_alert.fire({");
            tsBuilder.AppendLine("title: 'Error',");
            tsBuilder.AppendLine(" text: err_response[\"error\"][\"message\"], ");
            tsBuilder.AppendLine("icon: \"error\" ");
            tsBuilder.AppendLine(" });");
            tsBuilder.AppendLine("this.router.navigate(['/home']);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("});");
            //End
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("\n");

            // Call Edit Componet

            tsBuilder.AppendLine("public add_edit_record(_" + primary_key_column + ": string): void {");
            // Start
            tsBuilder.AppendLine("let dialogRef = this.dialog.open(" + add_edit_component_name + ", { data: { " + primary_key_column + ": _" + primary_key_column + " }, disableClose: true, panelClass: 'mid-width' });");
            tsBuilder.AppendLine("dialogRef.afterClosed().subscribe((res) => {");
            tsBuilder.AppendLine("this.get_master_data();");
            tsBuilder.AppendLine("});");
            // END
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("\n");

            // Page Change Handler
            tsBuilder.AppendLine("pageChangeHandler(event: any): void {");
            //Start
            //tsBuilder.AppendLine("this.query.page_index = event.pageIndex;");
            tsBuilder.AppendLine("this.query.page_size = event.pageSize; ");
            tsBuilder.AppendLine("this.get_master_data(); ");
            //End
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("\n");

            // Timer Section
            tsBuilder.AppendLine("typingTimer: any; ");
            tsBuilder.AppendLine("doneTypingInterval: number = 200; // 200 milisecond ");
            tsBuilder.AppendLine("timerActive: boolean = false; ");
            tsBuilder.AppendLine("\n");

            // Search Change Handler
            tsBuilder.AppendLine("searchChangeHandler(): void { ");
            //Start
            tsBuilder.AppendLine("clearTimeout(this.typingTimer); ");
            tsBuilder.AppendLine("if (!this.timerActive) ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("this.typingTimer = setTimeout(() => { ");
            tsBuilder.AppendLine("this.query.search_text = this.searched_text; ");
            tsBuilder.AppendLine("// this.query.page_index = this.paginator.pageIndex = 0; ");
            tsBuilder.AppendLine("this.get_master_data(); ");
            tsBuilder.AppendLine("}, this.doneTypingInterval); ");
            tsBuilder.AppendLine("} ");
            //End
            tsBuilder.AppendLine("} ");

            tsBuilder.AppendLine("\n");


            // Delete Record
            tsBuilder.AppendLine("public delete_record(" + primary_key_column + ": string): void { ");
            //Start
            tsBuilder.AppendLine("this.m2_alert.fire({ ");
            tsBuilder.AppendLine("title: 'Do you want to delete this data ?', ");
            tsBuilder.AppendLine("text: 'This process is irreversible.', ");
            tsBuilder.AppendLine("icon: 'warning', ");
            tsBuilder.AppendLine("showCancelButton: true, ");
            tsBuilder.AppendLine("confirmButtonText: 'yes', ");
            tsBuilder.AppendLine("cancelButtonText: 'no' ");
            tsBuilder.AppendLine("}).then((r) => { ");
            tsBuilder.AppendLine("if (r && r.isConfirmed) ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("this.objDataService." + entityServiceMethod[UtilityScript.delete_key] + "({ " + primary_key_column + ": " + primary_key_column + " }).subscribe({ ");
            tsBuilder.AppendLine("next: (data) => { ");
            tsBuilder.AppendLine("//this.toastr.success(data[0][\"message\"]); ");
            tsBuilder.AppendLine("this.items = this.items.filter((x) => { ");
            tsBuilder.AppendLine("return !(x." + primary_key_column + " == " + primary_key_column + ") ");
            tsBuilder.AppendLine("}) ");
            tsBuilder.AppendLine("}, error: (err_response) => { ");
            tsBuilder.AppendLine("this.m2_alert.fire({ ");
            tsBuilder.AppendLine("title: 'Error', ");
            tsBuilder.AppendLine("text: err_response[\"error\"][\"message\"], ");
            tsBuilder.AppendLine("icon: \"error\" ");
            tsBuilder.AppendLine("}); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}) ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}) ");
            //End
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("\n");

            // handle Key Plus
            tsBuilder.AppendLine("handleKeyPlus(event: KeyboardEvent) { ");
            //Start
            tsBuilder.AppendLine("if (event.key == '+') { ");
            tsBuilder.AppendLine($"this.add_edit_record(null); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("if (event.ctrlKey && event.shiftKey && event.key === 'F') { ");
            tsBuilder.AppendLine("event.preventDefault(); ");
            tsBuilder.AppendLine("this.myInputField.nativeElement.focus(); ");
            tsBuilder.AppendLine("} ");
            //End
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("\n");


            // Export to Excel
            tsBuilder.AppendLine("export_to_excel(): void { ");
            //Start
            tsBuilder.AppendLine($"this.objDataService.{entityServiceMethod[UtilityScript.view_key]}(this.query)");
            tsBuilder.AppendLine(".subscribe({ ");
            tsBuilder.AppendLine("next: (event) => { ");
            tsBuilder.AppendLine("let data = event as HttpResponse<Blob>; ");
            tsBuilder.AppendLine("const downloadedFile = new Blob([data.body as BlobPart], { ");
            tsBuilder.AppendLine("type: data.body?.type ");
            tsBuilder.AppendLine("}); ");
            tsBuilder.AppendLine("if (downloadedFile != null && downloadedFile.type != '') ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("var link = document.createElement('a'); ");
            tsBuilder.AppendLine("var downloadURL = window.URL.createObjectURL(downloadedFile); ");
            tsBuilder.AppendLine("link.href = downloadURL; ");
            tsBuilder.AppendLine("link.download = this._title + \".xlsx\"; ");
            tsBuilder.AppendLine("link.click(); ");
            tsBuilder.AppendLine("window.URL.revokeObjectURL(downloadURL); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}, error: (err_response) => { ");
            tsBuilder.AppendLine("this.m2_alert.fire({ ");
            tsBuilder.AppendLine("title: 'Error', ");
            tsBuilder.AppendLine("text: err_response[\"error\"][\"message\"], ");
            tsBuilder.AppendLine("icon: \"error\" ");
            tsBuilder.AppendLine("}); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}); ");
            //End
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("\n");

            //custom events
            tsBuilder.AppendLine("public custom_events(event: any) { ");
            //Start
            tsBuilder.AppendLine("let ColumnCaption = String(event?.config?.ColumnCaption).trim().toLowerCase(); ");
            tsBuilder.AppendLine("if (ColumnCaption == 'edit') ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine($"this.add_edit_record(event.data.{primary_key_column}); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("else if (ColumnCaption == 'delete') ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine($"this.delete_record(event.data.{primary_key_column}); ");
            tsBuilder.AppendLine("} ");
            //End
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("\n");

            //for Is active

            tsBuilder.AppendLine("table_info: any = {");
            tsBuilder.AppendLine($"tablename: 'stay.{entity_name}',");
            tsBuilder.AppendLine($"primarykeycolumn: '{primary_key_column}',");
            tsBuilder.AppendLine("primarykeyvalue: '0',");
            tsBuilder.AppendLine("fieldcolumn: 'is_locked',");
            tsBuilder.AppendLine("fieldvalue: '0'");
            tsBuilder.AppendLine("}");

            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("change_cell_value(event: any) {");
            tsBuilder.AppendLine("this.table_info.primarykeyvalue = event.current_data [this.table_info.primarykeycolumn];");
            tsBuilder.AppendLine("this.table_info.fieldcolumn = event.column_config.ColumnName;");
            tsBuilder.AppendLine("this.table_info.fieldvalue = event.current_data [this.table_info.fieldcolumn];");
            tsBuilder.AppendLine("this.common_service.updatestatus_by_table(this.table_info).subscribe({");
            tsBuilder.AppendLine("next: (data) => {");
            tsBuilder.AppendLine("//this.toastr.success(data[0][\"message\"]); ");
            tsBuilder.AppendLine("this.m2_alert.fire({");
            tsBuilder.AppendLine("title: 'Success',");
            tsBuilder.AppendLine("text: \"Status Updated Successfully!\",");
            tsBuilder.AppendLine("icon: \"success\" ");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("this.get_master_data();");
            tsBuilder.AppendLine("}, error: (err_response) => {");
            tsBuilder.AppendLine("this.m2_alert.fire({");
            tsBuilder.AppendLine("title: 'Error',");
            tsBuilder.AppendLine("text: err_response[\"error\"][\"message\"],");
            tsBuilder.AppendLine("icon: \"error\" ");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("})");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");
            // Component End
            tsBuilder.AppendLine("} ");

            return new Tuple<string, string>(list_component_name, tsBuilder.ToString());
        }

        // {entity_name}.component.spec.ts
        public static string generate_list_component_spec_ts(string entity_name, string title, string list_component_name)
        {
            string ComponentName = list_component_name;
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { ComponentFixture, TestBed } from '@angular/core/testing';");
            tsBuilder.AppendLine("import { " + ComponentName + " } from './" + entity_name + ".component';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("describe('" + ComponentName + "', () => {");
            tsBuilder.AppendLine($"let component: {ComponentName};");
            tsBuilder.AppendLine($"let fixture: ComponentFixture<{ComponentName}>;");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("beforeEach(() => {");
            tsBuilder.AppendLine("TestBed.configureTestingModule({");
            tsBuilder.AppendLine($"declarations: [{ComponentName}]");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine($"fixture = TestBed.createComponent({ComponentName});");
            tsBuilder.AppendLine("component = fixture.componentInstance;");
            tsBuilder.AppendLine("fixture.detectChanges();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("it('should create', () => {");
            tsBuilder.AppendLine("expect(component).toBeTruthy();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("});");
            return tsBuilder.ToString();
        }

        /// To Generate Add & Update Componenet
        /// 


        //{entity_name}-add-edit.component.html
        public static Tuple<string, List<table_design>> generate_add_edit_component_html_m2(string entity_name, string title, string primary_key_column, DataTable control_config, string schema_name)
        {
            var sort_meta_data = new List<table_design>();
            string form_group_name = $"{entity_name}Form";
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine($"<div id=\"{entity_name}_entry_container\" tabindex=\"1\"  style=\"border:none;outline:none\"> ");
            htmlBuilder.AppendLine($"<div class=\"container-fluid common-master-form\"> ");
            htmlBuilder.AppendLine($"<div mat-dialog-title class=\"d-flex justify-content-between align-items-center\"> ");
            htmlBuilder.AppendLine("<h3>{{_title}}</h3>");
            htmlBuilder.AppendLine($"<button class=\"btn close-btn\" (click)=\"close_dialog(null)\"><i class=\"bi bi-x-lg\"></i></button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"<form [formGroup]=\"{entity_name}Form\" *ngIf=\"{entity_name}Form\"> ");
            htmlBuilder.AppendLine($"<mat-dialog-content class=\"mat-typography\"> ");

            var table_meta_data = SQLScriptGenerator.GetTable_Designs(entity_name, schema_name);
            var sys_column_lst = SQLScriptGenerator.sys_column_lst;
            sys_column_lst.Add("is_locked");

            sort_meta_data = table_meta_data.Where(g => !sys_column_lst.Contains(g._column_name.ToLower().Trim())).ToList();
            foreach (var item in sort_meta_data)
            {
                var _column_name = item._column_name;
                if (primary_key_column != _column_name)
                {
                    var _preferred_controls = item._preferred_controls;
                    var caption = item._preferred_controls;
                    var IsMandetory = !item.IsNullable;
                    var getuserData = control_config.AsEnumerable().Where(g => Convert.ToString(g["Column Name"]).ToLower().Trim() == _column_name).ToArray();
                    if (getuserData != null && getuserData.Length > 0)
                    {
                        _preferred_controls = Convert.ToString(getuserData[0]["Control Name"]);
                        caption = Convert.ToString(getuserData[0]["Caption"]);
                        IsMandetory = Convert.ToString(getuserData[0]["Must"]).ToLower().Trim().Equals("true");
                    }
                    htmlBuilder.AppendLine($"<div class=\"row\" > ");
                    htmlBuilder.AppendLine($"<div class=\"col-12 col-lg-6 form-inline d-flex align-items-end py-2\" > ");
                    htmlBuilder.AppendLine($"<div class=\"form-group\"> ");
                    if (_preferred_controls == "txtbox")
                    {
                        htmlBuilder.AppendLine($"<smart-txtbox  ");
                        htmlBuilder.AppendLine($"class=\"w-100\" [form_group]=\"{form_group_name}\" caption=\"{caption}\" ");
                        htmlBuilder.AppendLine($"type=\"text\" field=\"{item._column_name}\" ");
                        htmlBuilder.AppendLine($"(copy)=\"false\" (paste)=\"false\" autocomplete=\"disabled\" ");
                        htmlBuilder.AppendLine($"[is_req]=\"{IsMandetory}\" ");
                        if (false)
                        {
                            htmlBuilder.AppendLine($"[is_numeric_with_minus]=\"custom_fields[i].datatype == 2\" ");
                            htmlBuilder.AppendLine($"[is_bit]=\"custom_fields[i].datatype == 3\" ");
                            htmlBuilder.AppendLine($"[maxlen]=\"custom_fields[i].text_max_length\" ");
                            htmlBuilder.AppendLine($"[minlen]=\"custom_fields[i].text_min_length\" ");
                            htmlBuilder.AppendLine($"[maxnumvalue]=\"custom_fields[i].num_max_value\" ");
                            htmlBuilder.AppendLine($"[digitafterdecimal]=\"custom_fields[i].digits_after_decimal\" ");
                            htmlBuilder.AppendLine($"(keypress)=\"text_value_key_up_handler($event)\" ");
                            htmlBuilder.AppendLine($"(keydown)=\"text_value_key_down_handler($event,custom_fields[i].text_case)\" ");
                            htmlBuilder.AppendLine($"[block_spl_char]=\"custom_fields[i].text_type==1\" ");
                            htmlBuilder.AppendLine($"[is_OnlyAlphabet]=\"custom_fields[i].text_type==2\" ");
                        }
                        htmlBuilder.AppendLine($"></smart-txtbox> ");
                    }
                    if (_preferred_controls == "date-picker")
                    {
                        htmlBuilder.AppendLine($"<smart-date-picker  [form_group]=\"{form_group_name}\" ");
                        htmlBuilder.AppendLine($"field=\"{item._column_name}\" [is_req]=\"custom_fields[i].IsMandetory\" caption=\"{caption}\"> ");
                        htmlBuilder.AppendLine($"</smart-date-picker> ");
                    }
                    if (_preferred_controls == "select")
                    {
                        htmlBuilder.AppendLine($"<smart-select  class=\"smart-componenet w-100\" ");
                        htmlBuilder.AppendLine($"[formGroup]=\"{form_group_name}\" field=\"{item._column_name}\" ");
                        htmlBuilder.AppendLine($"[IsReq]=\"custom_fields[i].IsMandetory\" Caption=\"{caption}\" ");
                        htmlBuilder.AppendLine($"[options]=\"custom_fields[i].options\" ");
                        htmlBuilder.AppendLine($"[multi_select]=\"false\"></smart-select> ");
                    }
                    if (_preferred_controls == "switch" || _column_name == "is_locked")
                    {
                        htmlBuilder.AppendLine($"<label class=\" me-2 w-auto\">Locked</label> ");
                        htmlBuilder.AppendLine($"<smart-switch [formGroup]=\"{form_group_name}\" ");
                        htmlBuilder.AppendLine($"field=\"{caption}\"> ");
                        htmlBuilder.AppendLine($"</smart-switch> ");
                    }
                    htmlBuilder.AppendLine($"</div> ");
                    htmlBuilder.AppendLine($"</div> ");
                    htmlBuilder.AppendLine($"</div> ");
                }
            }

            if (table_meta_data.Where(g => g._column_name.Equals("is_locked")).ToList() != null)
            {
                htmlBuilder.AppendLine($"<div class=\"row\" > ");
                htmlBuilder.AppendLine($"<div class=\"col-12 col-lg-6 form-inline d-flex align-items-end py-2\" > ");
                htmlBuilder.AppendLine($"<div class=\"form-group\"> ");
                htmlBuilder.AppendLine($"<label class=\" me-2 w-auto\">Locked</label> ");
                htmlBuilder.AppendLine($"<smart-switch [formGroup]=\"{form_group_name}\" ");
                htmlBuilder.AppendLine($"field=\"is_locked\" > ");
                htmlBuilder.AppendLine($"</smart-switch> ");
                htmlBuilder.AppendLine($"</div> ");
                htmlBuilder.AppendLine($"</div> ");
                htmlBuilder.AppendLine($"</div> ");
            }

            htmlBuilder.AppendLine($"</mat-dialog-content> ");
            htmlBuilder.AppendLine($"<mat-dialog-actions class=\"foot-action-btns justify-content-end gap-2\"> ");
            htmlBuilder.AppendLine($"<div> ");
            htmlBuilder.AppendLine("<button mat-button [disabled]=\"!" + form_group_name + ".valid\" (click)=\"submit()\" class=\"btn save-btn \" >{{ isNew ? 'Add' : 'Update' }}</button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"<div> ");
            htmlBuilder.AppendLine($"<button mat-button (click)=\"close_dialog(null)\" class=\"btn close-btn \">Cancel</button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($" </mat-dialog-actions> ");
            htmlBuilder.AppendLine($"</form> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"</div> ");
            return new Tuple<string, List<table_design>>(htmlBuilder.ToString(), sort_meta_data);



        }

        public static Tuple<string, List<table_design>> generate_add_edit_component_html(string entity_name, string title, string primary_key_column, DataTable control_config, string schema_name)
        {
            var sort_meta_data = new List<table_design>();
            string form_group_name = $"{entity_name}Form";
            StringBuilder htmlBuilder = new StringBuilder();
            string form_group_key = "formGroup";
            htmlBuilder.AppendLine($"<div id=\"{entity_name}_entry_container\" tabindex=\"1\"  style=\"border:none;outline:none\"> ");
            htmlBuilder.AppendLine($"<div class=\"container-fluid common-master-form\"> ");
            htmlBuilder.AppendLine($"<div mat-dialog-title class=\"d-flex justify-content-between align-items-center\"> ");
            htmlBuilder.AppendLine("<h3>{{_title}}</h3>");
            htmlBuilder.AppendLine($"<button class=\"btn close-btn\" (click)=\"close_dialog(null)\"><i class=\"bi bi-x-lg\"></i></button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"<form [formGroup]=\"{entity_name}Form\" *ngIf=\"{entity_name}Form\"> ");
            htmlBuilder.AppendLine($"<mat-dialog-content class=\"mat-typography\"> ");
            htmlBuilder.AppendLine($"<div fxLayout=\"row wrap\" fxLayoutAlign=\"\" fxLayoutGap=\"10px grid\" class=\"mb-0\"> ");

            var table_meta_data = SQLScriptGenerator.GetTable_Designs(entity_name, schema_name);
            var sys_column_lst = SQLScriptGenerator.sys_column_lst;
            sys_column_lst.Add("is_locked");

            sort_meta_data = table_meta_data.Where(g => !sys_column_lst.Contains(g._column_name.ToLower().Trim())).ToList();
            foreach (var item in sort_meta_data)
            {
                var _column_name = item._column_name;
                var _preferred_controls = item._preferred_controls;
                var caption = item._preferred_controls;
                var IsMandetory = !item.IsNullable;
                string sp_select_search = string.Empty;
                if (primary_key_column != _column_name)
                {

                    var getuserData = control_config.AsEnumerable().Where(g => Convert.ToString(g["Column Name"]).ToLower().Trim() == _column_name).ToArray();
                    if (getuserData != null && getuserData.Length > 0)
                    {
                        _preferred_controls = Convert.ToString(getuserData[0]["Control Name"]);
                        caption = Convert.ToString(getuserData[0]["Caption"]);
                        IsMandetory = Convert.ToString(getuserData[0]["Must"]).ToLower().Trim().Equals("true");
                        if (control_config.Columns.Contains("sp_select_search"))
                        {
                            sp_select_search = Convert.ToString(getuserData[0]["sp_select_search"]);
                        }
                    }
                    htmlBuilder.AppendLine($"<div fxFlex=\"50\" fxFlex.gt-sm=\"50\" >");
                    //htmlBuilder.AppendLine($"<label>{caption}</label> ");
                    if (_preferred_controls == "txtbox")
                    {
                        htmlBuilder.AppendLine($"<smart-txtbox  ");
                        htmlBuilder.AppendLine($"class=\"w-100\" [{form_group_key}]=\"{form_group_name}\" Caption=\"{caption}\" ");
                        htmlBuilder.AppendLine($"type=\"text\" field=\"{item._column_name}\" ");
                        //htmlBuilder.AppendLine($"(copy)=\"true\" (paste)=\"true\" autocomplete=\"disabled\" ");
                        htmlBuilder.AppendLine($"[IsReq]=\"{Convert.ToString(IsMandetory).ToLowerInvariant()}\" ");
                        if (false)
                        {
                            htmlBuilder.AppendLine($"[is_numeric_with_minus]=\"custom_fields[i].datatype == 2\" ");
                            htmlBuilder.AppendLine($"[is_bit]=\"custom_fields[i].datatype == 3\" ");
                            htmlBuilder.AppendLine($"[maxlen]=\"custom_fields[i].text_max_length\" ");
                            htmlBuilder.AppendLine($"[minlen]=\"custom_fields[i].text_min_length\" ");
                            htmlBuilder.AppendLine($"[maxnumvalue]=\"custom_fields[i].num_max_value\" ");
                            htmlBuilder.AppendLine($"[digitafterdecimal]=\"custom_fields[i].digits_after_decimal\" ");
                            htmlBuilder.AppendLine($"(keypress)=\"text_value_key_up_handler($event)\" ");
                            htmlBuilder.AppendLine($"(keydown)=\"text_value_key_down_handler($event,custom_fields[i].text_case)\" ");
                            htmlBuilder.AppendLine($"[block_spl_char]=\"custom_fields[i].text_type==1\" ");
                            htmlBuilder.AppendLine($"[is_OnlyAlphabet]=\"custom_fields[i].text_type==2\" ");
                        }
                        htmlBuilder.AppendLine($"></smart-txtbox> ");
                    }
                    if (_preferred_controls == "date-picker")
                    {
                        htmlBuilder.AppendLine($"<smart-date-picker  [{form_group_key}]=\"{form_group_name}\" ");
                        htmlBuilder.AppendLine($"field=\"{item._column_name}\" [IsReq]=\"custom_fields[i].IsMandetory\" caption=\"{caption}\"> ");
                        htmlBuilder.AppendLine($"</smart-date-picker> ");
                    }
                    if (_preferred_controls == "select")
                    {
                        htmlBuilder.AppendLine($"<smart-select  class=\"smart-componenet w-100\" ");
                        htmlBuilder.AppendLine($"[{form_group_key}]=\"{form_group_name}\" field=\"{item._column_name}\" ");
                        htmlBuilder.AppendLine($"[IsReq]=\"custom_fields[i].IsMandetory\" Caption=\"{caption}\" ");
                        htmlBuilder.AppendLine($"[options]=\"custom_fields[i].options\" ");
                        htmlBuilder.AppendLine($"[multi_select]=\"false\"></smart-select> ");
                    }
                    if (_preferred_controls == "switch" || _column_name == "is_locked")
                    {
                        htmlBuilder.AppendLine($"<label class=\" me-2 w-auto\">Locked</label> ");
                        htmlBuilder.AppendLine($"<smart-switch [{form_group_key}]=\"{form_group_name}\" ");
                        htmlBuilder.AppendLine($"field=\"{caption}\"> ");
                        htmlBuilder.AppendLine($"</smart-switch> ");
                    }
                    if (_preferred_controls == "smart-select-auto")
                    {
                        htmlBuilder.AppendLine($"<smart-select-autocomplete [form_group]=\"{form_group_name}\" field=\"{item._column_name}\" caption=\"{caption}\" sp_select_search=\"{sp_select_search}\" [is_req]=\"{Convert.ToString(IsMandetory).ToLowerInvariant()}\"  >");
                        htmlBuilder.AppendLine($"</smart-select-autocomplete>");
                    }
                    htmlBuilder.AppendLine($"</div> ");
                }
            }

            if (table_meta_data.Where(g => g._column_name.Equals("is_locked")).ToList() != null)
            {

                htmlBuilder.AppendLine($"<div *ngIf=\"!isNew\" fxFlex=\"100\"  fxFlex.gt-sm=\"50\" > ");
                htmlBuilder.AppendLine($"<label class=\" me-2 w-auto\">Locked</label> ");
                htmlBuilder.AppendLine($"<smart-switch [{form_group_key}]=\"{form_group_name}\" ");
                htmlBuilder.AppendLine($"field=\"is_locked\" > ");
                htmlBuilder.AppendLine($"</smart-switch> ");
                htmlBuilder.AppendLine($"</div> ");
            }
            htmlBuilder.AppendLine($"</div>");
            htmlBuilder.AppendLine($"</mat-dialog-content> ");
            htmlBuilder.AppendLine($"<mat-dialog-actions align=\"end\" class=\"foot-action-btns justify-content-end gap-2\"> ");
            htmlBuilder.AppendLine($"<div> ");
            htmlBuilder.AppendLine("<button mat-button [disabled]=\"!" + form_group_name + ".valid\" (click)=\"submit()\" class=\"btn save-btn \" >{{ isNew ? 'Add' : 'Update' }}</button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"<div> ");
            htmlBuilder.AppendLine($"<button mat-button (click)=\"close_dialog(null)\" class=\"btn close-btn \">Cancel</button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($" </mat-dialog-actions> ");
            htmlBuilder.AppendLine($"</form> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"</div> ");
            return new Tuple<string, List<table_design>>(htmlBuilder.ToString(), sort_meta_data);



        }

        //{entity_name}-add-edit.component.scss
        public static string generate_add_edit_component_scss(string entity_name, string title)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            return htmlBuilder.ToString();
        }

        //{entity_name}-add-edit.component.ts
        public static string generate_add_edit_component_ts(string entity_name, string _edit_component_name, string _entityservice, string primary_key_column, Dictionary<string, string> entityServiceMethod, List<table_design> edit_columninfo)
        {
            string _form_name = $"{entity_name}Form";
            StringBuilder TsBuilder = new StringBuilder();
            TsBuilder.AppendLine("import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';");
            TsBuilder.AppendLine("import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';");
            TsBuilder.AppendLine("import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';");
            TsBuilder.AppendLine("import { " + _entityservice + " } from '../" + entity_name + ".service';");
            TsBuilder.AppendLine("//import { ToastrService } from 'ngx-toastr';");
            TsBuilder.AppendLine("import { DynamicAlertService } from '../../../../shared/components/dynamic-alert/dynamic-alert.service';");
            TsBuilder.AppendLine("import { LoaderService } from '../../../../shared/components/loader/loader.service';");
            TsBuilder.AppendLine("\n");
            TsBuilder.AppendLine("@Component({");
            TsBuilder.AppendLine($"  selector: 'app-{entity_name}-add-edit',");
            TsBuilder.AppendLine($"  templateUrl: './{entity_name}-add-edit.component.html',");
            TsBuilder.AppendLine($"  styleUrls: ['./{entity_name}-add-edit.component.scss']");
            TsBuilder.AppendLine("})");
            TsBuilder.AppendLine("export class " + _edit_component_name + " implements OnInit {");
            //Start Edit Component 
            TsBuilder.AppendLine($"  public {_form_name}: FormGroup;");
            TsBuilder.AppendLine($"  public {_form_name}_backup: FormGroup;");
            TsBuilder.AppendLine("  public _title: string = \"\";");
            TsBuilder.AppendLine("  public errorMessage: string = \"\";");
            TsBuilder.AppendLine("\n");
            TsBuilder.AppendLine($"  public {primary_key_column}: any;");
            TsBuilder.AppendLine("  public isNew: boolean = true;");
            TsBuilder.AppendLine("\n");
            TsBuilder.AppendLine("  constructor(");
            TsBuilder.AppendLine("    private fb: FormBuilder,");
            TsBuilder.AppendLine("    @Inject(MAT_DIALOG_DATA) public data: any,");
            TsBuilder.AppendLine($"    public dialogRef: MatDialogRef<{_edit_component_name}>,");
            TsBuilder.AppendLine($"    private obj_{_entityservice}: {_entityservice},");
            TsBuilder.AppendLine("    //private toastr: ToastrService,");
            TsBuilder.AppendLine("    private m2_alert: DynamicAlertService,");
            TsBuilder.AppendLine("    private loader: LoaderService");
            TsBuilder.AppendLine("  ) { }");
            TsBuilder.AppendLine("\n");

            // ngOnInit 
            TsBuilder.AppendLine("  ngOnInit(): void {");
            TsBuilder.AppendLine("    this.init();");
            TsBuilder.AppendLine("    this.fetch_data();");
            TsBuilder.AppendLine($"    let ele: HTMLElement = document.getElementById('{entity_name}_entry_container');");
            TsBuilder.AppendLine("    ele.focus();");
            TsBuilder.AppendLine("  }");
            TsBuilder.AppendLine("\n");

            // init
            TsBuilder.AppendLine("  init(): void {");
            TsBuilder.AppendLine($"    this.{primary_key_column} = this.data.{primary_key_column} > 0 ? this.data.{primary_key_column} : null;");
            TsBuilder.AppendLine($"    this.isNew = (this.{primary_key_column} == null);");
            TsBuilder.AppendLine("  }");
            TsBuilder.AppendLine("\n");

            // fetch_data
            TsBuilder.AppendLine("  fetch_data() {");
            TsBuilder.AppendLine("this.build_form(null);");
            TsBuilder.AppendLine("    this.obj_" + _entityservice + "." + entityServiceMethod[UtilityScript.read_key] + "({ " + primary_key_column + ": this." + primary_key_column + " })");
            TsBuilder.AppendLine("      .subscribe({");
            TsBuilder.AppendLine("        next: (RtnData) => {");
            TsBuilder.AppendLine("          this._title = '" + entity_name + "';");
            TsBuilder.AppendLine("          var datarow = RtnData.data[0];");
            TsBuilder.AppendLine("          this.build_form(datarow);");
            TsBuilder.AppendLine("        }, error: (err_response) => {");
            TsBuilder.AppendLine("          this.m2_alert.fire({");
            TsBuilder.AppendLine("            title: 'Error',");
            TsBuilder.AppendLine("            text: err_response['error']['message'],");
            TsBuilder.AppendLine("            icon: 'error'");
            TsBuilder.AppendLine("          });");
            TsBuilder.AppendLine("          this.close_dialog(null);");
            TsBuilder.AppendLine("        }");
            TsBuilder.AppendLine("      });");
            TsBuilder.AppendLine("  }");
            TsBuilder.AppendLine("\n");
            //build_form
            TsBuilder.AppendLine("  build_form(rowdata: any) {");
            TsBuilder.AppendLine("    var obj: any = {};");

            TsBuilder.AppendLine($"    this.{_form_name} = this.fb.group(");
            TsBuilder.AppendLine("      {");
            var sys_column_lst = SQLScriptGenerator.sys_column_lst;
            for (int b = 0; b < edit_columninfo.Count; b++)
            {
                if (!sys_column_lst.Contains(edit_columninfo[b]._column_name))
                {
                    TsBuilder.AppendLine($"{edit_columninfo[b]._column_name}: [rowdata != null ? rowdata.{edit_columninfo[b]._column_name} : {(edit_columninfo[b]._column_name.Equals(primary_key_column) ? "0" : "null")}]");
                    if (!(b == (edit_columninfo.Count - 1)))
                    {
                        TsBuilder.Append(",");
                    }
                }
            }

            TsBuilder.AppendLine("      }");
            TsBuilder.AppendLine("    );");
            TsBuilder.AppendLine($"    this.{_form_name}_backup = this.cloneForm(this.{_form_name});");
            TsBuilder.AppendLine("");
            TsBuilder.AppendLine("  }");
            TsBuilder.AppendLine("");

            // To CloneForm Object
            TsBuilder.AppendLine("  cloneForm(originalForm: FormGroup): FormGroup {");
            TsBuilder.AppendLine("    const clonedForm = new FormGroup({});");
            TsBuilder.AppendLine("    Object.keys(originalForm.controls).forEach(controlName => {");
            TsBuilder.AppendLine("      const control = originalForm.controls[controlName];");
            TsBuilder.AppendLine("      clonedForm.addControl(controlName, new FormControl(control.value));");
            TsBuilder.AppendLine("    });");
            TsBuilder.AppendLine("    return clonedForm;");
            TsBuilder.AppendLine("  }");
            TsBuilder.AppendLine("");
            // To Check Custom Validators
            TsBuilder.AppendLine("custom_validators(): string {");
            TsBuilder.AppendLine("let err_message: string = '';");
            TsBuilder.AppendLine("if (err_message) {");
            TsBuilder.AppendLine("  err_message = err_message.substring(0, err_message.length - 1);");
            TsBuilder.AppendLine("}");
            TsBuilder.AppendLine("return err_message;");
            TsBuilder.AppendLine("}");

            // To Submit
            TsBuilder.AppendLine("submit() {");
            TsBuilder.AppendLine("let err = this.custom_validators();");
            TsBuilder.AppendLine("if (err) {");
            TsBuilder.AppendLine("this.m2_alert.fire({");
            TsBuilder.AppendLine("   title: 'Error',");
            TsBuilder.AppendLine("    text: err,");
            TsBuilder.AppendLine("    icon: 'error',");
            TsBuilder.AppendLine("    confirmButtonText: 'ok',");
            TsBuilder.AppendLine("  })");
            TsBuilder.AppendLine("  return;");
            TsBuilder.AppendLine("}");

            TsBuilder.AppendLine("if (this." + _form_name + ".valid) {");
            TsBuilder.AppendLine("  if (this." + primary_key_column + ") {");
            TsBuilder.AppendLine("    this.m2_alert.fire({");
            TsBuilder.AppendLine("      title: 'Are you sure you want to update the data ?',");
            TsBuilder.AppendLine("      text: 'This process is irreversible.',");
            TsBuilder.AppendLine("      icon: 'warning',");
            TsBuilder.AppendLine("     showCancelButton: true,");
            TsBuilder.AppendLine("     confirmButtonText: 'yes',");
            TsBuilder.AppendLine("     cancelButtonText: 'no'");
            TsBuilder.AppendLine("    }).then((r) => {");
            TsBuilder.AppendLine("      if (r.isConfirmed) {");
            TsBuilder.AppendLine("        this.save_entity_data();");
            TsBuilder.AppendLine("      }");
            TsBuilder.AppendLine("    })");
            TsBuilder.AppendLine("  } else {");
            TsBuilder.AppendLine("    this.save_entity_data();");
            TsBuilder.AppendLine("  }");
            TsBuilder.AppendLine("} else {");
            TsBuilder.AppendLine("  this.m2_alert.fire({");
            TsBuilder.AppendLine("    title: 'Error',");
            TsBuilder.AppendLine("    text: 'validation error',");
            TsBuilder.AppendLine("   icon: 'error',");
            TsBuilder.AppendLine("   confirmButtonText: 'ok',");
            TsBuilder.AppendLine("  })");
            TsBuilder.AppendLine("  return;");
            TsBuilder.AppendLine("}");
            TsBuilder.AppendLine("}");

            // To Save Commonmaster_data
            TsBuilder.AppendLine("save_entity_data(): void {");
            TsBuilder.AppendLine("this.loader.open();");
            TsBuilder.AppendLine("let snd_data = this." + _form_name + ".value;");
            TsBuilder.AppendLine($"this.obj_{_entityservice}.{entityServiceMethod[UtilityScript.create_update_key]}(snd_data)");
            TsBuilder.AppendLine("  .subscribe({");
            TsBuilder.AppendLine("    next: (data) => {");
            TsBuilder.AppendLine("    if (data.data[0].update_count > 0)");
            TsBuilder.AppendLine("    {");
            TsBuilder.AppendLine("    //this.toastr.success('record updated successfully!');");
            TsBuilder.AppendLine("    }");
            TsBuilder.AppendLine($"      this.{primary_key_column} = data.data[0].{primary_key_column};");
            TsBuilder.AppendLine($"      snd_data.{primary_key_column} = this.{primary_key_column};");
            TsBuilder.AppendLine("      this.close_dialog(null);");
            TsBuilder.AppendLine("      this.loader.close();");
            TsBuilder.AppendLine("    }, error: (err_response) => {");
            TsBuilder.AppendLine("     this.m2_alert.fire({");
            TsBuilder.AppendLine("        title: 'Error',");
            TsBuilder.AppendLine("        text: err_response[\"error\"][\"message\"],");
            TsBuilder.AppendLine("        icon: \"error\" ");
            TsBuilder.AppendLine("      });");
            TsBuilder.AppendLine("    }");
            TsBuilder.AppendLine("  });");
            TsBuilder.AppendLine("}");

            TsBuilder.AppendLine("close_dialog(obj: any): void {");
            TsBuilder.AppendLine("this.dialogRef.close(obj);");
            TsBuilder.AppendLine("}");


            TsBuilder.AppendLine("}");


            return TsBuilder.ToString();
        }

        //{entity_name}-add-edit.component.spec.ts
        public static string generate_add_edit_component_spec_ts(string entity_name, string component_name, string title, string add_edit_component_name)
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import {ComponentFixture,TestBed}  from '@angular/core/testing';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("import { " + component_name + " }  from './" + entity_name + "-add-edit.component';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("describe('" + component_name + "', () => {");
            tsBuilder.AppendLine("let component: " + component_name + ";");
            tsBuilder.AppendLine("let fixture: ComponentFixture<" + component_name + ">;");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("beforeEach(() => {");
            tsBuilder.AppendLine("TestBed.configureTestingModule({");
            tsBuilder.AppendLine("declarations: [" + component_name + "]");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("fixture = TestBed.createComponent(" + component_name + ");");
            tsBuilder.AppendLine("component = fixture.componentInstance;");
            tsBuilder.AppendLine("fixture.detectChanges();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("it('should create', () => {");
            tsBuilder.AppendLine("expect(component).toBeTruthy();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("});");

            return tsBuilder.ToString();
        }


    }

    public class AngularScriptGenerator1
    {

        public AngularScriptGenerator1() { }



        public void generate_component_listing_html()
        {
            try
            {
                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.AppendLine("<lite-grid [columns]=\"column_config\" [data]=\"data_items\" [rawdata_count]=\"data_count\" [config]=\"grid_config\" (customEvents)=\"custom_events($event)\" (pageChangeEvents)=\"pageChangeHandler($event)\" ></lite-grid>\r\n");
                htmlBuilder.AppendLine("<div class=\" page-header\" >");
                htmlBuilder.AppendLine("<div class=\"row align-items-center\">");
                htmlBuilder.AppendLine("<div class=\"col\">");
                htmlBuilder.AppendLine("<h2>Common Masters List</h2>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<div class=\"d-flex align-items-center gap-2 col-12 col-md-6 col-lg-5 col-xl-5 col-xxl-4\">");
                htmlBuilder.AppendLine("<div class=\"w-50 \">");
                htmlBuilder.AppendLine("<mat-form-field appearance = \"outline\" class=\"w-100\">");
                htmlBuilder.AppendLine("<mat-select[(value)]=\"query.search_column\">");
                htmlBuilder.AppendLine("<mat-option* ngFor = \"let opt of search_options\"[value] = \"opt.name\" >");
                htmlBuilder.AppendLine("{{opt.name}}");
                htmlBuilder.AppendLine("</ mat - option >");
                htmlBuilder.AppendLine("</ mat - select >");
                htmlBuilder.AppendLine("</ mat - form - field >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("<div class= \"input-group search-box shadow-sm w-50\" >");
                htmlBuilder.AppendLine("<input type = \"text\" class= \"form-control\" #search_field id=\"search_field\" placeholder=\"Press Enter to search keyword\" [(ngModel)]=\"searched_text\" (keyup)=\"searchChangeHandler()\" />");
                htmlBuilder.AppendLine("<button class= \"btn\" id = \"btnsearchsubmit\"(click) = \"searchChangeHandler()\" ><i class= \"bi bi-search\" ></ i ></ button >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("<div class= \"body-container\" >");
                htmlBuilder.AppendLine("<div class= \"data-table child-table-row\" * ngIf = \"commonmastertypes_list.length > 0\" >");
                htmlBuilder.AppendLine("<div class= \"table-responsive table_fullHeight\" >");
                htmlBuilder.AppendLine("<table class= \"table table-bordered mb-0\" >");
                htmlBuilder.AppendLine("<thead class= \"sticky-top\" >");
                htmlBuilder.AppendLine("<tr >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MasterName')\"[ngClass] = \"{ 'sorting': isSorting('MasterName'), 'sorting_asc': isSortAsc('MasterName'), 'sorting_desc': isSortDesc('MasterName') }\" ><div class= \"d-flex align-items-center justify-content-between\" ><span > Master Name </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MasterTitle')\"[ngClass] = \"{ 'sorting': isSorting('MasterTitle'), 'sorting_asc': isSortAsc('MasterTitle'), 'sorting_desc': isSortDesc('MasterTitle') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Master Title </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MasterTypeDescription')\"[ngClass] = \"{ 'sorting': isSorting('MasterTypeDescription'), 'sorting_asc': isSortAsc('MasterTypeDescription'), 'sorting_desc': isSortDesc('MasterTypeDescription') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Master Description </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('Locked')\"[ngClass] = \"{ 'sorting': isSorting('Locked'), 'sorting_asc': isSortAsc('Locked'), 'sorting_desc': isSortDesc('Locked') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Locked </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('Heirarchical')\"[ngClass] = \"{ 'sorting': isSorting('Heirarchical'), 'sorting_asc': isSortAsc('Heirarchical'), 'sorting_desc': isSortDesc('Heirarchical') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Has Parent </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('ParentName')\"[ngClass] = \"{ 'sorting': isSorting('ParentName'), 'sorting_asc': isSortAsc('ParentName'), 'sorting_desc': isSortDesc('ParentName') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Parent Name </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('NeedOrdering')\"[ngClass] = \"{ 'sorting': isSorting('NeedOrdering'), 'sorting_asc': isSortAsc('NeedOrdering'), 'sorting_desc': isSortDesc('NeedOrdering') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Need Sequencing </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MenuName')\"[ngClass] = \"{ 'sorting': isSorting('MenuName'), 'sorting_asc': isSortAsc('MenuName'), 'sorting_desc': isSortDesc('MenuName') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Menu Name </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('BeforeSaveSp')\"[ngClass] = \"{ 'sorting': isSorting('BeforeSaveSp'), 'sorting_asc': isSortAsc('BeforeSaveSp'), 'sorting_desc': isSortDesc('BeforeSaveSp') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Before Save Sp</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('AfterSaveSp')\"[ngClass] = \"{ 'sorting': isSorting('AfterSaveSp'), 'sorting_asc': isSortAsc('AfterSaveSp'), 'sorting_desc': isSortDesc('AfterSaveSp') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > After Save Sp</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('CreatedBy')\"[ngClass] = \"{ 'sorting': isSorting('CreatedBy'), 'sorting_asc': isSortAsc('CreatedBy'), 'sorting_desc': isSortDesc('CreatedBy') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Created By </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('CreatedDate')\"[ngClass] = \"{ 'sorting': isSorting('CreatedDate'), 'sorting_asc': isSortAsc('CreatedDate'), 'sorting_desc': isSortDesc('CreatedDate') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Created On </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('UpdatedBy')\"[ngClass] = \"{ 'sorting': isSorting('UpdatedBy'), 'sorting_asc': isSortAsc('UpdatedBy'), 'sorting_desc': isSortDesc('UpdatedBy') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Updated By </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('UpdatedDate')\"[ngClass] = \"{ 'sorting': isSorting('UpdatedDate'), 'sorting_asc': isSortAsc('UpdatedDate'), 'sorting_desc': isSortDesc('UpdatedDate') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Updated On </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('RecordCount')\"[ngClass] = \"{ 'sorting': isSorting('RecordCount'), 'sorting_asc': isSortAsc('RecordCount'), 'sorting_desc': isSortDesc('RecordCount') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Record Count </ span > <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MailToEmployees')\"[ngClass] = \"{ 'sorting': isSorting('MailToEmployees'), 'sorting_asc': isSortAsc('MailToEmployees'), 'sorting_desc': isSortDesc('MailToEmployees') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Mail To Employees</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th (click) = \"sortClick('MailToOthers')\"[ngClass] = \"{ 'sorting': isSorting('MailToOthers'), 'sorting_asc': isSortAsc('MailToOthers'), 'sorting_desc': isSortDesc('MailToOthers') }\" > <div class= \"d-flex align-items-center justify-content-between\" ><span > Mail To Others</span> <div class= \"assending-ic\" ><i class= \"bi bi-caret-up-fill\" ></ i > <i class= \"bi bi-caret-down-fill\" ></ i ></ div ></ div ></ th >");
                htmlBuilder.AppendLine("<th class= \"action-btn\" > Action </ th >");
                htmlBuilder.AppendLine("</ tr >");
                htmlBuilder.AppendLine("</ thead >");
                htmlBuilder.AppendLine("<tbody * ngFor = \"let item of commonmastertypes_list\" >");
                htmlBuilder.AppendLine("<tr style = \"cursor:pointer\"(click) = \"show_custom_fields_of_selected_masters(item)\"(dblclick) = \"edit_common_master(item.code)\" >");
                htmlBuilder.AppendLine("<td><p >{ { item.MasterName} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MasterTitle} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MasterTypeDescription} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.Locked} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.Heirarchical} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.ParentName} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.NeedOrdering} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MenuName} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.BeforeSaveSp} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.AfterSaveSp} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.CreatedBy} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.CreatedDate | date:'dd/MM/yyyy'} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.UpdatedBy} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.UpdatedDate | date:'dd/MM/yyyy'} }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.RecordCount } }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MailToEmployees } }</ p ></ td >");
                htmlBuilder.AppendLine("<td ><p >{ { item.MailToOthers } }</ p ></ td >");
                htmlBuilder.AppendLine("<td class= \"action-btn\" >");
                htmlBuilder.AppendLine("<button type = \"button\"(click) = \"edit_common_master(item.code)\" class= \"btn edit\" ><i class= \"bi bi-pencil-square\" ></ i ></ button >");
                htmlBuilder.AppendLine("<button type = \"button\"(click) = \"delete_common_master(item.code)\" class= \"btn delete\" ><i class= \"bi bi-trash\" ></ i ></ button >");
                htmlBuilder.AppendLine("</ td >");
                htmlBuilder.AppendLine("</ tr >");

                htmlBuilder.AppendLine("</ tbody>");
                htmlBuilder.AppendLine("</table>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<div class= \"table-footer page-content-head d-flex align-items-center justify-content-end mt-2\" >");
                htmlBuilder.AppendLine("<mat - paginator #master_paginator");


                htmlBuilder.AppendLine("[length] = \"masters_count\"");
                htmlBuilder.AppendLine("[pageSize] = \"1000\"");
                htmlBuilder.AppendLine("[pageIndex] = \"0\"");
                htmlBuilder.AppendLine("[pageSizeOptions] = \"[100, 300, 600, 1000]\"");
                htmlBuilder.AppendLine("aria - label = \"Select page\"(page) = \"pageChangeHandler($event)\" >");
                htmlBuilder.AppendLine("</ mat - paginator >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("</ div >");
                htmlBuilder.AppendLine("<a class= \"btn add-btn shadow\"(click) = \"add_button_click()\" cdkDragBoundary = \".page-body\" cdkDrag ><i class= \"bi bi-plus\" ></ i ></ a >");
                htmlBuilder.AppendLine("</ div >");




            }
            catch (Exception)
            {

                throw;
            }

        }

        public void generate_component_listing_ts()
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { AfterViewInit, Component, ElementRef, HostListener, OnDestroy, OnInit, ViewChild }  from '@angular/core';");
            tsBuilder.AppendLine("import { QuickMasterService } from './quick-master.service';");
            tsBuilder.AppendLine("import { NavigationStart, Router } from '@angular/router';");
            tsBuilder.AppendLine("import { MatDialog } from '@angular/material/dialog';");
            tsBuilder.AppendLine("import { QuickMasterEntryComponent } from '../quick-master-entry/quick-master-entry.component';");
            tsBuilder.AppendLine("import { ToastrService } from 'ngx-toastr';");
            tsBuilder.AppendLine("import { MatPaginator } from '@angular/material/paginator';");
            tsBuilder.AppendLine("import { DynamicAlertService } from '../../../shared/components/dynamic-alert/dynamic-alert.service';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("@Component({");
            tsBuilder.AppendLine("  selector: 'app-quickmaster',");
            tsBuilder.AppendLine("  templateUrl: './quickmaster.component.html',");
            tsBuilder.AppendLine("  styleUrls: ['./quickmaster.component.scss']");
            tsBuilder.AppendLine("})");
            tsBuilder.AppendLine("export class QuickmasterComponent implements OnInit, AfterViewInit {");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public commonmastertypes_list: any = [];");
            tsBuilder.AppendLine("  public custom_fields_of_commonmaster_list: any = [];");
            tsBuilder.AppendLine("  public custom_field_parent_code: any = '';");
            tsBuilder.AppendLine("  public masters_count: number;");
            tsBuilder.AppendLine("  public query: any;");
            tsBuilder.AppendLine("  public selected_master: string;");
            tsBuilder.AppendLine("  public searched_text: string = '';");
            tsBuilder.AppendLine("  public search_options: any[] = [];");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  @ViewChild('search_field') myInputField: ElementRef;");
            tsBuilder.AppendLine("  @ViewChild('master_paginator') paginator: MatPaginator;");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  constructor(private _quickmasterService: QuickMasterService, private router: Router,");
            tsBuilder.AppendLine("    private dialog: MatDialog, private toastr: ToastrService,");
            tsBuilder.AppendLine("    private m2_alert: DynamicAlertService, private elementRef: ElementRef) {");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  ngOnInit(): void {");
            tsBuilder.AppendLine("    this.oninit();");
            tsBuilder.AppendLine("    this.get_search_options();");
            tsBuilder.AppendLine("    this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    let ele : HTMLElement = document.getElementById('qm_container');");
            tsBuilder.AppendLine("    ele.focus();");
            tsBuilder.AppendLine("    /*console.log((ele[0] as HTMLElement).focus())*/");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  ngAfterViewInit(): void {");
            tsBuilder.AppendLine("    setTimeout(() => {");
            tsBuilder.AppendLine("      var elem = document.getElementById('search_field');");
            tsBuilder.AppendLine("      elem.focus();");
            tsBuilder.AppendLine("    }, 300);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  oninit(): void {");
            tsBuilder.AppendLine("    this.query = {");
            tsBuilder.AppendLine("      page_index: 0,");
            tsBuilder.AppendLine("      page_size: 1000,");
            tsBuilder.AppendLine("      search_text: '',");
            tsBuilder.AppendLine("      search_column: 'MasterName',");
            tsBuilder.AppendLine("      sort_col_name: 'MasterName',");
            tsBuilder.AppendLine("      sort_type: 'desc'");
            tsBuilder.AppendLine("    };");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  get_search_options(): void {");
            tsBuilder.AppendLine("    this._quickmasterService.get_common_master_type_info()");
            tsBuilder.AppendLine("      .subscribe(data => {");
            tsBuilder.AppendLine("        this.search_options = data;");
            tsBuilder.AppendLine("      })");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  get_allcommonmastertypes_list(): void {");
            tsBuilder.AppendLine("    this._quickmasterService.get_commonmastertypes_list(this.query)");
            tsBuilder.AppendLine("      .subscribe(data => {");
            tsBuilder.AppendLine("        this.commonmastertypes_list = data.data.CommonMasterTypes;");
            tsBuilder.AppendLine("        this.masters_count = data.MasterCount[0].totalRowsCount;");
            tsBuilder.AppendLine("      })");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  show_custom_fields_of_selected_masters(item: any): void {");
            tsBuilder.AppendLine("    this.commonmastertypes_list.forEach((obj: any) => {");
            tsBuilder.AppendLine("      if (obj.code != item.code) {");
            tsBuilder.AppendLine("        obj.ShowFieldConfig = false;");
            tsBuilder.AppendLine("      }");
            tsBuilder.AppendLine("    });");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("    if (item.ShowFieldConfig) {");
            tsBuilder.AppendLine("      item.ShowFieldConfig = false;");
            tsBuilder.AppendLine("    } else {");
            tsBuilder.AppendLine("      item.ShowFieldConfig = true;");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("    //if (this.custom_field_parent_code == _code) {");
            tsBuilder.AppendLine("    //  this.custom_field_parent_code = '';");
            tsBuilder.AppendLine("    //} else {");
            tsBuilder.AppendLine("    //  this._quickmasterService.get_cmnMst_CustomFieldConfig_list({ CmnMst_Code: _code })");
            tsBuilder.AppendLine("    //    .subscribe(data => {");
            tsBuilder.AppendLine("    //      this.selected_master = name;");
            tsBuilder.AppendLine("    //      this.custom_fields_of_commonmaster_list = data;");
            tsBuilder.AppendLine("    //      this.custom_field_parent_code = _code;");
            tsBuilder.AppendLine("    //    })");
            tsBuilder.AppendLine("    //}");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  add_button_click(): void {");
            tsBuilder.AppendLine("    this.open_entry_page(null);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  edit_common_master(code: number): void {");
            tsBuilder.AppendLine("    this.open_entry_page(code);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  delete_common_master(code: number): void {");
            tsBuilder.AppendLine("    this.m2_alert.fire({");
            tsBuilder.AppendLine("      title: 'Are you sure you want to delete this common master ?',");
            tsBuilder.AppendLine("      text: 'The data present for this master will also be deleted.',");
            tsBuilder.AppendLine("      icon: 'warning',");
            tsBuilder.AppendLine("      showCancelButton: true,");
            tsBuilder.AppendLine("      confirmButtonText: 'yes',");
            tsBuilder.AppendLine("      cancelButtonText: 'no'");
            tsBuilder.AppendLine("    }).then((r) => {");
            tsBuilder.AppendLine("      if (r.isConfirmed) {");
            tsBuilder.AppendLine("        this._quickmasterService.delete_common_master_type({ code: code }).subscribe({");
            tsBuilder.AppendLine("          next: (data) => {");
            tsBuilder.AppendLine("            this.toastr.success(data[0]['message']);");
            tsBuilder.AppendLine("            this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("            this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("          },");
            tsBuilder.AppendLine("          error: (err_response) => {");
            tsBuilder.AppendLine("            this.toastr.error(err_response['error']['message']);");
            tsBuilder.AppendLine("          }");
            tsBuilder.AppendLine("        })");
            tsBuilder.AppendLine("      }");
            tsBuilder.AppendLine("    })");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  open_entry_page(code: number): void {");
            tsBuilder.AppendLine("    let dialogRef = this.dialog.open(QuickMasterEntryComponent, { data: { code: code }, disableClose: true, panelClass: 'full-width' });");
            tsBuilder.AppendLine("    dialogRef.afterClosed().subscribe(() => {");
            tsBuilder.AppendLine("      this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("      this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("    });");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  handleKeyPlus(event: KeyboardEvent) {");
            tsBuilder.AppendLine("    if (event.key == '+') {");
            tsBuilder.AppendLine("      this.add_button_click();");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("    if (event.ctrlKey && event.shiftKey && event.key === 'F') {");
            tsBuilder.AppendLine("      event.preventDefault();");
            tsBuilder.AppendLine("      this.myInputField.nativeElement.focus();");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  //handle_row_keydown(event: KeyboardEvent, code: any) {");
            tsBuilder.AppendLine("  //  if (event.ctrlKey && event.key === 'E') {");
            tsBuilder.AppendLine("  //    event.preventDefault();");
            tsBuilder.AppendLine("  //    event.stopPropagation();");
            tsBuilder.AppendLine("  //    this.edit_common_master(code);");
            tsBuilder.AppendLine("  //  }");
            tsBuilder.AppendLine("  //}");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  pageChangeHandler(event: any): void {");
            tsBuilder.AppendLine("    this.query.page_index = event.pageIndex;");
            tsBuilder.AppendLine("    this.query.page_size = event.pageSize;");
            tsBuilder.AppendLine("    this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  searchChangeHandler(): void {");
            tsBuilder.AppendLine("    this.query.search_text = this.searched_text;");
            tsBuilder.AppendLine("    this.query.page_index = 0;");
            tsBuilder.AppendLine("    if (this.paginator) {");
            tsBuilder.AppendLine("      this.paginator.pageIndex = 0;");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("    this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    this.custom_fields_of_commonmaster_list = [];");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  sortClick(headerName: string) {");
            tsBuilder.AppendLine("    if (headerName) {");
            tsBuilder.AppendLine("      if (this.query.sort_col_name === headerName) {");
            tsBuilder.AppendLine("        //this.query.IsFreeSearch = false;");
            tsBuilder.AppendLine("        this.query.sort_type = this.query.sort_type === 'asc' ? 'desc' : 'asc';");
            tsBuilder.AppendLine("      }");
            tsBuilder.AppendLine("      this.query.sort_col_name = headerName;");
            tsBuilder.AppendLine("      //this.query.search_column = headerName;");
            tsBuilder.AppendLine("      this.get_allcommonmastertypes_list();");
            tsBuilder.AppendLine("    }");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  isSorting(name: string) {");
            tsBuilder.AppendLine("    return this.query.sort_col_name !== name && name !== '';");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  isSortAsc(name: string) {");
            tsBuilder.AppendLine("    return this.query.sort_col_name === name && this.query.sort_type === 'asc';");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  isSortDesc(name: string) {");
            tsBuilder.AppendLine("    return this.query.sort_col_name === name && this.query.sort_type === 'desc';");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("}");



        }

        public void generate_component_service_ts()
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { Injectable } from '@angular/core';");
            tsBuilder.AppendLine("import { Observable, of } from 'rxjs';");
            tsBuilder.AppendLine("import { HttpClient } from '@angular/common/http';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("import { environment } from '../../../../environments/environment';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("@Injectable({");
            tsBuilder.AppendLine("  providedIn: 'root'");
            tsBuilder.AppendLine("})");
            tsBuilder.AppendLine("export class QuickMasterService {");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public controller_name: string = 'quickmaster';");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  constructor(public http: HttpClient) { }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region to get CommonMasterTypes list */");
            tsBuilder.AppendLine("  //public get_commonmastertypes_list(): Observable<any> {");
            tsBuilder.AppendLine("  //  return this.http.post(environment.apiUrl + this.controller_name + '/get_commonmastertypes_list', null);");
            tsBuilder.AppendLine("  //}");
            tsBuilder.AppendLine("  public get_commonmastertypes_list(query: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/get_commonmastertypes_list', query);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion CommonMasterTypes */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region To Remove CommonMasterTypes */");
            tsBuilder.AppendLine("  public remove_commonMasterTypes(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/remove_commonMasterTypes', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion To Remove CommonMasterTypes */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region to get CustomFieldConfig List */");
            tsBuilder.AppendLine("  public get_cmnMst_CustomFieldConfig_list(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/get_cmnMst_CustomFieldConfig_list', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion to get CustomFieldConfig List */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region  To Add Or Update  CustomFieldConfig */");
            tsBuilder.AppendLine("  public addorupdate_cmnMst_CustomFieldConfig(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/addorupdate_cmnMst_CustomFieldConfig', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion  To Add Or Update  CustomFieldConfig */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  /* #region To Remove CustomFieldConfig */");
            tsBuilder.AppendLine("  public remove_cmnMst_CustomFieldConfig(common_master_obj: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/remove_cmnMst_CustomFieldConfig', common_master_obj);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("  /* #endregion To Remove CommonMasterTypes */");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public delete_common_master_type(query: any): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/delete_common_master_type', query);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("  public get_common_master_type_info(): Observable<any> {");
            tsBuilder.AppendLine("    return this.http.post(environment.apiUrl + this.controller_name + '/get_common_master_type_info', null);");
            tsBuilder.AppendLine("  }");
            tsBuilder.AppendLine("");
            tsBuilder.AppendLine("}");

        }


        /// <summary>
        /// Step 1 Create Service ts
        /// </summary>
        /// <param name="entity_name"></param>
        /// <param name="_entityservice"></param>
        /// <returns></returns>


        public static Dictionary<string, string> SystemReserveKeyValue()
        {
            var result = new Dictionary<string, string>();
            result.Add("created_on", "Created On");
            result.Add("created_by", "Created By");
            result.Add("updated_on", "Updated On");
            result.Add("updated_by", "Updated By");
            result.Add("update_count", "Update Count");
            result.Add("deleted_on", "Deleted On");
            result.Add("deleted_by", "Deleted By");
            result.Add("is_locked", "Is Active");
            return result;
        }

        // {entity_name}.service.ts
        public static Tuple<string, string, Dictionary<string, string>> generate_component_service_ts(string entity_name, string controller_name, string _entityservice, Dictionary<string, string> dic_apiendpoints)
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { Injectable } from '@angular/core';");
            tsBuilder.AppendLine("import { Observable } from 'rxjs';");
            tsBuilder.AppendLine("import { HttpClient } from '@angular/common/http';");
            tsBuilder.AppendLine("import { AppSettings } from '../../../appSettings';");
            tsBuilder.AppendLine("import { Router } from '@angular/router';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("@Injectable({");
            tsBuilder.AppendLine("providedIn: 'root'");
            tsBuilder.AppendLine("})");

            tsBuilder.AppendLine($"export class {_entityservice}");
            tsBuilder.AppendLine("{");

            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("public controller_name: string = \"" + controller_name + "\";");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("constructor(public http: HttpClient, private router: Router, private app_settings: AppSettings) { }");
            tsBuilder.AppendLine("\n");

            // For To Get Data For Listing View
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.view_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/" + dic_apiendpoints[UtilityScript.view_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            // For Create & Update
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.create_update_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/" + dic_apiendpoints[UtilityScript.create_update_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            // For Get Perticular Record
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.read_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/" + dic_apiendpoints[UtilityScript.read_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            // For Delete Record
            tsBuilder.AppendLine("public " + dic_apiendpoints[UtilityScript.delete_key] + "(query: any) :  Observable<any> {");
            tsBuilder.AppendLine("return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/" + dic_apiendpoints[UtilityScript.delete_key] + "', query);");
            tsBuilder.AppendLine("}");
            tsBuilder.AppendLine("\n");

            tsBuilder.AppendLine("}");
            return new Tuple<string, string, Dictionary<string, string>>(_entityservice, tsBuilder.ToString(), dic_apiendpoints);
        }

        // {entity_name}.service.spec.ts
        public static string generate_component_service_spec_ts(string entity_name, string _entityservice)
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { TestBed }  from '@angular/core/testing';");
            tsBuilder.AppendLine("import { " + _entityservice + " } from './" + entity_name.ToLower() + ".service';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("describe('" + _entityservice + "', () => {");
            tsBuilder.AppendLine("  let service: " + _entityservice + ";");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("beforeEach(() => {");
            tsBuilder.AppendLine("TestBed.configureTestingModule({ });");
            tsBuilder.AppendLine("service = TestBed.inject(" + _entityservice + ");");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("it('should be created', () => {");
            tsBuilder.AppendLine("expect(service).toBeTruthy();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("});");

            return tsBuilder.ToString();
        }


        /// <summary>
        /// To Generate List Component
        /// </summary>
        /// <param name="entity_name"></param>
        /// <param name="title"></param>
        /// <returns></returns>

        // {entity_name}.component.html
        public static string generate_list_component_html(string entity_name, string title)
        {


            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine($"<lite-grid [columns]=\"column_config\" [data]=\"data_items\" [rawdata_count]=\"data_count\" [config]=\"grid_config\" (customEvents)=\"custom_events($event)\" (pageChangeEvents)=\"pageChangeHandler($event)\" ></lite-grid>\r\n");

            return htmlBuilder.ToString();



        }

        // {entity_name}.component.scss
        public static string generate_list_component_scss(string entity_name, string title)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            return htmlBuilder.ToString();
        }


        // {entity_name}.component.ts
        public static Tuple<string, string> generate_list_component_ts(string entity_name, string title, string _entityservice, string primary_key_column, Dictionary<string, string> entityServiceMethod, string addeditcomponent_name, DataTable control_config)
        {
            string _entityname = UtilityScript.ConvertToTitleCase(entity_name);
            string add_edit_component = addeditcomponent_name;
            string list_component_name = $"{UtilityScript.ConvertToTitleCase(entity_name)}Component";
            StringBuilder tsBuilder = new StringBuilder();

            tsBuilder.AppendLine("import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';");
            tsBuilder.AppendLine("import { MatDialog } from '@angular/material/dialog';");
            tsBuilder.AppendLine("import { ActivatedRoute, Router } from '@angular/router';");
            tsBuilder.AppendLine("import { CommonAlertsComponent } from '../../../shared/utils/alerts/common-alerts/common-alerts.component';");
            tsBuilder.AppendLine("import { AlertService } from '../../../shared/utils/alerts/alert-service.service';");
            tsBuilder.AppendLine("import { CommonService } from '../../../shared/services/common.service';");
            tsBuilder.AppendLine("import { MatPaginator } from '@angular/material/paginator';");
            tsBuilder.AppendLine("import { AlertType } from '../../../shared/utils/alerts/alert-types';");
            string add_edit_component_name = _entityname + "AddEditComponent";
            tsBuilder.AppendLine("import { " + add_edit_component_name + "}  from './" + add_edit_component + "/" + add_edit_component + ".component';");
            tsBuilder.AppendLine("import { MasterService } from '../master.service';");
            tsBuilder.AppendLine("import { NotificationService } from '../../../shared/services/notification.service';");
            tsBuilder.AppendLine("import { DynamicAlertService } from '../../../shared/common/dynamic-alert/dynamic-alert.service';");

            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("@Component({");
            tsBuilder.AppendLine($"selector: 'app-{entity_name}',");
            tsBuilder.AppendLine($"standalone: false,");
            tsBuilder.AppendLine($"templateUrl: './{entity_name}.component.html',");
            tsBuilder.AppendLine($"styleUrls: ['./{entity_name}.component.scss']");
            tsBuilder.AppendLine("})");
            tsBuilder.AppendLine("export class " + list_component_name + " implements OnInit, AfterViewInit {");
            // Component Start
            tsBuilder.AppendLine("public _headertitle: string = 'Master';");
            tsBuilder.AppendLine("public column_config: any[] = []; ");
            tsBuilder.AppendLine("public data_items: any[] = []; ");
            tsBuilder.AppendLine("public data_count: number = 0; ");
            tsBuilder.AppendLine("public entity_code: number = 0; ");
            tsBuilder.AppendLine("public searched_text: string = ''; ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("public query: any; ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("@ViewChild('master_paginator') paginator!: MatPaginator; ");
            tsBuilder.AppendLine("@ViewChild('search_qmd_field') myInputField!: ElementRef; ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("grid_config: any = { ");
            tsBuilder.AppendLine("title:'<h5 class=\"mb-0\">" + entity_name + "</h5>', ");
            tsBuilder.AppendLine("grid_name: '" + entity_name + "', ");
            tsBuilder.AppendLine("is_addbtn: false, ");
            tsBuilder.AppendLine("full_width: true, ");
            tsBuilder.AppendLine("grid_class: '', ");
            tsBuilder.AppendLine("data_count:1000 ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("table_info: any = { ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("tablename: '[dbo]." + entity_name + " ', ");
            tsBuilder.AppendLine("primarykeycolumn: '" + primary_key_column + "', ");
            tsBuilder.AppendLine("primarykeyvalue: '0', ");
            tsBuilder.AppendLine("fieldcolumn: 'is_locked', ");
            tsBuilder.AppendLine("fieldvalue: '0' ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("constructor( ");
            tsBuilder.AppendLine("private _dialog: MatDialog, ");
            tsBuilder.AppendLine("private _router: Router, ");
            tsBuilder.AppendLine("private _alertservice: AlertService, ");
            tsBuilder.AppendLine("private _common_service: CommonService, ");
            tsBuilder.AppendLine("private _masterserviceobj: MasterService, ");
            tsBuilder.AppendLine("private _dynamicalertservice: DynamicAlertService, ");
            tsBuilder.AppendLine("private _notificationservice: NotificationService, ");
            tsBuilder.AppendLine(") { } ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("ngOnInit(): void { ");
            tsBuilder.AppendLine("this.init(); ");
            tsBuilder.AppendLine("this.get_data(); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("ngAfterViewInit(): void { ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("public get_data() ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("this.get_master_data(); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("public init() ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("this.searched_text = ''; ");
            tsBuilder.AppendLine("this.query = { ");
            tsBuilder.AppendLine("page_number: 1, ");
            tsBuilder.AppendLine("page_size: 20, ");
            tsBuilder.AppendLine("search_column: '" + primary_key_column + "', ");
            tsBuilder.AppendLine("search_text: '', ");
            tsBuilder.AppendLine("sort_col_name: '" + primary_key_column + "', ");
            tsBuilder.AppendLine("sort_type: 'Desc' ");
            tsBuilder.AppendLine("}; ");


            tsBuilder.AppendLine("this.data_items = []; ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("public get_master_data(): void { ");
            tsBuilder.AppendLine($"this._masterserviceobj.{entityServiceMethod[UtilityScript.view_key]}(this.query) ");
            tsBuilder.AppendLine(".subscribe({ ");
            tsBuilder.AppendLine("next: (RtnData) => { ");
            tsBuilder.AppendLine("this.column_config = RtnData.Data[0].Table; ");
            tsBuilder.AppendLine("this.data_items = RtnData.Data[1].Table1; ");
            tsBuilder.AppendLine("this.data_count = RtnData.DataCount; ");
            tsBuilder.AppendLine("}, ");
            tsBuilder.AppendLine("error: (err_response) => { ");
            tsBuilder.AppendLine("this._alertservice.showAlert(err_response[\"error\"][\"message\"], AlertType.BG_WARNING, false); ");
            tsBuilder.AppendLine("this._router.navigate(['/home']); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("public add_edit_record(_entity_code: string): void { ");
            tsBuilder.AppendLine("let dialogRef = this._dialog.open(" + add_edit_component_name + ", { data: { entity_code: _entity_code }, panelClass: 'mid-width' }); ");
            tsBuilder.AppendLine("dialogRef.afterClosed().subscribe((res) => { ");
            tsBuilder.AppendLine("this.get_master_data(); ");
            tsBuilder.AppendLine("}); ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("pageChangeHandler(event: any): void { ");
            tsBuilder.AppendLine("console.log(event); ");
            tsBuilder.AppendLine("this.query.page_size = event.pageSize; ");
            tsBuilder.AppendLine("this.query.page_number = event.pageIndex + 1; ");
            tsBuilder.AppendLine("this.data_count = event.length; ");
            tsBuilder.AppendLine("this.get_master_data(); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("typingTimer: any; ");
            tsBuilder.AppendLine("doneTypingInterval: number = 200; // 200 milisecond ");
            tsBuilder.AppendLine("timerActive: boolean = false; ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("searchChangeHandler(): void { ");
            tsBuilder.AppendLine("clearTimeout(this.typingTimer); ");
            tsBuilder.AppendLine("if (!this.timerActive) ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("this.typingTimer = setTimeout(() => { ");
            tsBuilder.AppendLine("this.query.search_text = this.searched_text; ");
            tsBuilder.AppendLine("// this.query.page_index = this.paginator.pageIndex = 0; ");
            tsBuilder.AppendLine("this.get_master_data(); ");
            tsBuilder.AppendLine("}, this.doneTypingInterval); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("public delete_record(_code: string): void { ");
            tsBuilder.AppendLine("this._dynamicalertservice.fire({ ");
            tsBuilder.AppendLine("title: 'Do you want to delete this data ?', ");
            tsBuilder.AppendLine("text: 'This process is irreversible.', ");
            tsBuilder.AppendLine("icon: 'warning', ");
            tsBuilder.AppendLine("showCancelButton: true, ");
            tsBuilder.AppendLine("confirmButtonText: 'yes', ");
            tsBuilder.AppendLine("cancelButtonText: 'no' ");
            tsBuilder.AppendLine("}).then((r) => { ");
            tsBuilder.AppendLine("if (r && r.isConfirmed) ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("this._masterserviceobj." + entityServiceMethod[UtilityScript.delete_key] + "({ " + primary_key_column + ": _code }).subscribe({ ");
            tsBuilder.AppendLine("next: (data) => { ");
            tsBuilder.AppendLine("this.data_items = this.data_items.filter((x) => { ");
            tsBuilder.AppendLine("return !(x." + primary_key_column + " == _code) ");
            tsBuilder.AppendLine("}) ");
            tsBuilder.AppendLine("}, error: (err_response) => { ");
            tsBuilder.AppendLine("this._notificationservice.showToast(err_response['error']['message']); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}) ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}) ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            // Export to Excel
            tsBuilder.AppendLine("export_to_excel(): void { ");
            //Start
            tsBuilder.AppendLine($"this._masterserviceobj.{entityServiceMethod[UtilityScript.view_key]}(this.query)");
            tsBuilder.AppendLine(".subscribe({ ");
            tsBuilder.AppendLine("next: (event) => { ");
            tsBuilder.AppendLine("let data = event as HttpResponse<Blob>; ");
            tsBuilder.AppendLine("const downloadedFile = new Blob([data.body as BlobPart], { ");
            tsBuilder.AppendLine("type: data.body?.type ");
            tsBuilder.AppendLine("}); ");
            tsBuilder.AppendLine("if (downloadedFile != null && downloadedFile.type != '') ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("var link = document.createElement('a'); ");
            tsBuilder.AppendLine("var downloadURL = window.URL.createObjectURL(downloadedFile); ");
            tsBuilder.AppendLine("link.href = downloadURL; ");
            tsBuilder.AppendLine("link.download = this._title + \".xlsx\"; ");
            tsBuilder.AppendLine("link.click(); ");
            tsBuilder.AppendLine("window.URL.revokeObjectURL(downloadURL); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}, error: (err_response) => { ");
            tsBuilder.AppendLine("this._notificationservice.showToast(err_response['error']['message']); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("}); ");
            //End
            tsBuilder.AppendLine("} ");

            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("public custom_events(event: any) { ");
            tsBuilder.AppendLine("console.log(event); ");
            tsBuilder.AppendLine("let ColumnCaption = String(event?.action).trim().toLowerCase(); ");
            tsBuilder.AppendLine("if (ColumnCaption == 'edit') ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine($"this.add_edit_record(event.data.{primary_key_column}); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("else if (ColumnCaption == 'delete') ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine($"this.delete_record(event.data.{primary_key_column}); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("else if (ColumnCaption == 'add_new') ");
            tsBuilder.AppendLine("{ ");
            tsBuilder.AppendLine("this.add_edit_record('0'); ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine("} ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("change_cell_value(event: any) { ");
            tsBuilder.AppendLine("this.table_info.primarykeyvalue = event.current_data[this.table_info.primarykeycolumn]; ");
            tsBuilder.AppendLine("this.table_info.fieldcolumn = event.column_config.ColumnName; ");
            tsBuilder.AppendLine("this.table_info.fieldvalue = event.current_data[this.table_info.fieldcolumn]; ");
            tsBuilder.AppendLine(" ");
            tsBuilder.AppendLine("} ");

            tsBuilder.AppendLine("} ");


            return new Tuple<string, string>(list_component_name, tsBuilder.ToString());
        }

        // {entity_name}.component.spec.ts
        public static string generate_list_component_spec_ts(string entity_name, string title, string list_component_name)
        {
            string ComponentName = list_component_name;
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import { ComponentFixture, TestBed } from '@angular/core/testing';");
            tsBuilder.AppendLine("import { " + ComponentName + " } from './" + entity_name + ".component';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("describe('" + ComponentName + "', () => {");
            tsBuilder.AppendLine($"let component: {ComponentName};");
            tsBuilder.AppendLine($"let fixture: ComponentFixture<{ComponentName}>;");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("beforeEach(() => {");
            tsBuilder.AppendLine("TestBed.configureTestingModule({");
            tsBuilder.AppendLine($"declarations: [{ComponentName}]");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine($"fixture = TestBed.createComponent({ComponentName});");
            tsBuilder.AppendLine("component = fixture.componentInstance;");
            tsBuilder.AppendLine("fixture.detectChanges();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("it('should create', () => {");
            tsBuilder.AppendLine("expect(component).toBeTruthy();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("});");
            return tsBuilder.ToString();
        }

        /// To Generate Add & Update Componenet
        /// 


        //{entity_name}-add-edit.component.html
        public static Tuple<string, List<table_design>> generate_add_edit_component_html_m2(string entity_name, string title, string primary_key_column, DataTable control_config, string schema_name)
        {
            var sort_meta_data = new List<table_design>();
            string form_group_name = $"{entity_name}Form";
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine($"<div id=\"{entity_name}_entry_container\" tabindex=\"1\"  style=\"border:none;outline:none\"> ");
            htmlBuilder.AppendLine($"<div class=\"container-fluid common-master-form\"> ");
            htmlBuilder.AppendLine($"<div mat-dialog-title class=\"d-flex justify-content-between align-items-center\"> ");
            htmlBuilder.AppendLine("<h3>{{_title}}</h3>");
            htmlBuilder.AppendLine($"<button class=\"btn close-btn\" (click)=\"close_dialog(null)\"><i class=\"bi bi-x-lg\"></i></button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"<form [formGroup]=\"{entity_name}Form\" *ngIf=\"{entity_name}Form\"> ");
            htmlBuilder.AppendLine($"<mat-dialog-content class=\"mat-typography\"> ");

            var table_meta_data = SQLScriptGenerator.GetTable_Designs(entity_name, schema_name);
            var sys_column_lst = SQLScriptGenerator.sys_column_lst;
            sys_column_lst.Add("is_locked");

            sort_meta_data = table_meta_data.Where(g => !sys_column_lst.Contains(g._column_name.ToLower().Trim())).ToList();
            foreach (var item in sort_meta_data)
            {
                var _column_name = item._column_name;
                if (primary_key_column != _column_name)
                {
                    var _preferred_controls = item._preferred_controls;
                    var caption = item._preferred_controls;
                    var IsMandetory = !item.IsNullable;
                    var getuserData = control_config.AsEnumerable().Where(g => Convert.ToString(g["Column Name"]).ToLower().Trim() == _column_name).ToArray();
                    if (getuserData != null && getuserData.Length > 0)
                    {
                        _preferred_controls = Convert.ToString(getuserData[0]["Control Name"]);
                        caption = Convert.ToString(getuserData[0]["Caption"]);
                        IsMandetory = Convert.ToString(getuserData[0]["Must"]).ToLower().Trim().Equals("true");
                    }
                    htmlBuilder.AppendLine($"<div class=\"row\" > ");
                    htmlBuilder.AppendLine($"<div class=\"col-12 col-lg-6 form-inline d-flex align-items-end py-2\" > ");
                    htmlBuilder.AppendLine($"<div class=\"form-group\"> ");
                    if (_preferred_controls == "txtbox")
                    {
                        htmlBuilder.AppendLine($"<smart-txtbox  ");
                        htmlBuilder.AppendLine($"class=\"w-100\" [form_group]=\"{form_group_name}\" caption=\"{caption}\" ");
                        htmlBuilder.AppendLine($"type=\"text\" field=\"{item._column_name}\" ");
                        htmlBuilder.AppendLine($"(copy)=\"false\" (paste)=\"false\" autocomplete=\"disabled\" ");
                        htmlBuilder.AppendLine($"[is_req]=\"{IsMandetory}\" ");
                        if (false)
                        {
                            htmlBuilder.AppendLine($"[is_numeric_with_minus]=\"custom_fields[i].datatype == 2\" ");
                            htmlBuilder.AppendLine($"[is_bit]=\"custom_fields[i].datatype == 3\" ");
                            htmlBuilder.AppendLine($"[maxlen]=\"custom_fields[i].text_max_length\" ");
                            htmlBuilder.AppendLine($"[minlen]=\"custom_fields[i].text_min_length\" ");
                            htmlBuilder.AppendLine($"[maxnumvalue]=\"custom_fields[i].num_max_value\" ");
                            htmlBuilder.AppendLine($"[digitafterdecimal]=\"custom_fields[i].digits_after_decimal\" ");
                            htmlBuilder.AppendLine($"(keypress)=\"text_value_key_up_handler($event)\" ");
                            htmlBuilder.AppendLine($"(keydown)=\"text_value_key_down_handler($event,custom_fields[i].text_case)\" ");
                            htmlBuilder.AppendLine($"[block_spl_char]=\"custom_fields[i].text_type==1\" ");
                            htmlBuilder.AppendLine($"[is_OnlyAlphabet]=\"custom_fields[i].text_type==2\" ");
                        }
                        htmlBuilder.AppendLine($"></smart-txtbox> ");
                    }
                    if (_preferred_controls == "date-picker")
                    {
                        htmlBuilder.AppendLine($"<smart-date-picker  [form_group]=\"{form_group_name}\" ");
                        htmlBuilder.AppendLine($"field=\"{item._column_name}\" [is_req]=\"custom_fields[i].IsMandetory\" caption=\"{caption}\"> ");
                        htmlBuilder.AppendLine($"</smart-date-picker> ");
                    }
                    if (_preferred_controls == "select")
                    {
                        htmlBuilder.AppendLine($"<smart-select  class=\"smart-componenet w-100\" ");
                        htmlBuilder.AppendLine($"[formGroup]=\"{form_group_name}\" field=\"{item._column_name}\" ");
                        htmlBuilder.AppendLine($"[IsReq]=\"custom_fields[i].IsMandetory\" Caption=\"{caption}\" ");
                        htmlBuilder.AppendLine($"[options]=\"custom_fields[i].options\" ");
                        htmlBuilder.AppendLine($"[multi_select]=\"false\"></smart-select> ");
                    }
                    if (_preferred_controls == "switch" || _column_name == "is_locked")
                    {
                        htmlBuilder.AppendLine($"<label class=\" me-2 w-auto\">Locked</label> ");
                        htmlBuilder.AppendLine($"<smart-switch [formGroup]=\"{form_group_name}\" ");
                        htmlBuilder.AppendLine($"field=\"{caption}\"> ");
                        htmlBuilder.AppendLine($"</smart-switch> ");
                    }
                    htmlBuilder.AppendLine($"</div> ");
                    htmlBuilder.AppendLine($"</div> ");
                    htmlBuilder.AppendLine($"</div> ");
                }
            }

            if (table_meta_data.Where(g => g._column_name.Equals("is_locked")).ToList() != null)
            {
                htmlBuilder.AppendLine($"<div class=\"row\" > ");
                htmlBuilder.AppendLine($"<div class=\"col-12 col-lg-6 form-inline d-flex align-items-end py-2\" > ");
                htmlBuilder.AppendLine($"<div class=\"form-group\"> ");
                htmlBuilder.AppendLine($"<label class=\" me-2 w-auto\">Locked</label> ");
                htmlBuilder.AppendLine($"<smart-switch [formGroup]=\"{form_group_name}\" ");
                htmlBuilder.AppendLine($"field=\"is_locked\" > ");
                htmlBuilder.AppendLine($"</smart-switch> ");
                htmlBuilder.AppendLine($"</div> ");
                htmlBuilder.AppendLine($"</div> ");
                htmlBuilder.AppendLine($"</div> ");
            }

            htmlBuilder.AppendLine($"</mat-dialog-content> ");
            htmlBuilder.AppendLine($"<mat-dialog-actions class=\"foot-action-btns justify-content-end gap-2\"> ");
            htmlBuilder.AppendLine($"<div> ");
            htmlBuilder.AppendLine("<button mat-button [disabled]=\"!" + form_group_name + ".valid\" (click)=\"submit()\" class=\"btn save-btn \" >{{ isNew ? 'Add' : 'Update' }}</button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"<div> ");
            htmlBuilder.AppendLine($"<button mat-button (click)=\"close_dialog(null)\" class=\"btn close-btn \">Cancel</button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($" </mat-dialog-actions> ");
            htmlBuilder.AppendLine($"</form> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"</div> ");
            return new Tuple<string, List<table_design>>(htmlBuilder.ToString(), sort_meta_data);



        }

        public static Tuple<string, List<table_design>> generate_add_edit_component_html(string entity_name, string title, string primary_key_column, DataTable control_config, string schema_name)
        {
            var sort_meta_data = new List<table_design>();
            string form_group_name = $"{entity_name}Form";
            StringBuilder htmlBuilder = new StringBuilder();
            string form_group_key = "formGroup";

            htmlBuilder.AppendLine($"<div class=\"add-new-popup\" [class.rtl-enabled]=\"themeService.isRTLEnabled()\"> ");
            htmlBuilder.AppendLine($"<div class=\"popup-dialog\"> ");
            htmlBuilder.AppendLine($"<mat-card class=\"daxa-card mb-25 border-radius bg-white border-none d-block\"> ");
            htmlBuilder.AppendLine($"<mat-card-header> ");
            htmlBuilder.AppendLine($"<mat-card-title> ");
            htmlBuilder.AppendLine("<h5 class=\"mb-0\">{{form_title}}</h5> ");
            htmlBuilder.AppendLine($"</mat-card-title> ");
            htmlBuilder.AppendLine($"<mat-card-subtitle> ");
            htmlBuilder.AppendLine($"<button mat-button (click)=\"close_dialog(null)\"> ");
            htmlBuilder.AppendLine($"<i class=\"ri-close-fill\"></i> ");
            htmlBuilder.AppendLine($"</button> ");
            htmlBuilder.AppendLine($"</mat-card-subtitle> ");
            htmlBuilder.AppendLine($"</mat-card-header> ");
            htmlBuilder.AppendLine($"<mat-card-content> ");
            htmlBuilder.AppendLine($"<div class=\"row\"> ");
            htmlBuilder.AppendLine($"<form [formGroup]=\"entity_formgroup\"> ");
            htmlBuilder.AppendLine($"<div class=\"row\"> ");
            var table_meta_data = SQLScriptGenerator.GetTable_Designs(entity_name, schema_name);
            var sys_column_lst = SQLScriptGenerator.sys_column_lst;
            sys_column_lst.Add("is_locked");

            sort_meta_data = table_meta_data.Where(g => !sys_column_lst.Contains(g._column_name.ToLower().Trim())).ToList();
            foreach (var item in sort_meta_data)
            {
                var _column_name = item._column_name;
                var _preferred_controls = item._preferred_controls;
                var caption = item._preferred_controls;
                var IsMandetory = !item.IsNullable;
                string sp_select_search = string.Empty;
                if (primary_key_column != _column_name)
                {

                    var getuserData = control_config.AsEnumerable().Where(g => Convert.ToString(g["Column Name"]).ToLower().Trim() == _column_name).ToArray();
                    if (getuserData != null && getuserData.Length > 0)
                    {
                        _preferred_controls = Convert.ToString(getuserData[0]["Control Name"]);
                        caption = Convert.ToString(getuserData[0]["Caption"]);
                        IsMandetory = Convert.ToString(getuserData[0]["Must"]).ToLower().Trim().Equals("true");
                        if (control_config.Columns.Contains("sp_select_search"))
                        {
                            sp_select_search = Convert.ToString(getuserData[0]["sp_select_search"]);
                        }
                    }
                    if (_preferred_controls == "txtbox")
                    {
                        htmlBuilder.AppendLine($"<div class=\"col-md-4 col-sm-6\"> ");
                        htmlBuilder.AppendLine($"<app-lite-box [formGroup]=\"entity_formgroup\" field=\"{item._column_name}\" Caption=\"{caption}\" [Isnum]=\"'text'\" [IsReq]=\"{Convert.ToString(IsMandetory).ToLowerInvariant()}\" [maxLen]=\"20\"></app-lite-box> ");
                        htmlBuilder.AppendLine($"</div> ");
                    }
                    if (_preferred_controls == "date-picker")
                    {

                    }
                    if (_preferred_controls == "select")
                    {
                        htmlBuilder.AppendLine($"<div class=\"col-md-4 col-sm-6\"> ");
                        htmlBuilder.AppendLine($"<lite-ddl [formGroup]=\"entity_formgroup\" field=\"{item._column_name}\" [Caption]=\"'{caption}'\" [IsReq]=\"{Convert.ToString(IsMandetory).ToLowerInvariant()}\" [options]=\"countryList\"> ");
                        htmlBuilder.AppendLine($"</lite-ddl> ");
                    }
                    if (_preferred_controls == "switch" || _column_name == "is_locked")
                    {
                        htmlBuilder.AppendLine($"<div class=\"col-md-4 col-sm-6\"> ");
                        htmlBuilder.AppendLine($"<lite-switch [formGroup]=\"entity_formgroup\" field=\"{item._column_name}\" Caption=\"{caption}\"></lite-switch> ");
                        htmlBuilder.AppendLine($"</div> ");
                    }
                    if (_preferred_controls == "editor")
                    {
                        htmlBuilder.AppendLine($"<div class=\"col-md-8 col-sm-6\"> ");
                        htmlBuilder.AppendLine($"<lite-editor [formGroup]=\"entity_formgroup\" field=\"note\" Caption=\"Company Description\"></lite-editor> ");
                        htmlBuilder.AppendLine($"</div> ");
                    }
                    if (_preferred_controls == "textarea")
                    {
                        htmlBuilder.AppendLine($"<div class=\"col-md-4 col-sm-6\"> ");
                        htmlBuilder.AppendLine($"<lite-textarea [formGroup]=\"entity_formgroup\" field=\"address\" Caption=\"Address\"></lite-textarea> ");
                        htmlBuilder.AppendLine($"</div> ");
                    }
                    if (_preferred_controls == "file")
                    {
                        htmlBuilder.AppendLine($"<div class=\"col-md-8 col-sm-6\"> ");
                        htmlBuilder.AppendLine($"<app-lite-file-uploader [formGroup]=\"entity_formgroup\" field=\"logo\" [multiple]=\"false\" accept=\"image/*\"></app-lite-file-uploader> ");
                        htmlBuilder.AppendLine($"</div> ");
                    }
                }
            }

            htmlBuilder.AppendLine($"<!-- Left Side: Company Details --> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"<!-- Buttons Section --> ");
            htmlBuilder.AppendLine($"<div class=\"btn-box\"> ");
            htmlBuilder.AppendLine($"<button mat-flat-button color=\"primary\" class=\"me-2\" [disabled]=\"!entity_formgroup.valid\" (click)=\"formsubmit()\">Save</button> ");
            htmlBuilder.AppendLine($"<button mat-flat-button color=\"warn\" (click)=\"close_dialog(null)\">Cancel</button> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"</form> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"</mat-card-content> ");
            htmlBuilder.AppendLine($"</mat-card> ");
            htmlBuilder.AppendLine($"</div> ");
            htmlBuilder.AppendLine($"</div> ");

            return new Tuple<string, List<table_design>>(htmlBuilder.ToString(), sort_meta_data);



        }

        //{entity_name}-add-edit.component.scss
        public static string generate_add_edit_component_scss(string entity_name, string title)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            return htmlBuilder.ToString();
        }

        //{entity_name}-add-edit.component.ts
        public static string generate_add_edit_component_ts(string entity_name, string _edit_component_name, string _entityservice, string primary_key_column, Dictionary<string, string> entityServiceMethod, List<table_design> edit_columninfo)
        {
            string _form_name = $"{entity_name}Form";
            StringBuilder TsBuilder = new StringBuilder();


            TsBuilder.AppendLine("import { isPlatformBrowser } from '@angular/common'; ");
            TsBuilder.AppendLine("import { Component, Inject, PLATFORM_ID, OnInit, OnDestroy } from '@angular/core'; ");
            TsBuilder.AppendLine("import { FormBuilder, FormGroup } from '@angular/forms'; ");
            TsBuilder.AppendLine("import { CommonService } from '../../../../shared/services/common.service'; ");
            TsBuilder.AppendLine("import { CustomizerSettingsService } from '../../../../shared/customizer-settings/customizer-settings.service'; ");
            TsBuilder.AppendLine("import { debounceTime, distinctUntilChanged, filter, switchMap } from 'rxjs'; ");
            TsBuilder.AppendLine("import { ComponyService } from '../../../company/compony.service'; ");
            TsBuilder.AppendLine("import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog'; ");
            TsBuilder.AppendLine("import { MasterService } from '../../master.service'; ");
            TsBuilder.AppendLine("import { NotificationService } from '../../../../shared/services/notification.service'; ");
            TsBuilder.AppendLine("import { LoaderService } from '../../../../shared/services/loader.service'; ");
            TsBuilder.AppendLine("import { DynamicAlertService } from '../../../../shared/common/dynamic-alert/dynamic-alert.service'; ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("@Component({ ");
            TsBuilder.AppendLine($"selector: 'app-{entity_name}-add-edit', ");
            TsBuilder.AppendLine("standalone: false, ");
            TsBuilder.AppendLine($"templateUrl: './{entity_name}-add-edit.component.html', ");
            TsBuilder.AppendLine($"styleUrl: './{entity_name}-add-edit.component.scss' ");
            TsBuilder.AppendLine("}) ");
            TsBuilder.AppendLine($"export class {_edit_component_name} implements OnInit ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("public entity_formgroup: FormGroup; ");
            TsBuilder.AppendLine("public entity_formgroup_backup: FormGroup; ");
            TsBuilder.AppendLine("public entity_code: number = 0; ");
            TsBuilder.AppendLine("public file_server_path: string; ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine($"public form_title: string = \"{entity_name}\"; ");
            TsBuilder.AppendLine("public errormessage: string = \"\"; ");
            TsBuilder.AppendLine("public isnew: boolean = true; ");
            TsBuilder.AppendLine(" ");

            TsBuilder.AppendLine("classApplied = true; ");
            TsBuilder.AppendLine("constructor( ");
            TsBuilder.AppendLine("private fb: FormBuilder, ");
            TsBuilder.AppendLine("@Inject(MAT_DIALOG_DATA) public data: any, ");
            TsBuilder.AppendLine($"public dialogRef: MatDialogRef<{_edit_component_name}>, ");
            TsBuilder.AppendLine("@Inject(PLATFORM_ID) private platformId: Object, ");
            TsBuilder.AppendLine("public themeService: CustomizerSettingsService, ");
            TsBuilder.AppendLine("private _notificationservice: NotificationService, ");
            TsBuilder.AppendLine("private _dynamicalertservice: DynamicAlertService, ");
            TsBuilder.AppendLine("private _loader: LoaderService, ");
            TsBuilder.AppendLine("private _commonservice: CommonService, ");
            TsBuilder.AppendLine("private _masterserviceobj: MasterService ");
            TsBuilder.AppendLine(") { } ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("ngOnInit() : void { ");
            TsBuilder.AppendLine("if (isPlatformBrowser(this.platformId)) { ");
            TsBuilder.AppendLine("this.init(); ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("init() : void { ");
            TsBuilder.AppendLine("console.log(this.data); ");
            TsBuilder.AppendLine("this.entity_code = this.data.entity_code > 0 ? this.data.entity_code : null; ");
            TsBuilder.AppendLine("this.isnew = (this.entity_code == null); ");
            TsBuilder.AppendLine("this.build_form(null); ");
            TsBuilder.AppendLine("this.fetch_data(); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("build_form(rowdata: any) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("var obj: any = { } ");
            TsBuilder.AppendLine("; ");
            TsBuilder.AppendLine("this.entity_formgroup = this.fb.group( ");
            TsBuilder.AppendLine("{ ");
            var sys_column_lst = SQLScriptGenerator.sys_column_lst;
            for (int b = 0; b < edit_columninfo.Count; b++)
            {
                if (!sys_column_lst.Contains(edit_columninfo[b]._column_name))
                {
                    TsBuilder.AppendLine($"{edit_columninfo[b]._column_name}: [rowdata != null ? rowdata.{edit_columninfo[b]._column_name} : {(edit_columninfo[b]._column_name.Equals(primary_key_column) ? "0" : "null")}]");
                    if (!(b == (edit_columninfo.Count - 1)))
                    {
                        TsBuilder.Append(",");
                    }
                }
            }
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("); ");
            TsBuilder.AppendLine("this.entity_formgroup_backup = this._commonservice.cloneForm(this.entity_formgroup); ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("fetch_data() ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("if (this.entity_code > 0) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("this._masterserviceobj." + entityServiceMethod[UtilityScript.read_key] + "({ " + primary_key_column + ": this.entity_code }) ");
            TsBuilder.AppendLine(".subscribe({ ");
            TsBuilder.AppendLine("next: (RtnData) => { ");
            TsBuilder.AppendLine("var datarow = RtnData?.data[0]; ");
            TsBuilder.AppendLine("this.build_form(datarow); ");
            TsBuilder.AppendLine("}, error: (err_response) => { ");
            TsBuilder.AppendLine("this._notificationservice.showToast(err_response['error']['message']); ");
            TsBuilder.AppendLine("this.close_dialog(null); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("}); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("close_dialog(obj: any) : void { ");
            TsBuilder.AppendLine("this.dialogRef.close(obj); ");
            TsBuilder.AppendLine("this.classApplied = !this.classApplied; ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");

            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("ngOnDestroy(): void { ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("save_entity_data(): void { ");
            TsBuilder.AppendLine("this._loader.show(); ");
            TsBuilder.AppendLine("let snd_data = this.entity_formgroup.getRawValue(); ");
            TsBuilder.AppendLine($"this._masterserviceobj.{entityServiceMethod[UtilityScript.create_update_key]}(snd_data) ");
            TsBuilder.AppendLine(".subscribe({ ");
            TsBuilder.AppendLine("next: (data) => { ");
            TsBuilder.AppendLine("if (data.data[0].update_count > 0) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("this._notificationservice.showToast('record updated successfully!'); ");
            TsBuilder.AppendLine("//this.toastr.success('record updated successfully!'); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine($"this.entity_code = data.data[0].{primary_key_column}; ");
            TsBuilder.AppendLine($"snd_data.{primary_key_column} = this.entity_code; ");
            TsBuilder.AppendLine("this.close_dialog(null); ");
            TsBuilder.AppendLine("this._loader.hide(); ");
            TsBuilder.AppendLine("}, error: (err_response) => { ");
            TsBuilder.AppendLine("this._notificationservice.showToast(err_response['error']['message']); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("}); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("custom_validators(): string { ");
            TsBuilder.AppendLine("let err_message: string = ''; ");
            TsBuilder.AppendLine("if (err_message) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("err_message = err_message.substring(0, err_message.length - 1); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("return err_message; ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("formsubmit() { ");
            TsBuilder.AppendLine("let err = this.custom_validators(); ");
            TsBuilder.AppendLine("if (err) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("this._dynamicalertservice.fire({ ");
            TsBuilder.AppendLine("title: 'Error', ");
            TsBuilder.AppendLine("text: err, ");
            TsBuilder.AppendLine("icon: 'error', ");
            TsBuilder.AppendLine("confirmButtonText: 'ok', ");
            TsBuilder.AppendLine("}) ");
            TsBuilder.AppendLine("return; ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("if (this.entity_formgroup.valid) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("if (this.entity_code) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("this._dynamicalertservice.fire({ ");
            TsBuilder.AppendLine("title: 'Are you sure you want to update the data ?', ");
            TsBuilder.AppendLine("text: 'This process is irreversible. ', ");
            TsBuilder.AppendLine("icon: 'warning', ");
            TsBuilder.AppendLine("showCancelButton: true, ");
            TsBuilder.AppendLine("confirmButtonText: 'yes', ");
            TsBuilder.AppendLine("cancelButtonText: 'no' ");
            TsBuilder.AppendLine("}).then((r) => { ");
            TsBuilder.AppendLine("if (r.isConfirmed) ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("this.save_entity_data(); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("}) ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("else ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("this.save_entity_data(); ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("else ");
            TsBuilder.AppendLine("{ ");
            TsBuilder.AppendLine("this._dynamicalertservice.fire({ ");
            TsBuilder.AppendLine("title: 'Error', ");
            TsBuilder.AppendLine("text: 'validation error', ");
            TsBuilder.AppendLine("icon: 'error', ");
            TsBuilder.AppendLine("confirmButtonText: 'ok', ");
            TsBuilder.AppendLine("}) ");
            TsBuilder.AppendLine("return; ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine("} ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine(" ");
            TsBuilder.AppendLine("} ");





            return TsBuilder.ToString();
        }

        //{entity_name}-add-edit.component.spec.ts
        public static string generate_add_edit_component_spec_ts(string entity_name, string component_name, string title, string add_edit_component_name)
        {
            StringBuilder tsBuilder = new StringBuilder();
            tsBuilder.AppendLine("import {ComponentFixture,TestBed}  from '@angular/core/testing';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("import { " + component_name + " }  from './" + entity_name + "-add-edit.component';");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("describe('" + component_name + "', () => {");
            tsBuilder.AppendLine("let component: " + component_name + ";");
            tsBuilder.AppendLine("let fixture: ComponentFixture<" + component_name + ">;");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("beforeEach(() => {");
            tsBuilder.AppendLine("TestBed.configureTestingModule({");
            tsBuilder.AppendLine("declarations: [" + component_name + "]");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("fixture = TestBed.createComponent(" + component_name + ");");
            tsBuilder.AppendLine("component = fixture.componentInstance;");
            tsBuilder.AppendLine("fixture.detectChanges();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("\n");
            tsBuilder.AppendLine("it('should create', () => {");
            tsBuilder.AppendLine("expect(component).toBeTruthy();");
            tsBuilder.AppendLine("});");
            tsBuilder.AppendLine("});");

            return tsBuilder.ToString();
        }


    }
}
