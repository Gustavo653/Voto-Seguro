// import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { AppConfigComponent } from './app.config.component';
// import { LayoutService } from '../service/app.layout.service';
// import { MenuService } from '../app.menu.service';
// import { StorageService } from 'src/app/demo/service/storage.service';
// import { HttpClientTestingModule } from '@angular/common/http/testing';

// describe('AppConfigComponent', () => {
//     let component: AppConfigComponent;
//     let fixture: ComponentFixture<AppConfigComponent>;

//     beforeEach(async () => {
//         await TestBed.configureTestingModule({
//             declarations: [AppConfigComponent],
//             imports: [HttpClientTestingModule],
//             providers: [LayoutService, MenuService, StorageService],
//         }).compileComponents();
//     });

//     beforeEach(() => {
//         fixture = TestBed.createComponent(AppConfigComponent);
//         component = fixture.componentInstance;
//         fixture.detectChanges();
//     });

//     it('should create', () => {
//         expect(component).toBeTruthy();
//     });

//     it('should have the default values', () => {
//         expect(component.minimal).toBe(false);
//         expect(component.scales).toEqual([10, 12, 14, 16, 18, 20]);
//     });

//     it('should update the scale when the value changes', () => {
//         const newScale = 18;
//         component.scale = newScale;
//         expect(component.scale).toBe(newScale);
//     });
// });
