import { Component, ElementRef, OnInit } from '@angular/core';

import * as d3 from 'd3';
import * as topojson from 'topojson-client';

import { GeometryCollection } from 'topojson-specification';

import { Level, MapService } from './map.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent implements OnInit {
  private common: any = {
    svg: {},
    projection: {},
    topologyFeature: {},
    path: {},
  };

  private mapOptions: any = {
    containerSelector: '#map',
    defaultStateColor: '#ccc',
    strokeColor: '#BDBDBD',
    strokeWidth: '0.7',
    defaultFill: 'none',
    statesElementClass: 'states',
    padding: 50,
    widthProportion: 1.6,
    heightProportion: 1.6,
    getStateId: (fips: string) => {
      return 'fips' + fips;
    }
  };

  loading: boolean = false;
  levels: Level[] = [];

  constructor(private mapService: MapService, private element: ElementRef) {}

  onLevelsChange(levels: Level[]): void {
    this.levels = levels;
  }

  onMetricChange(value: number): void {
    this.loading = true;

    this.mapService.getStatesRiskLevels(value).subscribe((data: any) => {
      this.renderStatesData(data);
      this.loading = false;
    });
  }

  ngOnInit(): void {
    this.initMap();
  }

  initMap(): void {
    this.mapService.getTopographyData().subscribe((topography: any) => {
      this.draw(topography);
    });
  }

  draw(topography: any): void {
    const { width, height } = this.getMapContainerSize();

    this.common.svg = d3.select(this.mapOptions.containerSelector).append('svg')
      .attr('width', width)
      .attr('height', height);

    this.common.topologyFeature = topojson.feature(
      topography,
      topography.objects.states as GeometryCollection
    );

    this.common.projection = d3.geoIdentity()
      .fitSize([width, height], this.common.topologyFeature);

    this.common.path = d3.geoPath().projection(this.common.projection);

    this.renderStates();

    d3.select(window).on('resize', this.resizeMap);
  }

  renderStates(): void {
    this.common.svg
      .append('g')
      .attr('class', this.mapOptions.statesElementClass)
      .attr('stroke', this.mapOptions.strokeColor)
      .attr('stroke-width', this.mapOptions.strokeWidth)
      .selectAll('path.state')
      .data(this.common.topologyFeature.features)
      .join('path')
      .attr('id', (d: any) => this.mapOptions.getStateId(d.id))
      .attr('fill', this.mapOptions.defaultFill)
      .attr('d', this.common.path);
  }

  renderStatesData(data: any): void {
    for (const fips in data) {
      const value = data[fips].value;
      const fill = this.levels.find((level) => { return level.value == value; })?.color ?? this.mapOptions.defaultStateColor;
      d3.select('path#' + this.mapOptions.getStateId(fips)).attr('fill', fill);
    }
  }

  resizeMap = () => {
    const { width, height } = this.getMapContainerSize();

    this.common.projection.fitSize([width, height], this.common.topologyFeature);

    this.common.svg.attr('width', width + this.mapOptions.padding).attr('height', height);
    this.common.svg.selectAll('path').attr('d', this.common.path);
  };

  getMapContainerSize = (): { width: number; height: number } => {
    const mapContainerElement = this.element.nativeElement.querySelector(this.mapOptions.containerSelector) as HTMLDivElement;
    const width = mapContainerElement.clientWidth / this.mapOptions.widthProportion;
    const height = width / this.mapOptions.heightProportion;

    return { width, height };
  };
}
