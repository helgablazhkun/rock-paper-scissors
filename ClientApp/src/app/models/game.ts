export type GameEvent = 'WaitingForSecondPlayerToJoin'
        | 'StartTheGame'
        | 'WaitingForPlayerToPlay'
        | 'RoundEnd'
        | 'GameEnd'
        | 'AskServerResponse';

export type GameStatus = 'NotStarted'
        | 'Initiated'
        | 'InProgress'
        | 'Finished';

export type Weapon = 'Rock'
        | 'Paper'
        | 'Scissors';

export type GameResult = 'Win'
        | 'Lose'
        | 'Draw';

export interface Game {
  status: GameStatus;
  roundResult: GameResult | null;
  gameResult: GameResult | null;
  round: number;
}

export interface GameSession {
  sessionId: string;
  nick: string;
  weaponRound1: Weapon;
  weaponRound2: Weapon;
  weaponRound3: Weapon;
  weaponRound4: Weapon;
  weaponRound5: Weapon;
}

export interface FinalRoundResult {
  playerWeapon: Weapon;
  opponentWeapon: Weapon;
  playerResult: GameResult;
 }

export interface FinalGameStatistic {
  playerNick: string;
  opponentNick: string;
  playerFinalResult: GameResult;
  round1Result: FinalRoundResult;
  round2Result: FinalRoundResult;
  round3Result: FinalRoundResult;
  round4Result: FinalRoundResult;
  round5Result: FinalRoundResult;
}
