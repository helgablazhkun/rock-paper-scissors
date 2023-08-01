import { Component, Input } from '@angular/core';
import { FinalGameStatistic } from '../models/game';

@Component({
  selector: 'app-game-statistic',
  templateUrl: './game-statistic.component.html'
})
export class GameStatisticComponent {

  @Input() gameStatistic!: FinalGameStatistic;
  constructor() {
  }
}