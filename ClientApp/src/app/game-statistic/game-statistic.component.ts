import { Component, Inject, Input } from '@angular/core';
import { GameStatistic } from '../models/game';

@Component({
  selector: 'app-game-statistic',
  templateUrl: './game-statistic.component.html'
})
export class GameStatisticComponent {

  @Input() gameStatistic!: GameStatistic;
  constructor() {
  }
}