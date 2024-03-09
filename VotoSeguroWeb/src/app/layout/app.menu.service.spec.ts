import { TestBed } from '@angular/core/testing';
import { MenuChangeEvent } from './api/menuchangeevent';
import { MenuService } from './app.menu.service';

describe('MenuService', () => {
    let menuService: MenuService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [MenuService],
        });
        menuService = TestBed.inject(MenuService);
    });

    it('should emit menu change event', (done) => {
        const menuChangeEvent: MenuChangeEvent = {
            key: '',
        };
        menuService.menuSource$.subscribe((event: any) => {
            expect(event).toEqual(menuChangeEvent);
            done();
        });
        menuService.onMenuStateChange(menuChangeEvent);
    });

    it('should emit reset event', (done) => {
        menuService.resetSource$.subscribe(() => {
            expect(true).toBeTrue();
            done();
        });
        menuService.reset();
    });
});
