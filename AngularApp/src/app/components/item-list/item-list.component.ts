import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [],
  templateUrl: './item-list.component.html',
  styleUrl: './item-list.component.css'
})
export class ItemListComponent {
  @Input() name = "";
  @Input() description = "";
  @Input() price = "";
}
