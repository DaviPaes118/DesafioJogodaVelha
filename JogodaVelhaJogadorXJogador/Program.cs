namespace JogodaVelhaJogadorXJogador
{
    internal class Program
    {
        static char[,] Board = new char[3, 3];          //Aqui são declarações de variáveis globais. Onde eu defino que o jogo é uma matrix 3x3
        static char CurrentPlayer = 'X';                //E o jogador que for começar, sempre vai ser X
        static bool ActiveGame = true;

        static void Main(string[] args)
        {
            ShowWelcomeScreen();


            BoardInitialize();         //Essa função é chamada para o tabuleiro iniciar

            while (true)
            {
                ActiveGame = true;
                CurrentPlayer = 'X';
                BoardInitialize();

                while (ActiveGame)
                {
                    ShowBoard();                     //Essa função é responsável por mostrar o tabuleiro no console.

                    MakeMove();                      //Essa função é a responsável por fazer as jogadas. Aqui é definido se o jogador escolheu um espaço válido e disponível.

                    WinCheck();                     //Verificação de vitória.

                    DrawCheck();                   //Verificação de empate.

                    ChangePlayer();                //Muda o jogador automáticamente após o movimento do jogador anterior. De X para O e vice-versa.
                }

                Console.WriteLine("O jogo terminou, deseja reiniciar? (S / N)");
                char answer = Console.ReadKey().KeyChar;

                if (answer != 's' && answer != 'S')                      //Laço de repetição para que seja possível jogar novamente sem ter que fechar o programa.
                {
                    break;
                }
            }
        }
        static void BoardInitialize()                     //O tabuleiro é iniciado com espaços em branco.
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board[i, j] = ' ';
                }
            }
        }
        static void ShowBoard()                             //Aqui é a montagem do tabuleiro, com especificações das coordenadas e separações por "|".
        {                                                   //Também fiz algumas estilizações do tabuleiro para que fique mais atraente.
            Console.Clear();

            Console.WriteLine("   0   1   2");

            Console.WriteLine("  +---+---+---");

            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " | ");

                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Board[i, j] + " | ");
                }
                Console.WriteLine();

                Console.WriteLine("  +---+---+---");
            }
        }
        static void MakeMove()     // Essa função será responsável para validar os movimentos dos jogadores. Para ser considerado um movimento válido, o espaço preciso ser
        {                          // Vazio (sem nenhuma jogada anterior nesse mesmo espaço) e válido (Dentro das coordenadas do tabuleiro). 
            int line, column;
            do
            {
                Console.WriteLine("Jogador " + CurrentPlayer + ", insira a linha (0-2) e a coluna (0-2), separadas por espaço:");
                string[] input = Console.ReadLine().Split(' ');
                line = int.Parse(input[0]);
                column = int.Parse(input[1]);
            } while (!ValidPosition(line, column) || !EmptyPosition(line, column));

            Board[line, column] = (CurrentPlayer == 'X') ? 'X' : 'O';
        }
        static bool ValidPosition(int line, int column)                    //Aqui é onde ocorre a verificação se a posição escolhida é válida.
        {
            return line >= 0 && line < 3 && column >= 0 && column < 3;
        }
        static bool EmptyPosition(int line, int column)                    //E aqui é verificada se a posição escolhida está disponível, ou seja, vazia, para a jogada.
        {
            return Board[line, column] == ' ';
        }
        static void WinCheck()                          //Essa parte vai ocorrer a verificação se houve vitória de um dos jogadores. Cada vez que um movimento é feito,
        {                                               //O programa vai verificar se houve vitória ou não.
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == CurrentPlayer && Board[i, 1] == CurrentPlayer && Board[i, 2] == CurrentPlayer ||
                    Board[0, i] == CurrentPlayer && Board[1, i] == CurrentPlayer && Board[2, i] == CurrentPlayer)
                {
                    ShowBoard();
                    Console.WriteLine("Jogador " + CurrentPlayer + " venceu!");
                    ActiveGame = false;
                    break;
                }
            }
            if (Board[0, 0] == CurrentPlayer && Board[1, 1] == CurrentPlayer && Board[2, 2] == CurrentPlayer ||
            Board[0, 2] == CurrentPlayer && Board[1, 1] == CurrentPlayer && Board[2, 0] == CurrentPlayer)
            {
                ShowBoard();
                Console.WriteLine("Jogador " + CurrentPlayer + " venceu!");
                ActiveGame = false;
            }
        }
        static void DrawCheck()                           //Assim como na verificação de vitória, a cada jogada feita, o programa também verifica se houve empate.
        {
            if (!ActiveGame)
                return;

            bool Draw = true;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == ' ')
                    {
                        Draw = false;
                        break;
                    }
                }
            }

            if (Draw)
            {
                ShowBoard();
                Console.WriteLine("O jogo empatou!");
                ActiveGame = false;
            }
        }
        static void ChangePlayer()                          //Aqui é onde vai ocorrer a mudança instantânea e automática de jogador. Assim que um jogador fizer seu movimento,
        {                                                   //O programa muda o jogador automaticamente para a vez do outro jogador.
            CurrentPlayer = (CurrentPlayer == 'X') ? 'O' : 'X';
        }
        static void ShowWelcomeScreen()             //Essa parte é a estilização e apresentação do jogo.
        {
            Console.Clear();

            string mensagem = "Bem-vindo ao Jogo da Velha!";
            int espacos = Console.WindowWidth / 2 - mensagem.Length / 2;

            string asciiArt = @"        __ _                               
\ \      / /__| | ___ ___  _ __ ___   ___   _|  _ \ 
 \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \ / _ \ (_) | | |
  \ V  V /  __/ | (_| (_) | | | | | |  __/  _| |_| |
   \_/\_/ \___|_|\___\___/|_| |_| |_|\___| (_)____/ ";

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