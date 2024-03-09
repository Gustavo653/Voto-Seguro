import { NgModule } from '@angular/core';
import { TypePipe } from './demo/pipe/TypePipe';
import { BooleanPipe } from './demo/pipe/BooleanPipe';

@NgModule({
    declarations: [TypePipe, BooleanPipe],
    exports: [TypePipe, BooleanPipe],
})
export class SharedModule {}
