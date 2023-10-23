namespace DesafioExtraJogodaVelha
{
    public class Program
    {
        static char[,] board = new char[3, 3];
        static char CurrentPlayer = 'X';
        static bool ActiveGame = true;

        static void Main(string[] args)
        {
            ShowWelcomeScreen();

            Game();

        }

        static void Game()
        {
            InitializeBoard(); //O Tabuleiro inicializa.

            //MakeAIPlay(); //A IA faz a jogada dela.

            while (ActiveGame)
            {
                ShowBoard();

                if (CurrentPlayer == 'X')
                {
                    MakeAIPlay();
                }
                else
                {
                    MakeMove();
                }

                if (WinCheck())
                {
                    ActiveGame = false;
                }

                else if (DrawCheck())
                {
                    ActiveGame = false;
                }

                ChangePlayer();
            }
            Console.WriteLine("O Jogo terminou. Deseja jogar novamente? (s / n)");
            char answer = char.Parse(Console.ReadLine().ToUpper());

            if (answer == 'S')
            {
                ActiveGame = true;
                CurrentPlayer = 'X';
                Game();
            }

        }
        static void InitializeBoard() //Função que vai preparar o tabuleiro para um novo jogo.
        {
            for (int i = 0; i < 3; i++) // 'i' são as linhas do tabuleiro e é o contador que começa em 0 e vai até 2 (representando as linhas 0, 1 e 2)
            {
                for (int j = 0; j < 3; j++) // 'j' são as colunas do tabuleiro e, assim como 'i',  é o contador que começa em 0 e vai até 2 (representando as colunas 0, 1 e 2)
                {
                    board[i, j] = ' ';
                }
            } // Ao final dos dois loops, o tabuleiro será completamente preenchido com espaços em branco, representando um tabuleiro vazio e pronto para um novo jogo
        }
        static void ShowBoard()
        {
            Console.Clear();

            Console.WriteLine("   0   1   2");         // Vai exibir os números 0, 1 e 2, representando as colunas do tabuleiro. Vai servir como rótulo das colunas.

            Console.WriteLine("  +---+---+---");

            for (int i = 0; i < 3; i++)              // Inicia um loop que irá iterar sobre as linhas do tabuleiro.
            {
                Console.Write(i + " | ");

                for (int j = 0; j < 3; j++)         //Irá iterar sobre as colunas do tabuleiro.
                {
                    Console.Write(board[i, j] + " | ");         //Representa o estado atual do jogo (X, O ou espaço vazio).
                }
                Console.WriteLine();

                Console.WriteLine("  +---+---+---");
            }
        }
        static void MakeMove()
        {
            int line, Column;
            do
            {
                Console.WriteLine("Jogador " + CurrentPlayer + ", insira a linha (0-2) e a coluna (0-2), separadas por espaço: ");
                string[] input = Console.ReadLine().Split(' ');
                // Pede ao jogador para inserir a linha e coluna onde ele deseja fazer a jogada. O CurrentPlayer é usado para indicar qual jogador está jogando no momento (X ou O).

                line = int.Parse(input[0]);
                Column = int.Parse(input[1]);

            } while (!ValidPosition(line, Column) || !EmptyPosition(line, Column));
            //A condição verifica se a posição (linha e coluna) inserida pelo jogador é válida e se está vazia.
            //A função ValidPosition verifica se a posição está dentro dos limites do tabuleiro (0 a 2) e a função EmptyPosition verifica se a posição está vazia (ou seja, se não foi ocupada por X ou O).

            board[line, Column] = CurrentPlayer; //Garantindo  que a posição inserida pelo jogador é válida e vazia, a jogada é registrada no tabuleiro, colocando o símbolo do jogador atual (X ou O) na posição indicada.
        }

        static bool ValidPosition(int line, int column) //Essa parte vai verificar se a posição é uma posição válida.
        {
            return (line >= 0 && line < 3 && column >= 0 && column < 3); // Todas as condições precisam ser verdadeiras para que a função retorne true. Prevenindo tentativas de acessar posições inexistentes.
        }
        static bool EmptyPosition(int line, int column) // Essa parte vai verificar se a posição é uma posição vazia.
        {
            return board[line, column] == ' '; // Retorna true se a posição especificada estiver vazia. 
        }
        static bool WinCheck(bool isAI = false) //Vai checar se há a vitória de um dos jogadores. (X ou O)
        {
            
            List<char> Players = new List<char> { 'X', 'O' };

            foreach (char p in Players)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (board[i, 0] == p && board[i, 1] == p && board[i, 2] == p ||
                        board[0, i] == p && board[1, i] == p && board[2, i] == p)
                    // Isso verifica se o jogador atual tem três símbolos consecutivos na mesma linha ou coluna.
                    {
                        ShowBoard();
                        Console.WriteLine("\nJogador " + p + " venceu!!"); //Se uma dessas condições for verdadeira, significa que o jogador atual venceu o jogo. O tabuleiro é exibido e uma mensagem de vitória é mostrada.
                        return true;
                    }
                }
                if (board[0, 0] == p && board[1, 1] == p && board[2, 2] == p || //Essa parte vai verificar as diagonais e se houver um vencedor, faz o mesmo que descrito acima.
                    board[0, 2] == p && board[1, 1] == p && board[2, 0] == p)
                {
                    ShowBoard();
                    Console.WriteLine("\nJogador " + p + " venceu!!");
                    return true;
                }
            }
            return false;
        }
        static bool DrawCheck() //Vai verificar se o jogo termina em empate.
        {
            if (!ActiveGame) //Verifica se o jogo já está encerrado e acaba por aqui sem fazer mais verificações.
                return false;

            bool Draw = true;

            for (int i = 0; i < 3; i++) // Esses dois loops vão servir para verificar se ainda há lugares vazios. Se houver, o programa entende que o jogo ainda não acabou.
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        Draw = false;
                        break;
                    }
                }
            }

            if (Draw) //Após todas as verificações acima, e comprovando que "Draw = true", é mostrado na tela que o jogo empatou.
            {
                ShowBoard();
                Console.WriteLine("O jogo empatou!");
            }

            return Draw;
        }

        static int MelhorJogada()
        {
            int melhorValor = int.MinValue;
            int melhorLinha = -1;
            int melhorColuna = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'X';
                        int valorJogada = MelhorPosicao(board);
                        board[i, j] = ' ';

                        if (valorJogada > melhorValor)
                        {
                            melhorValor = valorJogada;
                            melhorLinha = i;
                            melhorColuna = j;
                        }
                    }
                }
            }
            int result = melhorLinha * 3 + melhorColuna;
            return result;
        }

        static int Minimax()
        { 
            if (WinCheck(true))
            {
                return 1;
            }

            if (DrawCheck())
            {
                return 0;
            }
                return -1;
        
        }

        static int MelhorPosicao(char[,] estado )
        {
            int melhorValor = int.MinValue;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (estado[i, j] == ' ')
                    {
                        estado[i, j] = 'X';
                        int valorJogada = Minimax();
                        estado[i, j] = ' ';
                        melhorValor = Math.Max(melhorValor, valorJogada);
                    }
                }
            }

            return melhorValor;
        }

        static void MakeAIPlay() // Fazendo o computador fazer jogadas.
        {
            int melhorJogada = MelhorJogada();
            int linha = melhorJogada / 3;
            int coluna = melhorJogada % 3;
            board[linha, coluna] = 'X';
        }
        static void ChangePlayer()  //Vai alternar o jogador atual entre "X" e "O" após cada jogada.
        {
            _ = CurrentPlayer == char.Parse("X") ? CurrentPlayer = char.Parse("O") : CurrentPlayer = char.Parse("X"); //Vai realizar as verificações e fazer a troca dos jogadores.
        }
        static void ShowWelcomeScreen()             //Essa parte é a estilização e apresentação do jogo.
        {
            Console.Clear();

            string mensagem = "Bem-vindo ao Jogo da Velha!";
            int espacos = Console.WindowWidth / 2 - mensagem.Length / 2;

            string asciiArt = @"
 _       __________    __________  __  _________     ____ 
| |     / / ____/ /   / ____/ __ \/  |/  / ____/  _ / __ \
| | /| / / __/ / /   / /   / / / / /|_/ / __/    (_) / / /
| |/ |/ / /___/ /___/ /___/ /_/ / /  / / /___   _ / /_/ / 
|__/|__/_____/_____/\____/\____/_/  /_/_____/  (_)_____/  
                                                          ";

            int espacosAscii = Console.WindowWidth / 2 - asciiArt.Split('\n')[0].Length / 2;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string(' ', espacos) + mensagem);
            Console.WriteLine(new string(' ', espacosAscii) + asciiArt);
            Console.ResetColor();

            Console.WriteLine("\nPressione qualquer tecla para começar...");
            Console.ReadKey();
        }
    }
}



