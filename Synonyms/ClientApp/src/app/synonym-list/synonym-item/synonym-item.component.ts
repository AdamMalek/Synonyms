import { Component, OnInit, Input } from '@angular/core';
import { SynonymItem } from '../../models/synonymItem';

@Component({
  selector: 'app-synonym-item',
  templateUrl: './synonym-item.component.html',
  styleUrls: ['./synonym-item.component.css']
})
export class SynonymItemComponent {
  @Input() public item:SynonymItem;
}
