import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { RatingModule } from 'primeng/rating';
import { CardModule } from 'primeng/card';
import { RippleModule } from 'primeng/ripple';
import { SliderModule } from 'primeng/slider';
import { TableModule } from 'primeng/table';
import { DataViewModule } from 'primeng/dataview';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { ScrollerModule } from 'primeng/scroller';
import { BadgeModule } from 'primeng/badge';

@NgModule({
    imports: [
        CommonModule,
        HomeRoutingModule,
        FormsModule,
        TableModule,
        RatingModule,
        ButtonModule,
        SliderModule,
        InputTextModule,
        ToggleButtonModule,
        RippleModule,
        CardModule,
        MultiSelectModule,
        ScrollerModule,
        DropdownModule,
        ProgressBarModule,
        ToastModule,
        DataViewModule,
        BadgeModule,
    ],
    declarations: [HomeComponent],
})
export class HomeModule {}
