import { TestBed } from '@angular/core/testing';
import { LayoutService } from './app.layout.service';

describe('LayoutService', () => {
    let service: LayoutService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(LayoutService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should initialize with default values', () => {
        expect(service.config).toEqual({
            ripple: false,
            inputStyle: 'outlined',
            menuMode: 'static',
            colorScheme: 'light',
            theme: 'lara-light-indigo',
            scale: 14,
        });

        expect(service.state).toEqual({
            staticMenuDesktopInactive: false,
            overlayMenuActive: false,
            profileSidebarVisible: false,
            configSidebarVisible: false,
            staticMenuMobileActive: false,
            menuHoverActive: false,
        });
    });

    it('should emit config update', (done) => {
        const newConfig = {
            ripple: true,
            inputStyle: 'filled',
            menuMode: 'overlay',
            colorScheme: 'dark',
            theme: 'lara-dark-blue',
            scale: 16,
        };

        service.configUpdate$.subscribe((config) => {
            expect(config).toEqual(newConfig);
            done();
        });

        service.config = newConfig;
        service.onConfigUpdate();
    });

    it('should emit overlay open', (done) => {
        service.overlayOpen$.subscribe(() => {
            expect(true).toBe(true);
            done();
        });

        service.onOverlaySubmenuOpen();
    });

    it('should toggle menu states', () => {
        expect(service.state.overlayMenuActive).toBeFalse();
        expect(service.state.staticMenuDesktopInactive).toBeFalse();
        expect(service.state.staticMenuMobileActive).toBeFalse();

        service.onMenuToggle();
        expect(service.state.overlayMenuActive).toBeFalse();

        service.onMenuToggle();
        expect(service.state.staticMenuDesktopInactive).toBeFalse();
    });

    it('should show/hide profile sidebar', () => {
        expect(service.state.profileSidebarVisible).toBeFalse();

        service.showProfileSidebar();
        expect(service.state.profileSidebarVisible).toBeTrue();

        service.showProfileSidebar();
        expect(service.state.profileSidebarVisible).toBeFalse();
    });

    it('should show config sidebar', () => {
        expect(service.state.configSidebarVisible).toBeFalse();

        service.showConfigSidebar();
        expect(service.state.configSidebarVisible).toBeTrue();
    });

    it('should check if menu mode is overlay', () => {
        expect(service.isOverlay()).toBeFalse();

        service.config.menuMode = 'overlay';
        expect(service.isOverlay()).toBeTrue();
    });
});
