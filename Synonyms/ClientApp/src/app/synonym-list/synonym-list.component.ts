import { Component, OnInit, OnDestroy } from '@angular/core';
import { SynonymItem } from '../models/synonymItem';
import { SynonymService } from '../services/synonymService';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-synonym-list',
  templateUrl: './synonym-list.component.html',
  styleUrls: ['./synonym-list.component.css']
})
export class SynonymListComponent implements OnInit, OnDestroy {
  synonymList: Observable<SynonymItem[]>;
  sub: Subscription;
  
  constructor(private synonymService: SynonymService) { }

  ngOnInit() {
    this.synonymList = this.synonymService.getAllSynonyms();
    this.sub = this.synonymService.onDataUpdated.subscribe(() => {
      this.synonymList = this.synonymService.getAllSynonyms();
    })
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
