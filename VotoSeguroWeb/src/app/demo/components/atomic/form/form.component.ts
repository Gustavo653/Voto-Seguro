import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormField, PrimeFlexStyle } from 'src/app/demo/api/base';

@Component({
    selector: 'app-form',
    templateUrl: './form.component.html',
})
export class FormComponent implements OnInit, OnChanges {
    form!: FormGroup;
    style = new PrimeFlexStyle();
    data: any = {};
    @Input() inputData: any = {};
    @Input() modalDialog: boolean = false;
    @Input() fields!: FormField[];
    @Output() formData: EventEmitter<any> = new EventEmitter<any>();

    ngOnInit(): void {
        this.form = this.createFormGroup();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (this.inputData) this.data = this.inputData;
    }

    constructor(private formBuilder: FormBuilder) {}

    createFormGroup(): FormGroup {
        const formGroupConfig: { [key: string]: any } = {};
        this.fields.forEach((field: FormField) => {
            const validators = [];
            if (field.email) validators.push(Validators.email);
            if (field.required) validators.push(Validators.required);
            formGroupConfig[field.id] = ['', validators];
        });
        return this.formBuilder.group(formGroupConfig);
    }

    save() {
        if (this.form?.valid) this.formData.emit(this.data);
    }

    hideDialog() {
        this.formData.emit(undefined);
    }

    isFieldInvalid(fieldName: string): boolean {
        const field = this.form.get(fieldName);
        return field ? field.invalid && field.touched : false;
    }

    getErrorMessage(fieldName: string): string {
        const field = this.form.get(fieldName);
        if (field?.errors) {
            if (field.errors['required']) {
                return 'Este campo é obrigatório.';
            }
            if (field.errors['email']) {
                return 'Este campo deve ter um email válido.';
            }
        }
        return '';
    }

    validateAllFormFields(): void {
        Object.keys(this.form.controls).forEach((field) => {
            const control = this.form.get(field);
            if (control) {
                control.markAsTouched({ onlySelf: true });
            }
        });
    }
}
