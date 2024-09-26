using System;
class Program
{
    static Random random = new Random();
    static void Main(string[] args)
    {
        int playerHealth = 100;
       
        int enemyCount = 1;

        Console.WriteLine("Auto Battler Game");
        Console.WriteLine("Press any key to start a battle. Press 'q' to quit.");

        while (true)
        {
            var key = Console.ReadKey(true).KeyChar;
            Console.Clear();
            if (key == 'q' || key == 'Q')
                break;

            string weather = GetRandomWeather();  //fun little statments to add flavour to the game
            if (weather == "Sunny")
            {
                Console.WriteLine("The Bright sun fills you with energy");
            }
            if (weather ==  "Rainy")
            {
                Console.WriteLine("The heavy rain makes it harder to hold your weapon");
            }
            if (weather == "Foggy")
            {
                Console.WriteLine("The thick fog makes it easier to surprise your enemies");
            }
            if (weather == "Windy")
            {
                Console.WriteLine("The Wind is blowing into your enemies eyes use it to avoid damage");
            }

            int enemyHealth = 50 + (enemyCount * 10);
            int enemyAttack = 5 + enemyCount;

            Console.WriteLine($"\nBattle {enemyCount} start! Player HP {playerHealth} Enemy HP {enemyHealth}");

            while (playerHealth > 0 && enemyHealth > 0)
            {
                // Player attacks
                int damage = DnDDamageDice(weather);
                enemyHealth -= damage;
                Console.WriteLine($"Player deals {damage} damage.                       Enemy HP: {Math.Max(enemyHealth, 0)}");

                System.Threading.Thread.Sleep(1000);
                if (enemyHealth <= 0) break;

                // Enemy attacks
                damage = updatedenemydmg(enemyAttack, weather);
                playerHealth -= damage;
                Console.WriteLine($"                   Enemy deals {damage} damage.     Player HP: {Math.Max(playerHealth, 0)}");

                System.Threading.Thread.Sleep(1000); // Slow down the battle for readability
            }

            if (playerHealth <= 0)
            {
                Console.WriteLine("Game Over! You were defeated.");
                break;
            }
            else
            {
                Console.WriteLine($"You defeated enemy {enemyCount}!");
                enemyCount++;
                playerHealth = Math.Min(playerHealth + 20, 100); // Heal a bit after each battle
                Console.WriteLine($"You sit and rest to heal your wounds... You now have {playerHealth} HP!");
                Console.WriteLine("When you're ready press any key to enter the next battle");
            }
        }
        Console.ReadKey(true);
    }

    static int DnDDamageDice(string weather)
    {
        int baseDamage = random.Next(2, 13);  //stats are based off the Greatsword from dnd 5e 

        if (weather == "Sunny" )  //sunny adding 1 dmg
        { baseDamage += 1;  }
        else if (weather == "Rainy")                                                            
        { baseDamage = Math.Max(baseDamage - 1, 0); }  // was making my dmg go negative with { baseDamage -= 1; }
        int crit = 5;  // a d20 in dnd has a 5% chance to crit 
        if (weather == "Foggy") {  crit *= 2 ; }

            if (random.Next(100) < crit)     
        {
            Console.WriteLine ("CRITICAL HIT!!!!( •̀ ω •́ )✧");  // the art doesnt work in the console but it makes it have a funny ding noise is ill keep it in 
            return baseDamage*2 ;
        }
        return baseDamage;
    }

    static int updatedenemydmg(int baseAttack, string weather)
    {
           if (weather == "Windy")
        { 
            baseAttack = Math.Max(baseAttack - 1, 0);
        }
        return random.Next(Math.Max(baseAttack - 2, 1), baseAttack + 3);  // so no negative dmg 

    

    }
    static string GetRandomWeather()
    {
        string[] weathers = { "Sunny", "Rainy", "Windy", "Foggy" }; //weather system insipred by pokemon
        return weathers[random.Next(weathers.Length)];
    }


}

