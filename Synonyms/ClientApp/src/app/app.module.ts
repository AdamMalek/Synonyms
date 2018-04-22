import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AddSynonymComponent } from './add-synonym/add-synonym.component';
import { SynonymListComponent } from './synonym-list/synonym-list.component';
import { SynonymItemComponent } from './synonym-list/synonym-item/synonym-item.component';
import { SynonymService } from './services/synonymService';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    AddSynonymComponent,
    SynonymListComponent,
    SynonymItemComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ SynonymService],
  bootstrap: [AppComponent]
})
export class AppModule { }
