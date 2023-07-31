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

export interface GameStatistic {
   playerResult: GameResult,
   playerSession: GameSession,
   opponentSession: GameSession
}
