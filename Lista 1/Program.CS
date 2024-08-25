using System;

class JogoDaVelhaMinimax
{
    static char[,] tabuleiro = {
        {' ', ' ', ' '},
        {' ', ' ', ' '},
        {' ', ' ', ' '}
    };

    static char jogador = 'X';       // PC joga com 'X'
    static char adversario = 'O';    // player joga com 'O'

    static void Main(string[] args)
    {
        // Jogo inicial
        while (true)
        {
            ExibirTabuleiro();
            if (jogador == 'X')
            {
                // Jogada do PC
                (int x, int y) = MelhorMovimentoMinimax();
                tabuleiro[x, y] = jogador;
                Console.WriteLine($"Computador escolheu: {x}, {y}");
            }
            else
            {
                // Jogada de player
                JogadaDoJogador();
            }

            // Verifica vitória ou empate
            if (VerificaVitoria(jogador))
            {
                ExibirTabuleiro();
                Console.WriteLine($"{jogador} venceu!");
                break;
            }
            else if (TabuleiroCheio())
            {
                ExibirTabuleiro();
                Console.WriteLine("Empate!");
                break;
            }

            // Alterna o jogador
            jogador = (jogador == 'X') ? 'O' : 'X';
        }
    }

    static void ExibirTabuleiro()
    {
        Console.WriteLine("---------");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(tabuleiro[i, j] + " ");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("---------");
    }

    static void JogadaDoJogador()
    {
        int x, y;
        do
        {
            Console.WriteLine("Digite sua jogada (linha e coluna): ");
            x = int.Parse(Console.ReadLine());
            y = int.Parse(Console.ReadLine());
        } while (tabuleiro[x, y] != ' ' || x < 0 || x >= 3 || y < 0 || y >= 3);

        tabuleiro[x, y] = jogador;
    }

    static bool VerificaVitoria(char p)
    {
        for (int i = 0; i < 3; i++)
        {
            if (tabuleiro[i, 0] == p && tabuleiro[i, 1] == p && tabuleiro[i, 2] == p)
                return true;
            if (tabuleiro[0, i] == p && tabuleiro[1, i] == p && tabuleiro[2, i] == p)
                return true;
        }
        if (tabuleiro[0, 0] == p && tabuleiro[1, 1] == p && tabuleiro[2, 2] == p)
            return true;
        if (tabuleiro[0, 2] == p && tabuleiro[1, 1] == p && tabuleiro[2, 0] == p)
            return true;
        return false;
    }

    static bool TabuleiroCheio()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (tabuleiro[i, j] == ' ')
                    return false;
        return true;
    }

    static int Minimax(bool isMaximizing)
    {
        if (VerificaVitoria(jogador)) return 1;
        if (VerificaVitoria(adversario)) return -1;
        if (TabuleiroCheio()) return 0;

        if (isMaximizing)
        {
            int melhorPontuacao = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        tabuleiro[i, j] = jogador;
                        int pontuacao = Minimax(false);
                        tabuleiro[i, j] = ' ';
                        melhorPontuacao = Math.Max(pontuacao, melhorPontuacao);
                    }
                }
            }
            return melhorPontuacao;
        }
        else
        {
            int melhorPontuacao = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        tabuleiro[i, j] = adversario;
                        int pontuacao = Minimax(true);
                        tabuleiro[i, j] = ' ';
                        melhorPontuacao = Math.Min(pontuacao, melhorPontuacao);
                    }
                }
            }
            return melhorPontuacao;
        }
    }

    static (int, int) MelhorMovimentoMinimax()
    {
        int melhorPontuacao = int.MinValue;
        int melhorX = -1;
        int melhorY = -1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] == ' ')
                {
                    tabuleiro[i, j] = jogador;
                    int pontuacao = Minimax(false);
                    tabuleiro[i, j] = ' ';
                    if (pontuacao > melhorPontuacao)
                    {
                        melhorPontuacao = pontuacao;
                        melhorX = i;
                        melhorY = j;
                    }
                }
            }
        }

        return (melhorX, melhorY);
    }
}
