import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    selector: 'app-error',
    templateUrl: './error.component.html',
})
export class ErrorComponent implements OnInit {
    error: any = {};
    constructor(
        private router: ActivatedRoute,
        protected layoutService: LayoutService
    ) {}
    ngOnInit() {
        this.router.queryParamMap.subscribe((params) => {
            this.error.code = params.get('code');
            this.error.message = params.get('message');
            this.error.description = params.get('description');
        });
    }
}
