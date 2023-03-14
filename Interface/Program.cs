using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static System.Console;


namespace House
{

    interface IWorker
    {
        string Name { get; }
    }
    interface IPart
    {
        void Do(House house);
    }

    class Basement : IPart
    {
        public void Do(House house)
        {
            house.basement = new Basement();
        }
    }

    class Walls : IPart
    {
        public void Do(House house)
        {
            house.walls.Add(new Walls());
        }
    }

    class Door : IPart
    {
        public void Do(House house)
        {
            house.door = new Door();
        }
    }

    class Window : IPart
    {
        public void Do(House house)
        {
            house.window.Add(new Window());
        }
    }

    class Roof : IPart
    {
        public void Do(House house)
        {
            house.roof = new Roof();
        }
    }

    class House
    {
        public Basement basement;
        public List<Walls> walls;
        public List<Window> window;
        public Door door;
        public Roof roof;

        public void Paint(TeamLeader t)
        {
            if (t.report.Count == 11)
            {

                string domik = @"
                	
                           o
                       _---|         _ _ _ _ _
                    o   ---|     o   ]-I-I-I-[
   _ _ _ _ _ _  _---|      | _---|    \ ` ' /
   ]-I-I-I-I-[   ---|      |  ---|    |.   |
    \ `   '_/       |     / \    |    | /^\|
     [*]  __|       ^    / ^ \   ^    | |*||
     |__   ,|      / \  /    `\ / \   | ===|
  ___| ___ ,|__   /    /=_=_=_=\   \  |,  _|
  I_I__I_I__I_I  (====(_________)___|_|____|____
  \-\--|-|--/-/  |     I  [ ]__I I_I__|____I_I_|
   |[]      '|   | []  |`__  . [  \-\--|-|--/-/
   |.   | |' |___|_____I___|___I___|---------|
  / \| []   .|_|-|_|-|-|_|-|_|-|_|-| []   [] |
 <===>  |   .|-=-=-=-=-=-=-=-=-=-=-|   |    / \
 ] []|`   [] ||.|.|.|.|.|.|.|.|.|.||-      <===>
 ] []| ` |   |/////////\\\\\\\\\\.||__.  | |[] [
 <===>     ' ||||| |   |   | ||||.||  []   <===>
  \T/  | |-- ||||| | O | O | ||||.|| . |'   \T/
   |      . _||||| |   |   | ||||.|| |     | |
../|' v . | .|||||/____|____\|||| /|. . | . ./
.|//\............/...........\........../../\\\";

                Console.WriteLine(domik);
            }
            else WriteLine("Дом еще не построен...");
        }
    }

    class Team : IWorker
    {
        public TeamLeader t;
        public List<Worker> w;
        public string Name { get => "Скрепыши"; }

        public Team()
        {
            t = new TeamLeader("Глав. Скрепыш");
            w = new List<Worker> { new Worker("Крепышонок"), new Worker("Серепышесса"), new Worker("Крепышундель"), new Worker("Скрепка") };
        }


    }

    class Worker : IWorker
    {
        string Name { get; set; }

        string IWorker.Name => Name;

        public Worker(string name)
        { Name = name; }

        public void Build(House house, TeamLeader t)
        {
            if (house.basement == null)
            {
                Basement basement = new Basement();
                basement.Do(house);
                t.report.Add($"Строитель {Name} строит подвал!");
            }
            else if (house.walls == null || house.walls.Count < 4)
            {
                if (house.walls == null) house.walls = new List<Walls>();
                Walls wall = new Walls();
                wall.Do(house);
                t.report.Add($"Строитель {Name}строит стену {house.walls.Count}!");
            }
            else if (house.door == null)
            {
                Door door = new Door();
                door.Do(house);
                t.report.Add($"Строитель {Name} ставит дверь!");

            }

            else if (house.window == null || house.window.Count < 4)
            {
                if (house.window == null) house.window = new List<Window>();
                Window window = new Window();
                window.Do(house);
                t.report.Add($"Строитель {Name} ставит окно {house.window.Count}!");

            }

            else if (house.roof == null)
            {
                Roof roof = new Roof();
                roof.Do(house);
                t.report.Add($"Строитель {Name} строит крышу!");

            }

        }
    }

    class TeamLeader : IWorker
    {
        string Name { get; set; }
        public List<string> report = new List<string>();
        string IWorker.Name => Name;

        public TeamLeader(string name)
        { Name = name; }
        public void Report()
        {
            double d = (report.Count / 11.0) * 100;
            WriteLine($"{(int)d} % работы сделано!");
        }
    }

    class Program
    {
        static void Main()
        {
            House house = new House();
            Team team = new Team();

            WriteLine(team.Name);

            Random r = new Random();

            for (int i = 0; i < 6; i++)
            {
                team.w[r.Next(0, 3)].Build(house, team.t);
            }

            foreach (var a in team.t.report)
            {
                WriteLine(a);
            }

            team.t.Report();
            WriteLine();
            for (int i = 0; i < 5; i++)
            {
                team.w[r.Next(0, 3)].Build(house, team.t);
            }

            foreach (var a in team.t.report)
            {
                WriteLine(a);
            }
            team.t.Report();

            house.Paint(team.t);
            ReadKey();
        }
    }
}