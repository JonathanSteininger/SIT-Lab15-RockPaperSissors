using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockPaperScissorsLib;

namespace lab15_RockPaperSissors
{
    internal class Application
    {
        public Network network { get; set; }
        public PlayerInfo player { get; set; }

        public Application(Network network, PlayerInfo player)
        {
            this.network = network;
            this.player = player;
            if (!network.Connected) network.Connect();
            network.Send(player);
            Update();
        }

        private void Update()
        {
            string input;
            writeHelp();
            while((input = Console.ReadLine()).ToUpper() != "Q")
            {
                if (input.Length < 1) continue;
                char command = input.ToUpper()[0];
                GameInput(command);
                writeHelp();
            }
        }
        private void writeHelp() => Console.WriteLine("Q = quit; R = Rock; P = Paper; S = Sissor");

        private async void GameInput(char input)
        {
            RoundAction action;
            switch (input)
            {
                case 'R':
                    action = RoundAction.Rock; 
                    break;
                case 'P':
                    action = RoundAction.Paper;
                    break;
                case 'S':
                    action = RoundAction.Scissors;
                    break;
                case 'Q':
                    action = RoundAction.Quit;
                    break;
                default: return;
            }
            GameCmd command = new GameCmd(action);
            if (action == RoundAction.Quit)
            {
                network.Send(command);
                await Task.Delay(100);
                return;
            }
            GameMessage m = network.Request<GameMessage>(command);
            Console.WriteLine($"You chose {action}");
            Console.WriteLine($"Your opponent chose {m.OpponentAction}");
            Console.WriteLine($"{m.Message}");
            Console.WriteLine($"You {m.Result}");
            Console.WriteLine($"Your Score: {m.PlayerScore}, Your Opponents score: {m.OpponentScore}\n");

        }

        

        public Application(string ip, int port, PlayerInfo player) : this(new Network(ip, port), player) { }
        public Application(string ip, int port, string playerName) : this(ip, port, new PlayerInfo(playerName)) { }


    }
}
