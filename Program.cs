using System;
using System.Linq;

/*
TODO:
1. Fix bug, when the player completes requirements for levels > 0 
and only then completes level 0, 
the program doesn't congrat player with all the other levels, 
that he/she completed(though it congrats after next turns)

2. "Change Clicker" to "Math quiz" or smth like that
*/

namespace Robot {
    class Program {
        static void Main() {
            string option;
            string temp;
            int balance = 10000;
            
            int arena_matches = 0;
            int arena_matches_won = 0;
            int arena_bets = 0;
            int arena_bets_won = 0;
            int level = 0;

            int new_level_bets = 0;
            int new_level_matches = 0;

            Robot[] robots_from_shop = {Robot.random_robot(), Robot.random_robot(), Robot.random_robot()};
            Robot enemy = Robot.random_robot();
            Robot[] arena_robots = {Robot.random_robot(), Robot.random_robot()};
            Robot owned_robot = new Robot("Beginner's bot", 45, 200, 7);
            
            while (true) {
                
                Console.WriteLine("0 - Shop");
                Console.WriteLine("1 - Arena");
                Console.WriteLine("2 - Your robot");
                Console.WriteLine("3 - Statistic");
                Console.WriteLine("4 - Levels");
                Console.WriteLine("5 - See your balance");
                Console.WriteLine("6 - Clicker");
                Console.WriteLine("7 - Exit");
                Console.Write("Choose an option: ");
                option = Console.ReadLine();
                
                Console.WriteLine();
                switch (option) {
                    case "0":
                        for (byte index = 0; index < 3; index++) {
                            Console.WriteLine("\nBot number " + Convert.ToString(index + 1) + ":");
                            robots_from_shop[index].describe();
                            Console.WriteLine($"Price - {robots_from_shop[index].price}");
                        }

                        Console.Write("Choose bot to buy: ");
                        try {
                            option = Console.ReadLine();
                            if (!(new byte[] {1, 2, 3}).Contains(Convert.ToByte(option))) {
                                throw new ArgumentOutOfRangeException("Argument is out of range.");
                            } else {
                                if (balance >= robots_from_shop[Convert.ToInt32(option) - 1].price) {
                                    balance -= robots_from_shop[Convert.ToInt32(option) - 1].price;
                                    balance += Convert.ToInt16(owned_robot.price * 0.75f);
                                    owned_robot = robots_from_shop[Convert.ToInt32(option) - 1];
                                    robots_from_shop[Convert.ToInt32(option) - 1] = Robot.random_robot();
                                    Console.WriteLine($"{owned_robot.name} is succesfully bought!");
                                } else {
                                    Console.WriteLine("Not enough money :(");
                                }
                            }
                        } catch {
                            Console.WriteLine("Invalid option");
                        }
                        break;

                    case "1":
                        Console.Write("1 - Fight on arena\n2 - Make a bet\nChoose an option: ");
                        option = Console.ReadLine();

                        switch (option) {
                            case "1":
                                Console.WriteLine("Here's your opponent!");
                                enemy.describe();
                                Console.Write("Ready to fight(y/n)? ");
                                option = Console.ReadLine();
                                if (option == "y") {
                                    if (owned_robot.power > enemy.power) {
                                        balance += Convert.ToInt32(enemy.price / 4.0);
                                        Console.WriteLine($"You won and got {enemy.price / 4.0}");
                                        enemy = Robot.random_robot();
                                        arena_matches_won++;
                                    } else {
                                        Console.WriteLine("You lose! Try again later!");
                                    }
                                    arena_matches++;
                                }
                                break;

                            case "2":
                                Console.Write("And your bet is? ");
                                option = Console.ReadLine();
                                try {
                                    if (Convert.ToInt32(option) > balance) {
                                        Console.WriteLine("Not enough money :(");
                                    } else {
                                        Console.WriteLine("Robot 1:");
                                        arena_robots[0].describe();
                                        Console.WriteLine("\nRobot 2:");
                                        arena_robots[1].describe();
                                        Console.Write("Which robot is gonna win? ");
                                        temp = Console.ReadLine();
                                        switch (temp) {
                                            case "1":
                                                if (arena_robots[0].power > arena_robots[1].power) {
                                                    balance += Convert.ToInt32(option) / 2;
                                                    Console.WriteLine("You won!");
                                                    arena_bets_won++;
                                                    arena_bets++;
                                                } else {
                                                    balance -= Convert.ToInt32(option);
                                                    Console.WriteLine("You lose!");
                                                    arena_bets++;
                                                }
                                                break;

                                            case "2":
                                                if (arena_robots[0].power < arena_robots[1].power) {
                                                    balance += Convert.ToInt32(option) / 2;
                                                    Console.WriteLine("You won!");
                                                    arena_bets_won++;
                                                    arena_bets++;
                                                } else {
                                                    balance -= Convert.ToInt32(option);
                                                    Console.WriteLine("You lose!");
                                                    arena_bets++;
                                                }
                                                break;

                                            default:
                                                Console.WriteLine("Invalid option");
                                                break;
                                        }
                                    }
                                    arena_robots = new Robot[] {Robot.random_robot(), Robot.random_robot()};
                                    break;
                                } catch {
                                    Console.WriteLine("Invalid option");
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;

                    case "2":
                        owned_robot.describe();
                        Console.WriteLine($"Price - {owned_robot.price}");
                        break;

                    case "3":
                        Console.WriteLine($"Arena matches ever: {arena_matches}");
                        Console.WriteLine($"Arena matches won: {arena_matches_won}");
                        Console.WriteLine($"\nArena bets ever: {arena_bets}");
                        Console.WriteLine($"Arena bets won: {arena_bets_won}");
                        break;

                    case "4":
                        if (level == 0) {
                            Console.WriteLine("Needed for level 1:\n\t - Buy a new robot");
                        } else {
                            Console.WriteLine($"You need for level {level + 1}:");
                            Console.WriteLine($"\t - Win {new_level_bets} bets");
                            Console.WriteLine($"\t - Win {new_level_matches} matches");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Your balance is " + Convert.ToString(balance));
                        break;

                    case "6":
                        Console.WriteLine("Welcome to clicker! If you ran out of money, you can easely earn it here! Just type <Enter> to earn 125 every 50 \"clicks\" and \"Exit\" to exit");
                        temp = "0";
                        option = "";
                        while (option != "Exit") {
                            option = Console.ReadLine();
                            temp = Convert.ToString(Convert.ToByte(temp) + 1);
                            if (temp == "50") {
                                temp = "0";
                                balance += 125;
                                Console.WriteLine("125 earned!");
                            }
                        }
                        break;

                    case "7":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid option: " + option);
                        break;
                }
                if (level == 0 && owned_robot.name != "Beginner's bot") {
                    level = 1;
                    Console.WriteLine("Congratulations! You've completed level 0!");
                    new_level_bets = Convert.ToInt32(Math.Pow(1.3, level)) + level;
                    new_level_matches = Convert.ToInt32(Math.Pow(1.05, level)) + level;
                } else if (level != 0 && new_level_bets <= arena_bets_won && new_level_matches <= arena_matches_won) {
                    Console.WriteLine($"Congratulations! You've completed level {level}!");
                    level++;
                    new_level_bets = Convert.ToInt32(Math.Pow(1.3, level)) + level;
                    new_level_matches = Convert.ToInt32(Math.Pow(1.05, level)) + level;
                }
                Console.WriteLine();
            }
        }
    }
}