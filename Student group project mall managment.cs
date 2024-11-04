using System;
using System.Collections.Generic;

namespace StudentClubManagementSystem
{
    public class ClubRole
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string ContactInfo { get; set; }

        public ClubRole(string name, string role, string contactInfo)
        {
            Name = name;
            Role = role;
            ContactInfo = contactInfo;
        }
    }

    public class Society
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<string> Events { get; set; } = new List<string>();

        public virtual void AddActivity(string activity)
        {
            Events.Add(activity);
        }

        public virtual void ListEvents()
        {
            Console.WriteLine($"Events for {Name}:");
            foreach (var ev in Events)
            {
                Console.WriteLine($"- {ev}");
            }
        }
    }

    public class FundedSociety : Society
    {
        public double FundingAmount { get; set; }

        public FundedSociety(string name, string contact, double fundingAmount)
        {
            Name = name;
            Contact = contact;
            FundingAmount = fundingAmount;
        }
    }

    public class NonFundedSociety : Society
    {
        public NonFundedSociety(string name, string contact)
        {
            Name = name;
            Contact = contact;
        }
    }

    public class StudentClub
    {
        private double budget;
        public ClubRole President { get; set; }
        public ClubRole VicePresident { get; set; }
        public List<Society> Societies { get; set; } = new List<Society>();

        public StudentClub(double initialBudget)
        {
            budget = initialBudget;
        }

        public void RegisterSociety(Society society)
        {
            Societies.Add(society);
            Console.WriteLine($"Society '{society.Name}' registered successfully.");
        }

        public void DispenseFunds()
        {
            foreach (var society in Societies)
            {
                if (society is FundedSociety fundedSociety)
                {
                    Console.WriteLine($"Dispensing ${fundedSociety.FundingAmount} to {fundedSociety.Name}");
                }
                else
                {
                    Console.WriteLine($"{society.Name} is not funded.");
                }
            }
        }

        public void DisplaySocieties()
        {
            Console.WriteLine("List of Societies:");
            foreach (var society in Societies)
            {
                Console.WriteLine($"- {society.Name} (Contact: {society.Contact})");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new student club with an initial budget
            StudentClub club = new StudentClub(10000);

            // Assign roles directly
            club.President = new ClubRole("Alice", "President", "alice@example.com");
            club.VicePresident = new ClubRole("Bob", "Vice President", "bob@example.com");

            // Register societies
            club.RegisterSociety(new FundedSociety("Techbit Society", "tech@example.com", 5000));
            club.RegisterSociety(new NonFundedSociety("Sports Society", "sports@example.com"));
            club.RegisterSociety(new FundedSociety("Literary Society", "literary@example.com", 3000));

            // Add events to societies
            var techSociety = club.Societies.Find(s => s.Name == "Techbit Society");
            techSociety.AddActivity("Tech Workshop");
            techSociety.AddActivity("Coding Bootcamp");

            var literarySociety = club.Societies.Find(s => s.Name == "Literary Society");
            literarySociety.AddActivity("Poetry Reading");

            // Display societies and their events
            club.DisplaySocieties();
            techSociety.ListEvents();
            literarySociety.ListEvents();

            // Dispense funds
            club.DispenseFunds();
        }
    }
}
