import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { SliderModule } from 'primeng/slider';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { PanelModule } from 'primeng/panel';
import { FieldsetModule } from 'primeng/fieldset';
import { FormTableComponent } from './form-table/form-table.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared.module';
import { CheckboxModule } from 'primeng/checkbox';
import { FormComponent } from './form/form.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DialogModule } from 'primeng/dialog';
import { ListboxModule } from 'primeng/listbox';
import { FormTableLazyComponent } from './form-table-lazy/form-table-lazy.component';
import { PickListModule } from 'primeng/picklist';
import { TooltipModule } from 'primeng/tooltip';

@NgModule({
    declarations: [FormTableComponent, FormComponent, FormTableLazyComponent],
    exports: [FormTableComponent, FormComponent, FormTableLazyComponent],
    imports: [
        RouterModule,
        CommonModule,
        RadioButtonModule,
        ReactiveFormsModule,
        FormsModule,
        TableModule,
        RatingModule,
        ButtonModule,
        SliderModule,
        InputTextModule,
        ToggleButtonModule,
        RippleModule,
        DropdownModule,
        MultiSelectModule,
        ListboxModule,
        RadioButtonModule,
        DropdownModule,
        ProgressBarModule,
        ToastModule,
        PanelModule,
        FieldsetModule,
        SharedModule,
        CheckboxModule,
        InputTextareaModule,
        DialogModule,
        PickListModule,
        TooltipModule,
    ],
})
export class AtomicModule {}
