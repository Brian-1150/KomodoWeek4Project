using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komodo_Repository;

namespace Komodo_Console {
    class ProgramUI {

        private DevTeamRepository _devTeamRepo = new DevTeamRepository();
        private DeveloperRepository _developerRepo = new DeveloperRepository();
        public void Run() {
            SeedTeam();
            SeedContentList();
            Menu();
        }
        //Main Menu
        private void Menu() {
            bool exit = false; //Why do I have to assign false since default bool should be false
            do {
                //Display Menu
                Console.WriteLine("Select an option:  \n" +
                    " 1.  View All Developers\n" +
                    " 2.  View Developers in need of PluralSight\n" +
                    " 3.  Add a New Developer \n" +
                    " 4.  Update Developer Info\n" +
                    " 5.  Delete Developer\n" +
                    "----------Team Functions------------\n" +
                    "\n" +
                    " 6.  View List of Teams\n" +
                    " 7.  View Detailed List of a Team\n" +
                    " 8.  Create New Team\n" +
                    " 9.  Add Developers to a Team\n" +
                    "10.  Exit");
                //Get User Input
                string input = Console.ReadLine();

                //Read Input and Invoke Appropriate Method(s)
                switch (input) {
                    case "1": //Add
                        ViewDevelopers();
                        break;
                    case "2": //View
                        ListOfDevInNeedOfPlural();
                        break;
                    case "3": //Update
                        AddDeveloper();
                        break;
                    case "4": //Delete
                        UpdateList();
                        break;
                    case "5":  //List of Developer who still need PluralSight
                        DeleteDeveloper();
                        break;
                    case "10": //Exit
                        Console.WriteLine("Have a great rest of your day!");
                        exit = true;
                        break;
                    case "6": //view teams
                        ViewListOfTeams();
                        break;
                    case "7": //View detials
                        ViewDetailsForTeam();
                        break;
                    case "8": //Create new team
                        CreateTeam();
                        break;
                    case "9": //Add to team
                        AddDevelopersToTeam();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number 1-5");
                        break;
                }
                Console.WriteLine("Please press any key to continue..."); //just to pause and let user know he is in control of proceeding
                Console.ReadKey();

                Console.Clear(); // clears the console window to remove all the previous menues etc..

            } while (exit == false);


        }
        //Add
        private void AddDeveloper() {
            Console.Clear();
            Developer newDeveloper = new Developer();
            Console.WriteLine("Enter the name of the new developer:");
            newDeveloper.Name = Console.ReadLine();

            Console.WriteLine($"Enter the badge number for {newDeveloper.Name}");
            string badgeAsString = Console.ReadLine();
            newDeveloper.BadgeNumber = int.Parse(badgeAsString);

            string hasAccess;
            do {
                Console.WriteLine($"Does {newDeveloper.Name} have Pluralsight access?  y/n ");
                hasAccess = Console.ReadLine().ToLower();
                if (hasAccess == "y") {
                    newDeveloper.TypeOfAccess = (AccessType)1;
                }
                else if (hasAccess == "n") {
                    newDeveloper.TypeOfAccess = (AccessType)0;
                }
                else {
                    Console.WriteLine("Please enter a (y) or (n)");
                }
            } while (hasAccess != "y" && hasAccess != "n");

            _developerRepo.AddToList(newDeveloper);
        }
        //View
        private void ViewDevelopers() {
        Console.Clear();
            List<Developer> listOfDevelopers = _developerRepo.GetList();
            foreach (Developer developer in listOfDevelopers) {
                Console.WriteLine($"Name:  {developer.Name}\n" +
                    $"\tBadge Number: {developer.BadgeNumber}\n" +
                    $"\tPluralSight Access Status: {developer.TypeOfAccess}\n\n");

            }

        }
        //Update
        private void UpdateList() {
            Console.Clear();
            ViewDevelopers();
            Console.WriteLine("Enter the name of the developer you wish to edit: ");
            string nameOfDev = Console.ReadLine();

            Developer developer = new Developer();

            Console.WriteLine("Enter the new name: ");
            developer.Name = Console.ReadLine();

            Console.WriteLine($"Enter the badge number for {developer.Name}");
            developer.BadgeNumber = int.Parse(Console.ReadLine());

            string hasAccess;
            do {
                Console.WriteLine($"Does {developer.Name} have Pluralsight access?  y/n ");
                hasAccess = Console.ReadLine().ToLower();
                if (hasAccess == "y") {
                    developer.TypeOfAccess = (AccessType)1;
                }
                else if (hasAccess == "n") {
                    developer.TypeOfAccess = (AccessType)0;
                }
                else {
                    Console.WriteLine("Please enter a (y) or (n)");
                }
            } while (hasAccess != "y" && hasAccess != "n");

            bool wasUpdated = _developerRepo.UpdateDeveloper(nameOfDev, developer);
            if (wasUpdated) {
                Console.WriteLine("Deveolper successfully updated");
            }
            else {
                Console.WriteLine("Update failed");
            }



        }

        //Delete
        private void DeleteDeveloper() {
            Console.Clear();
            ViewDevelopers();
            Console.WriteLine("Enter the badge number of the fired developer: ");
            int numOfDev = int.Parse(Console.ReadLine());
            bool wasDeleted = _developerRepo.DeleteDeveloper(numOfDev);
            if (wasDeleted) {
                Console.WriteLine("The developer was deleted. ");
            }
            else {
                Console.WriteLine("The developer could not be deleted.");
            }
        }
        //List of Dev who need Plural
        private void ListOfDevInNeedOfPlural() {
            Console.Clear();
            List<Developer> listOfDevelopers = _developerRepo.GetList();
            foreach (Developer developer in listOfDevelopers) {
                if (developer.TypeOfAccess == AccessType.no)
                    Console.WriteLine($"Name:  {developer.Name}\n" +
                        $"\tBadge Number: {developer.BadgeNumber}\n" +
                        $"\tPluralSight Access Status: {developer.TypeOfAccess}\n\n");
            }
            }
        //Methods for DevTeam
        private void CreateTeam() {
            Console.Clear();
            DevTeam team = new DevTeam();
            Console.WriteLine("What is the name of the new team? ");
            team.TeamName = Console.ReadLine();
            Console.WriteLine($"Enter the Team ID for {team.TeamName}");
            team.TeamID = int.Parse(Console.ReadLine());
            Console.WriteLine($"How many developers would you like to add to {team.TeamName}?");
            int numOfDev = int.Parse(Console.ReadLine());
            List<Developer> newList = new List<Developer>(numOfDev);
            ViewDevelopers();
            for (int i = 0; i < numOfDev; i++) {
                Console.WriteLine($"Enter the badge number of the next developer you wish to add to {team.TeamName}: ");
                int id = int.Parse(Console.ReadLine());

                // Developer next = _developerRepo.GetDeveloper(id);
                newList.Add(_developerRepo.GetDeveloper(id));
            }
            team.TeamMembers = newList;
            _devTeamRepo.CreateNewTeam(team);


        }
        private void ViewListOfTeams() {
            List<DevTeam> listOfTeams = _devTeamRepo.GetListOfTeams();
            foreach (DevTeam team in listOfTeams) {
                Console.WriteLine(team.TeamName);
            }
        }
        private void ViewDetailsForTeam() {
            Console.Clear();
            ViewListOfTeams();
                Console.WriteLine("Which team would you like to view?:");
            string team = Console.ReadLine();
            _devTeamRepo.TeamDetails(team);  //print details for team
            
        }

        private void AddDevelopersToTeam() {
            Console.Clear();
            ViewListOfTeams();
            Console.WriteLine("\n To which Team would you like to add a developer?");
            string teamNameString = Console.ReadLine();
            DevTeam team = new DevTeam();
            Console.WriteLine("Re-enter or update team name: ");
            team.TeamName = Console.ReadLine();
            Console.WriteLine($"Confirm or update Team ID");
            team.TeamID = int.Parse(Console.ReadLine());
            Console.WriteLine($"How many developers would you like to add to {team.TeamName}?");
            int numOfDev = int.Parse(Console.ReadLine());
            List<Developer> newList = new List<Developer>(numOfDev);
            ViewDevelopers();
            for (int i = 0; i < numOfDev; i++) { 
            Console.WriteLine($"Enter the badge number of the next developer you wish to add to {team.TeamName}: ");
                int id = int.Parse(Console.ReadLine());

               // Developer next = _developerRepo.GetDeveloper(id);
                newList.Add(_developerRepo.GetDeveloper(id));
            }
            team.TeamMembers = newList;
            _devTeamRepo.UpdateTeam(teamNameString, team);

        }

        private void SeedContentList() {
            Developer b = new Developer("Brian", 101, AccessType.yes);
            Developer e = new Developer("Ellen", 102, (AccessType)1);
            Developer c = new Developer("Carter", 103, AccessType.no);
            Developer a = new Developer("Austin", 104, AccessType.no);
            _developerRepo.AddToList(b);
            _developerRepo.AddToList(e);
            _developerRepo.AddToList(c);
            _developerRepo.AddToList(a);
        }
        private void SeedTeam() {
            Developer a = new Developer("Austin", 104, AccessType.no);
            List<Developer> test = new List<Developer>();
            test.Add(a);

            DevTeam Colts = new DevTeam("Colts", 88, test);
            _devTeamRepo.CreateNewTeam(Colts);
            
        }
    }
}

