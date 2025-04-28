import React, { useEffect, useState } from 'react';
import { startGame } from '../services/api';
import { Game } from '../types/game';

const StartPage: React.FC = () => {
    const [game, setGame] = useState<Game | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string>('');
    const [flippedIndexes, setFlippedIndexes] = useState<number[]>([]);
    const [matchedIndexes, setMatchedIndexes] = useState<number[]>([]);
    const [isGameOver, setIsGameOver] = useState<boolean>(false);
    const [isProcessing, setIsProcessing] = useState<boolean>(false);

    useEffect(() => {
        const fetchGame = async () => {
            try {
                const newGame = await startGame();
                setGame(newGame);
            } catch (err) {
                setError('Failed to start a new game.');
            } finally {
                setLoading(false);
            }
        };

        fetchGame();
    }, []);

    // ===== Manage card clicks =====
    const handleCardClick = (index: number) => {
        if (
            isProcessing ||
            flippedIndexes.length === 2 ||
            flippedIndexes.includes(index) ||
            matchedIndexes.includes(index)
        ) {
            return;
        }

        const newFlipped = [...flippedIndexes, index];
        setFlippedIndexes(newFlipped);

        if (newFlipped.length === 2) {
            const firstCard = game?.cards[newFlipped[0]];
            const secondCard = game?.cards[newFlipped[1]];

            if (firstCard && secondCard) {
                setIsProcessing(true);

                setTimeout(() => {
                    if (firstCard.color === secondCard.color) {
                        // MATCH!
                        setMatchedIndexes((prev) => [...prev, newFlipped[0], newFlipped[1]]);
                        setGame((prevGame) => prevGame ? { ...prevGame, score: prevGame.score + 1 } : null);
                        setFlippedIndexes([]);
                    } else {
                        // NO MATCH
                        setGame((prevGame) => prevGame ? { ...prevGame, score: prevGame.score - 1 } : null);
                    }


                    setFlippedIndexes([]);
                    setIsProcessing(false);
                }, 2000);
            }
        }
    };

    useEffect(() => {
        if (game && matchedIndexes.length === game.cards.length) {
            setIsGameOver(true);
        }
    }, [matchedIndexes, game]);

    const handleNewGame = async () => {
        setLoading(true);
        setIsGameOver(false);
        setFlippedIndexes([]);
        setMatchedIndexes([]);

        try {
            const newGame = await startGame();
            setGame(newGame);
        } catch (err) {
            setError('Failed to start a new game.');
        } finally {
            setLoading(false);
        }
    };

    if (loading) {
        return <p>Loading game...</p>;
    }

    if (error) {
        return <p>{error}</p>;
    }

    return (
        <div>
            {isGameOver && (
                <div style={{ marginBottom: '20px', textAlign: 'center' }}>
                    <h2>🎉 Congratulations! You found all pairs!</h2>
                    <button onClick={handleNewGame} style={{ padding: '10px 20px', fontSize: '16px', cursor: 'pointer' }}>
                        Start New Game
                    </button>
                </div>
            )}
            <h1>🎨 Colour Memory Game</h1>
            <p>Game ID: {game?.id}</p>
            <p>Score: {game?.score}</p>

            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: '10px', marginTop: '20px' }}>
                {game?.cards.map((card, index) => {
                    const isFlipped = flippedIndexes.includes(index) || matchedIndexes.includes(index);
                    const isMatched = matchedIndexes.includes(index);

                    return (
                        <div
                            key={index}
                            onClick={() => handleCardClick(index)}
                            style={{
                                width: '80px',
                                height: '80px',
                                backgroundColor: isMatched ? '#4CAF50' : (isFlipped ? card.color : '#999'),
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center',
                                fontSize: '20px',
                                color: isFlipped || isMatched ? 'white' : 'transparent',
                                cursor: 'pointer',
                                borderRadius: '8px',
                                transition: 'background-color 0.5s, transform 0.5s',
                                fontWeight: 'bold'
                            }}
                        >
                            {isFlipped || isMatched ? (isMatched ? '✔️' : card.color) : ''}
                        </div>
                    );
                })}

            </div>
        </div>
    );
};

export default StartPage;