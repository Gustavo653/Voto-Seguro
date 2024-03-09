import { Component, OnInit } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';

@Component({
    templateUrl: './home.component.html',
    providers: [MessageService, ConfirmationService],
    styles: [
        `
            :host ::ng-deep .p-frozen-column {
                font-weight: bold;
            }

            :host ::ng-deep .p-datatable-frozen-tbody {
                font-weight: bold;
            }

            :host ::ng-deep .p-progressbar {
                height: 0.5rem;
            }

            .user-profile {
                display: flex;
                align-items: center;
                margin: 10px;
            }
        `,
    ],
})
export class HomeComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
