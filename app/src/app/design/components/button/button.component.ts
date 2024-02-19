import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent {
  @Input() disabled = false;
  @Input() label = '';
  @Input() icon = '';
  @Input() flex = false;
  @Input() buttonStyle: IconStyle | string = '';
  @Output() clicked = new EventEmitter();

  onClick(): void {
    if (!this.disabled) {
      this.clicked.emit();
    }
  }
}

type IconStyle = 'outlined' | 'text' | '';
