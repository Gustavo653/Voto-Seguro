import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'type' })
export class TypePipe implements PipeTransform {
    transform(value: string): string {
        switch (value) {
            case 'image':
                return 'Imagem';
            case 'video':
                return 'Vídeo';
            case 'web_woauth':
                return 'Site sem autenticação';
            default:
                return value;
        }
    }
}
