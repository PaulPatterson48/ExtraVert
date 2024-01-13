using ExtraVert;
using System.Collections.Generic;
using System.IO;


class Program
{
    static void Main()
    {
        List<Plant> plants = new List<Plant>
        {
            new Plant() { Species = "cactus", LightNeeds = 1, AskingPrice = 15.00M, City = "Austin", Sold = false, ZIP = "73301" },
            new Plant() { Species = "Rose", LightNeeds = 2, AskingPrice = 10.00M, City = "Dallas", Sold = true, ZIP = "75001" },
            new Plant() { Species = "Orchids", LightNeeds = 5, AskingPrice = 25.00M, City = "Nashville", Sold = false, ZIP = "37011" },
            new Plant() { Species = "lavender", LightNeeds = 3, AskingPrice = 17.00M, City = "Memphis", Sold = false, ZIP = "37501" },
            new Plant() { Species = "Jade plant", LightNeeds = 4, AskingPrice = 27.75M, City = "Chicago", Sold = false, ZIP = "60601" }
        };
        Console.WriteLine("Welcome to the ExtraVert Plant Adoption App!");        

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("a. Display all plants");
            Console.WriteLine("b. Post a plant to be adopted");
            Console.WriteLine("c. Adopt a plant");
            Console.WriteLine("d. Dlist a plant");
            Console.WriteLine("e. View Statistics");
            Console.WriteLine("f. Exit");

            Console.Write("Choose an option (a, b, c, d, e, f):");
            var choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            PlantofTheDay(plants);

            if (choice == 'a')
            {
                DisplayAllPlants(plants);
            }
            else if (choice == 'b')
            {
                Console.WriteLine("Post a plant to be adopted");

                Plant newPlant = new Plant();

                Console.Write("Enter the Year for post expiration: ");
                int year;
                //The out parameter lets you pass an argument to a method by reference rather than by value
                if (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Invalid input. Please enter a valid year");
                    return;
                }

                Console.Write("Enter the moth for post expiration: ");
                int month;
                if (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12) 
                {
                    Console.WriteLine("Invalid input. Please enter a valid month");
                    return;
                }

                Console.Write("Enter the Day for post expiration: ");
                int day;
                if (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > 31) 
                {
                    Console.WriteLine("Invalid input. Please enter a valid day");
                    return;
                }
               
                try
                {
                    DateTime expirationDate = new DateTime(year, month, day);

                    newPlant.AvailableUntil = expirationDate;

                    plants.Add(newPlant);

                    Console.WriteLine($"Plant add successfully. Available until: {newPlant.AvailableUntil} ");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid date. Please enter a valid date");
                }                

            }
            else if (choice == 'c')
            {
                Console.WriteLine("Adopt a plant");
                
            }
            else if (choice == 'd')
            {
                SearchForPlantsBySunLight(plants);
            }
            else if (choice == 'e')
            {
                ViewAppStatistics(plants);
            }
            else
            {
                Console.WriteLine("Exiting the ExtraVert Plant Adoption App!");
                return;
            }          
        }
    }
    static void DisplayAllPlants(List<Plant> plants)
    {
       
        for (int i = 0; i < plants.Count; i++)
        {
            string plantDetails = PlantDetails(plants[i]);
            Console.WriteLine($"{i + 1}. {plantDetails}");
        }

    }
    static void PlantofTheDay(List<Plant> plants)
    {
        Random random = new Random();
        int randomInterger = random.Next(0,plants.Count);
        Plant randomPlant = plants[randomInterger];

        Console.WriteLine($"Today's plant of the day is {randomPlant.Species}");
    }
    static void SearchForPlantsBySunLight(List<Plant> plants)
    {
        Console.WriteLine($"Enter a number between 1 and 5");
        int maxLightNeeds;
        if (!int.TryParse(Console.ReadLine(), out maxLightNeeds) || maxLightNeeds < 1 || maxLightNeeds > 5)
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 5 ");
        }
        else
        {
            List<Plant> matchingPlants = new List<Plant>();

            foreach (var plant in plants)
            {
                if (plant.LightNeeds <= maxLightNeeds)
                {
                    matchingPlants.Add(plant);
                    Console.WriteLine(plant.Species);
                }
            }
        }      
    }
    static void ViewAppStatistics(List<Plant> plants)
    {
        // Calculate and display app statistics
        Console.WriteLine("\nStats:");
        Console.WriteLine($"Lowest price plant name: {GetLowestPricePlant(plants)?.Species}");
        Console.WriteLine($"Number of Plants Available: {GetNumberOfPlantsAvailable(plants)}");
        Console.WriteLine($"Name of plant with highest light needs: {GetHighestLightNeedsPlant(plants)?.Species}");
        Console.WriteLine($"Average light needs: {GetAverageLightNeeds(plants)}");
        Console.WriteLine($"Percentage of plants adopted: {GetPercentageOfPlantsAdopted(plants)}%");
    }
    static Plant GetLowestPricePlant(List<Plant> plants)
    {
        
        Plant lowestPricePlant = plants[0];
        foreach (var plant in plants)
        {
            if (plant.AskingPrice < lowestPricePlant.AskingPrice)
            {
                lowestPricePlant = plant;
            }
        }

        return lowestPricePlant;
    }
    static int GetNumberOfPlantsAvailable(List<Plant> plants)
    {
        int count = 0;
        foreach (var plant in plants)
        {
            if (!plant.Sold)
            {
                count++;
            }
        }
        return count;
    }
    static Plant GetHighestLightNeedsPlant(List<Plant> plants)
    {
        
        Plant highestLightNeedsPlant = plants[0];
        foreach(var plant in plants)
        {
            if (plant.LightNeeds > highestLightNeedsPlant.LightNeeds)
            {
                highestLightNeedsPlant = plant;
            }
        }
        return highestLightNeedsPlant;
    }
    static decimal GetAverageLightNeeds(List<Plant> plants)
    {
       
        int gettotalLightNeeds = 0;

        foreach (var plant in plants)
        {
            gettotalLightNeeds += plant.LightNeeds;
        }

        decimal averageLightNeeds = (decimal)gettotalLightNeeds / plants.Count;

        return averageLightNeeds;
    }
    static decimal GetPercentageOfPlantsAdopted(List<Plant> plants) //Comment New Changes
    {
        
        decimal getAdoptedPlants = 0.0M;
        foreach (var plant in plants)
        {
            if (plant.Sold)
            {
                getAdoptedPlants++;
            }
        }
        decimal percentageAdopted = ((decimal)getAdoptedPlants / plants.Count) * 100;
        return percentageAdopted;
    }
    static string PlantDetails(Plant plant)
    {
        string availability = plant.Sold ? "was sold" : "is available";
        string priceInfo = plant.Sold ? $"for {plant.AskingPrice:c}" : $"for {plant.AskingPrice:c} dollars";
        return $"{plant.Species} in {plant.City} {availability} {priceInfo}";
    }


}
