import { Component, Input } from '@angular/core';
import { LayoutService } from '../service/app.layout.service';
import { MenuService } from '../app.menu.service';
import { StorageService } from 'src/app/demo/service/storage.service';

@Component({
    selector: 'app-config',
    templateUrl: './app.config.component.html',
})
export class AppConfigComponent {
    @Input() minimal: boolean = false;

    scales: number[] = [10, 12, 14, 16, 18, 20];

    constructor(public layoutService: LayoutService, public menuService: MenuService, private storageService: StorageService) {
        this.scale = parseInt(storageService.getData('scale') ?? '14');
        this.inputStyle = storageService.getData('inputStyle') ?? 'outlined';
        this.menuMode = storageService.getData('menuMode') ?? 'static';
        this.ripple = Boolean(storageService.getData('ripple') ?? 'false');
        this.changeTheme(storageService.getData('theme') ?? 'lara-light-indigo', storageService.getData('colorScheme') ?? 'light');
        this.applyScale();
    }

    get visible(): boolean {
        return this.layoutService.state.configSidebarVisible;
    }

    set visible(_val: boolean) {
        this.layoutService.state.configSidebarVisible = _val;
    }

    get scale(): number {
        return this.layoutService.config.scale;
    }

    set scale(_val: number) {
        this.storageService.saveData('scale', _val.toString());
        this.layoutService.config.scale = _val;
    }

    get menuMode(): string {
        return this.layoutService.config.menuMode;
    }

    set menuMode(_val: string) {
        this.storageService.saveData('menuMode', _val.toString());
        this.layoutService.config.menuMode = _val;
    }

    get inputStyle(): string {
        return this.layoutService.config.inputStyle;
    }

    set inputStyle(_val: string) {
        this.storageService.saveData('inputStyle', _val.toString());
        this.layoutService.config.inputStyle = _val;
    }

    get ripple(): boolean {
        return this.layoutService.config.ripple;
    }

    set ripple(_val: boolean) {
        this.storageService.saveData('ripple', _val.toString());
        this.layoutService.config.ripple = _val;
    }

    onConfigButtonClick() {
        this.layoutService.showConfigSidebar();
    }

    changeTheme(theme: string, colorScheme: string) {
        this.storageService.saveData('theme', theme);
        this.storageService.saveData('colorScheme', colorScheme);
        const themeLink = <HTMLLinkElement>document.getElementById('theme-css');
        const newHref = themeLink.getAttribute('href')!.replace(this.layoutService.config.theme, theme);
        this.layoutService.config.colorScheme;
        this.replaceThemeLink(newHref, () => {
            this.layoutService.config.theme = theme;
            this.layoutService.config.colorScheme = colorScheme;
            this.layoutService.onConfigUpdate();
        });
    }

    replaceThemeLink(href: string, onComplete: Function) {
        const id = 'theme-css';
        const themeLink = <HTMLLinkElement>document.getElementById('theme-css');
        const cloneLinkElement = <HTMLLinkElement>themeLink.cloneNode(true);

        cloneLinkElement.setAttribute('href', href);
        cloneLinkElement.setAttribute('id', id + '-clone');

        themeLink.parentNode!.insertBefore(cloneLinkElement, themeLink.nextSibling);

        cloneLinkElement.addEventListener('load', () => {
            themeLink.remove();
            cloneLinkElement.setAttribute('id', id);
            onComplete();
        });
    }

    decrementScale() {
        this.scale -= 2;
        this.applyScale();
    }

    incrementScale() {
        this.scale += 2;
        this.applyScale();
    }

    applyScale() {
        document.documentElement.style.fontSize = this.scale + 'px';
    }
}
