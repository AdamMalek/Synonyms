import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, EventEmitter, Output } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { SynonymItem } from "../models/synonymItem";

@Injectable()
export class SynonymService {
    constructor(private http: HttpClient) { }

    @Output() onDataUpdated: EventEmitter<void> = new EventEmitter<void>();

    public addSynonym(term: string, synonyms: string) {
        this.http.post("/api/synonyms",
                        { term: term, synonyms: synonyms }                        
                    )
            .subscribe(() => { this.onDataUpdated.emit(); });
    }
    public getAllSynonyms(): Observable<SynonymItem[]> {
        return this.http.get<SynonymItem[]>("/api/synonyms");
    }
}