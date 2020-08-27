using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            int round = 0;
            bool cpnt = false;
            bool ppnt = false;
            String coord;
            Boolean onGoing = true;
            Console.WriteLine("Welcome to battleship. Please type your name: ");
            String z = Console.ReadLine();
            Board prim = new Board(z);
            Console.WriteLine("Your pieces are located at: ");
            Console.WriteLine();
            while (onGoing)
            {
                round++;
                cpnt = false;
                ppnt = false;
                Thread.Sleep(300);
                prim.displayBoard();
                coord = "";
                Thread.Sleep(300);
                Console.WriteLine();
                Console.WriteLine("Type a coordinate to fire a shot");
                Console.WriteLine("Round " + round);
                Console.WriteLine();
                coord = Console.ReadLine();
                Console.WriteLine();
                String[] c2 = coord.Split(' ');
                if (prim.fire(c2)) {
                    Console.WriteLine("Hit");
                    ppnt = true;
                }
                else
                {
                    Console.WriteLine("Miss");
                }
                Console.WriteLine();
                Thread.Sleep(300);
                Console.Write("waiting for opponent");
                for(int i = 0; i < 6; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(75);                 
                }
                Console.WriteLine();
                Thread.Sleep(300);
                if (prim.cpuFire())
                    cpnt = true;
                Console.WriteLine();
                Thread.Sleep(300);
                Console.Write("calculating scores");
                for (int i = 0; i < 6; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(75);
                }
                Console.WriteLine(); Console.WriteLine();
                Console.Write("Round Results: ");
                if (cpnt && ppnt)
                    Console.Write("Draw, both you and your opponent scored");
                else if (cpnt && !ppnt)
                    Console.Write("Loss, your opponent scored but you did not");
                else if (!cpnt && ppnt)
                    Console.Write("Win, you scored whereas your opponent did not");
                else
                    Console.Write("Draw, neither you nor your opponent scored");
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine("Player_Score: " + prim.getPlayerPoints() + "\t || \t CPU_Score: " + prim.getCpuPoints());
                Console.WriteLine();
                for (int i = 0; i < 100; i++)
                {
                    Console.Write("-");
                    Thread.Sleep(5);
                }
                Console.WriteLine(); Console.WriteLine();
                if (!prim.cont())
                {
                    onGoing = false;
                    prim.winSeq();
                }
            }
        }
    }

    class Board
    {
        private String pName = "";
        private int cpuNum = 0;
        private int playerNum = 0;
        private Boolean[,] playerBoard;
        private Boolean[,] cpuBoard;
        private int playerPoints = 0;
        private int cpuPoints = 0;

        public Board(String x)
        {
            playerBoard = new Boolean[10, 10];
            cpuBoard = new Boolean[10, 10];
            pName = x;
            generateBoard();
        }

        public Boolean fire(String[] pos)
        {
            String a = pos[0].ToLower();
            int t = alphToInt(a);
            int t2 = int.Parse(pos[1]);
            if (getCpuBoard(t, t2))
            {
                setCpuBoard(t, t2, false);
                playerPoints++;
                return true;
            }
            else
                return false;
        }

        public Boolean cpuFire()
        {
            Random randy = new Random();
            int r1 = randy.Next(0, 9);
            int r2 = randy.Next(0, 9);

            if (getPlayerBoard(r1, r2))
            {
                Console.WriteLine("One of your ships was sunk");
                setPlayerBoard(r1, r2, false);
                cpuPoints++;
                return true;
            }
            else
                Console.WriteLine("Your opponent missed their shot");
                return false;
        }

        public int alphToInt(String x)
        {
            if (x.Equals("a"))
                return 0;
            else if (x.Equals("b"))
                return 1;
            else if (x.Equals("c"))
                return 2;
            else if (x.Equals("d"))
                return 3;
            else if (x.Equals("e"))
                return 4;
            else if (x.Equals("f"))
                return 5;
            else if (x.Equals("g"))
                return 6;
            else if (x.Equals("h"))
                return 7;
            else if (x.Equals("i"))
                return 8;
            else if (x.Equals("j"))
                return 9;
            else
                return -1;
        }

        public void generateBoard()
        {
            for(int i = 0; i < 10; i++)
            {
                Random rand = new Random();
                int r = rand.Next(0, 9);
                int r2 = rand.Next(0, 9);
                setPlayerBoard(i, r, true);
                setPlayerBoard(i, r2, true);
                if (r == r2)
                    incrPlayerNum(1);
                else
                    incrPlayerNum(2);
            }
            for (int i = 0; i < 10; i++)
            {
                Random rand = new Random();
                int r = rand.Next(0, 9);
                int r2 = rand.Next(0, 9);
                setCpuBoard(i, r, true);
                setCpuBoard(i, r, true);
                if (r == r2)
                    incrCpuNum(1);
                else
                    incrCpuNum(2);
            }
        }

        public void displayBoard()
        {
            Console.WriteLine("  A B C D E F G H I J");
            for(int i = 0; i < 10; i++)
            {
                String nextLine;
                nextLine = (i + 1).ToString() + " ";
                for(int j = 0; j < 10; j++)
                {
                    if (getPlayerBoard(i, j))
                        nextLine += "X ";
                    else
                        nextLine += "O ";
                }
                Console.WriteLine(nextLine);
            }
        }

        public void displayCpuBoard()
        {
            Console.WriteLine("This is your board");
            Console.WriteLine("  A B C D E F G H I J");
            for (int i = 0; i < 10; i++)
            {
                String nextLine;
                nextLine = (i + 1).ToString() + " ";
                for (int j = 0; j < 10; j++)
                {
                    if (getCpuBoard(i, j))
                        nextLine += "X ";
                    else
                        nextLine += "O ";
                }
                Console.WriteLine(nextLine);
            }
        }

        /*
         * private Boolean[,] playerBoard;
           private Boolean[,] cpuBoard;
           private int playerPoints = 0;
           private int cpuPoints = 0;
         */

        public int getPlayerPoints()
        {
            return playerPoints;
        }

        public int getCpuPoints()
        {
            return cpuPoints;
        }

        public void setPlayerBoard(int x, int y, Boolean z)
        {
            playerBoard[x, y] = z;
        }

        public void setCpuBoard(int x, int y, Boolean z)
        {
            cpuBoard[x, y] = z;
        }

        public Boolean getPlayerBoard(int x, int y)
        {
            return playerBoard[x, y];
        }

        public Boolean getCpuBoard(int x, int y)
        {
            return cpuBoard[x, y];
        }

        public int getCpuNum()
        {
            return cpuNum;
        }

        public int getPlayerNum()
        {
            return playerNum;
        }

        public void incrPlayerNum(int x)
        {
            playerNum += x;
        }

        public void incrCpuNum(int x)
        {
            cpuNum += x;
        }

        public String getPName()
        {
            return pName;
        }

        public Boolean cont()
        {
            if (playerPoints == cpuNum || cpuPoints == playerNum)
                return false;
            else
                return true;
        }

        public String getWinner()
        {
            if (playerPoints > cpuPoints)
                return getPName();
            else
                return ("cpu");
        }

        public void winSeq()
        {
            Console.WriteLine(); Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
                Thread.Sleep(5);
            }
            Console.Write("GAME-OVER");
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
                Thread.Sleep(5);
            }
            Console.WriteLine(); Console.WriteLine();
            if (getWinner().Equals(getPName()))
            {
                Console.WriteLine("Congrats " + getPName() + ", you sunk all of your opponent's battleships");
            }
            else
                Console.WriteLine("Sorry about the loss, better luck next time champ");

        }
    }
}
