import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { MapService } from '../map/map.service';
import { Level } from '../map/map.service';

@Component({
  selector: 'app-map-legend',
  templateUrl: './map-legend.component.html',
  styleUrls: ['./map-legend.component.css']
})

export class MapLegendComponent implements OnInit {
  @Output() levelsChanged = new EventEmitter<Level[]>();

  levels: Level[] = [];

  constructor(private mapService: MapService) { }

  ngOnInit(): void {
    this.initLevels();
  }

  initLevels(): void {
    this.mapService.getLevels().subscribe((levels: Level[]) => {
      this.levels = levels;
      this.levelsChanged.emit(levels);
    })
  }
}
