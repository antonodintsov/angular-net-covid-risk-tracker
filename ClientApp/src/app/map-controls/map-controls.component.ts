import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { MapComponent } from '../map/map.component';

import { MapService } from '../map/map.service';
import { Metric } from '../map/map.service';

@Component({
  selector: 'app-map-controls',
  templateUrl: './map-controls.component.html',
  styleUrls: ['./map-controls.component.css']
})

export class MapControlsComponent implements OnInit {
  @Output() metricChanged = new EventEmitter<number>();

  metrics: Metric[] = [];

  @Input() loading: boolean = false;

  private _metricSelected: number = 0;
  set metricSelected(value: number) {
    if (this._metricSelected !== value) {
      this.metricChanged.emit(value);
    }

    this._metricSelected = value;
  }
  get metricSelected(): number {
    return this._metricSelected;
  }

  constructor(private mapService: MapService) { }

  ngOnInit(): void {
    this.initMetrics();
  }

  initMetrics(): void {
    this.mapService.getMetrics().subscribe((metrics: Metric[]) => {
      this.metrics = metrics;
    })
  }
}
