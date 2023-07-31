import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Player } from '../models/player';
import { Game, Weapon, GameResult } from '../models/game';
import { GameService } from '../services/gameService';

@Component({
  selector: 'app-weapon',
  templateUrl: './weapon.component.html',
  styleUrls: ['./weapon.component.css']
})
export class WeaponComponent {
  @Output() shot = new EventEmitter();
  @Input() weapon!: Weapon;
  @Input() disabled = false;

  get icon(){
     switch (this.weapon) {
      case 'Rock':
        return 'bi bi-hand-thumbs-up-fill';
      case 'Scissors':
        return 'bi bi-scissors';

      default:
        return 'bi-file-earmark-fill';
     }
  }
}

