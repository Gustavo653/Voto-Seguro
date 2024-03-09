import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CategoryComponent } from './category/category.component';
import { ItemComponent } from './item/item.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'categories', component: CategoryComponent },
            { path: 'items', component: ItemComponent },
        ]),
    ],
    exports: [RouterModule],
})
export class ConfigRoutingModule {}
