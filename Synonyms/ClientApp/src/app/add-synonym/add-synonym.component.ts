import { Component, OnInit } from '@angular/core';
import { SynonymService } from '../services/synonymService';
import { NgModel } from '@angular/forms';

@Component({
  selector: 'app-synonym-add',
  templateUrl: './add-synonym.component.html',
  styleUrls: ['./add-synonym.component.css']
})
export class AddSynonymComponent {
  constructor(private synonymService: SynonymService) { }

  term: string = "";
  synonyms: string = "";

  addSynonym(){
    // if (this.term === "" || this.term === null || this.synonyms === "" || this.synonyms === null) return;

    this.synonymService.addSynonym(this.term,this.synonyms);
    this.term = "";
    this.synonyms = "";
  }
}
