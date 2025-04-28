export interface Card {
    color: string;
    isMatched: boolean;
}

export interface Game {
    id: string;
    cards: Card[];
    score: number;
    isGameOver: boolean;
}
