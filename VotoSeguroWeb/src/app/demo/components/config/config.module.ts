import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ButtonModule } from 'primeng/button';
import { AtomicModule } from '../atomic/atomic.module';
import { DataViewModule } from 'primeng/dataview';
import { ProgressBarModule } from 'primeng/progressbar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { ConfigRoutingModule } from './config-routing.module';
import { ListboxModule } from 'primeng/listbox';
import { DropdownModule } from 'primeng/dropdown';
import { FieldsetModule } from 'primeng/fieldset';
import { ToastModule } from 'primeng/toast';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FileUploadModule } from 'primeng/fileupload';
import { CategoryComponent } from './category/category.component';
import { ItemComponent } from './item/item.component';

@NgModule({
    declarations: [
        CategoryComponent,
        ItemComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        DialogModule,
        ConfirmDialogModule,
        ProgressBarModule,
        DataViewModule,
        SelectButtonModule,
        FormsModule,
        AtomicModule,
        ButtonModule,
        CalendarModule,
        InputTextareaModule,
        ListboxModule,
        InputNumberModule,
        FieldsetModule,
        DropdownModule,
        ToastModule,
        FileUploadModule,
        InputTextModule,
        ConfigRoutingModule,
    ],
})
export class ConfigModule {}
