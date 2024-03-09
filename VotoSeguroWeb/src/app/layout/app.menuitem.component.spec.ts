import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppMenuitemComponent } from './app.menuitem.component';
import { MenuService } from './app.menu.service';

describe('AppMenuitemComponent', () => {
    let component: AppMenuitemComponent;
    let fixture: ComponentFixture<AppMenuitemComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [AppMenuitemComponent],
            imports: [RouterTestingModule],
            providers: [MenuService],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AppMenuitemComponent);
        component = fixture.componentInstance;
        component.item = {
            label: 'PPR GQ 11',
            icon: 'pi pi-fw pi-file',
            routerLink: ['/inspection/fila-inspecao-PPR_GQ_11'],
        };
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should update active state on menu state change', () => {
        const menuService = TestBed.inject(MenuService);
        const menuStateChangeEvent = { key: 'test-key', routeEvent: false };
        component.parentKey = 'parent-key';
        component.index = 0;
        component.key = 'parent-key-0';
        menuService.onMenuStateChange(menuStateChangeEvent);
        expect(component.active).toBe(false);
        menuStateChangeEvent.key = 'parent-key-1';
        menuService.onMenuStateChange(menuStateChangeEvent);
        expect(component.active).toBe(false);
    });

    it('should update active state on route change', () => {
        const menuService = TestBed.inject(MenuService);
        component.item = {
            routerLink: ['/test'],
        };
        component.parentKey = 'parent-key';
        component.index = 0;
        component.key = 'parent-key-0';
        component.updateActiveStateFromRoute();
        expect(component.active).toBe(false);
        menuService.onMenuStateChange({ key: 'parent-key-0', routeEvent: true });
        expect(component.active).toBe(true);
    });

    it('should toggle active state on item click', () => {
        const menuService = TestBed.inject(MenuService);
        component.item = {
            items: [],
        };
        expect(component.active).toBe(false);
        component.itemClick({ preventDefault: () => {} } as any);
        expect(component.active).toBe(true);
        component.itemClick({ preventDefault: () => {} } as any);
        expect(component.active).toBe(false);
        component.item = {
            disabled: true,
            items: [],
        };
        component.active = true;
        component.itemClick({ preventDefault: () => {} } as any);
        expect(component.active).toBe(true);
        component.item.command = () => {};
        component.itemClick({ preventDefault: () => {} } as any);
        expect(component.active).toBe(true);
    });

    it('should have submenuAnimation "expanded" when root or active', () => {
        component.root = true;
        expect(component.submenuAnimation).toBe('expanded');
        component.active = true;
        expect(component.submenuAnimation).toBe('expanded');
        component.root = false;
        component.active = false;
        expect(component.submenuAnimation).toBe('collapsed');
    });
});
