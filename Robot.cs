using System;

namespace Robot {
    class Robot {
        private static Random rnd = new Random();
        private static string[] random_name1 = {"Little", "Big", "Sneaky", "Mystery", "Confusing", "OP", ""};
        private static string[] random_name2 = {"Johny", "Tom", "Oleg", "BOT", "noob", "dummy"};
        
        public string name
        {get; set;}
        public short weight
        {get;}
        public short hp
        {get;}
        public short attack
        {get;}
        public int price
        {get;}
        
        public float power
        {get;}
        public Robot(string name, short weight, short hp, byte attack) {
            this.name = name;
            this.weight = weight;
            this.hp = hp;
            this.attack = attack;
            power = Convert.ToSingle(hp) / weight + attack;
            price = Convert.ToInt16(power) * 250 + Convert.ToInt16(rnd.Next(-8, 17) * 125);
        }

        public static Robot random_robot() {
            string random_name = random_name1[rnd.Next(random_name1.Length)];
            random_name += " " + random_name2[rnd.Next(random_name2.Length)];
            short random_weight = (short)rnd.Next(1, 50);
            short random_hp = (short)rnd.Next(100, 1000);
            byte random_attack = (byte)rnd.Next(5, 18);

            return new Robot(random_name, random_weight, random_hp, random_attack);
        }

        public void describe() {
            Console.WriteLine($"Name - {name}");
            Console.WriteLine($"Weight - {weight}");
            Console.WriteLine($"HP - {hp}");
            Console.WriteLine($"Attack - {attack}");
        }
    }
}