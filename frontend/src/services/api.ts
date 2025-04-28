import axios from 'axios';
import { Game } from '../types/game';

const API_URL = 'https://localhost:7042/api/game';

export const startGame = async (): Promise<Game> => {
    const response = await axios.post<Game>(`${API_URL}/start`);
    return response.data;
};

export const getGame = async (id: string): Promise<Game> => {
    const response = await axios.get<Game>(`${API_URL}/${id}`);
    return response.data;
};

export const updateGame = async (id: string, game: Game): Promise<void> => {
    await axios.put(`${API_URL}/${id}`, game);
};
