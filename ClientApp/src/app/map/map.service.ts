import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { of } from 'rxjs';

export interface Level {
  value: number;
  color: string;
}

export interface Metric {
  value: number;
  viewValue: string;
}

export interface StateSummary {
  fips: string,
  state: string,
  riskLevels: { [key: string]: number; }
}

export interface RiskLevels {
  overall: number,
  testPositivityRatio: number,
  caseDensity: number,
  infectionRate: number
}

@Injectable({
  providedIn: 'root',
})

export class MapService {
  constructor(private http: HttpClient) {}

  getLevels(): Observable<Level[]> {
    const levels: Level[] = [
      { value: 1, color: '#01d475' },
      { value: 2, color: '#fec900' },
      { value: 3, color: '#ff9501' },
      { value: 4, color: '#d9002b' },
      { value: 5, color: '#780019' }
    ];

    return of(levels);
  }

  getMetrics(): Observable<Metric[]> {
    const metrics: Metric[] = [
      { value: 1, viewValue: 'Overall Risk Level' },
      { value: 2, viewValue: 'Cases per 100k Level' },
      { value: 3, viewValue: 'Test Positivity Ratio Level' },
      { value: 4, viewValue: 'Infection Rate Level' }
    ];

    return of(metrics);
  }

  getTopographyData(): Observable<any> {
    //const dataURL = 'https://d3js.org/us-10m.v1.json';
    const dataURL = '/assets/data/topography/us-10m.v1.json';
    return this.http.get(dataURL);
  }

  getStatesSummary(): Observable<StateSummary[]> {
    //const dataURL = 'https://d3js.org/us-10m.v1.json';
    const dataURL = '/api/metrics/statesSummary';
    return this.http.get<StateSummary[]>(dataURL);
  }

  getStatesRiskLevels(level: number): Observable<any> {
    const dataURL = '/api/metrics/statesRiskLevels/' + level;
    return this.http.get(dataURL);
  }
}
