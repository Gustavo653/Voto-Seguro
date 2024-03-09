import { Component } from '@angular/core';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    selector: 'app-notfound',
    templateUrl: './notfound.component.html',
})
export class NotfoundComponent {
    constructor(protected layoutService:LayoutService){}
}
