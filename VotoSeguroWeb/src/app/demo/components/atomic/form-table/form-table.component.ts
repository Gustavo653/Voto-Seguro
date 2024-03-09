import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { get } from 'lodash';
import { Table } from 'primeng/table';
import { TableColumn, TableColumnSpan } from 'src/app/demo/api/base';

@Component({
    selector: 'app-form-table',
    templateUrl: './form-table.component.html',
})
export class FormTableComponent implements OnInit {
    @Input() cols: TableColumn[] = [];
    @Input() spans: TableColumnSpan[] = [];
    @Input() data: any;
    @Input() loading: boolean = true;
    @ViewChild('filter') filter!: ElementRef;
    @Output() event: EventEmitter<{
        data: any;
        type: number;
    }> = new EventEmitter();

    globalFilterCols: string[] = [];
    _selection: any[] = [];
    _ = get;

    constructor() {}

    ngOnInit() {
        this.globalFilterCols = this.cols.map((col) => col.field);
    }

    get selection(): any[] {
        return this._selection;
    }

    set selection(val: any[]) {
        this._selection = val;
        this.event.emit({ data: this._selection, type: 0 });
    }

    getSort(type: string): boolean {
        return type != 'redirect' && type != 'checkbox-selection' && type != 'edit' && type != 'delete';
    }

    edit(val: any) {
        this.event.emit({ data: val, type: 1 });
    }

    delete(val: any) {
        this.event.emit({ data: val, type: 2 });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }
}
