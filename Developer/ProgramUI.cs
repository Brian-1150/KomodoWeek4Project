using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komodo_Repository;


namespace Komodo_Console {
    public class ProgramUI {

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
                    "\n" +                                                  //" 6.  View List of Teams\n" +
                    " 6.  View Detailed List of a Team\n" +
                    " 7.  Create New Team\n" +
                    " 8.  Update Team Name/ID and Add Developers to a Team\n" +
                    " 9.  Remove Developers From a Team\n" +
                    "10.  Exit");
                                                                            
                //Get User Input
                string input = Console.ReadLine();

                //Read Input and Invoke Appropriate Method(s)
                switch (input) {
                    case "1": //View
                        ViewDevelopers();
                        break;
                    case "2":  //List of Developer who still need PluralSight
                        ListOfDevInNeedOfPlural();
                        break;
                    case "3": //Add Developer
                        AddDeveloper();
                        break;
                    case "4": //Update
                        UpdateList();
                        break;
                    case "5":  //Delete
                        DeleteDeveloper();
                        break;
                    case "6": //View list of teams with prompt to see details                                             
                        ViewDetailsForTeam();                                                                  
                        break;
                    case "7": //Create new team
                        CreateTeam();
                        break;
                    case "8": //Add to team
                        AddDevelopersToTeam();
                        break;
                    case "9": //Remove Developers from team
                        RemoveDevFromTeam();
                        break;
                    case "10": //Exit
                        Console.WriteLine("Have a great rest of your day!");
                        exit = true;
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
            int index = 1;
            
                List<Developer> listOfDevelopers = _developerRepo.GetList();
            foreach (Developer developer in listOfDevelopers) {
                Console.WriteLine(index + $".  \nName:  {developer.Name}\n" +
                    $"\tBadge Number: {developer.BadgeNumber}\n" +
                    $"\tPluralSight Access Status: {developer.TypeOfAccess}\n");
                    index++;
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
                Console.WriteLine("Update failed!  Check spelling(case sensitive)");
            }



        }

        //Delete
        private void DeleteDeveloper() {
            Console.Clear();
            ViewDevelopers();
            Console.WriteLine("Enter the index number of the fired developer: ");
            int numOfDev = int.Parse(Console.ReadLine());
            bool wasDeleted = _developerRepo.DeleteDeveloper(numOfDev);
            bool wasDeletedFromTeam = _developerRepo.DeleteFromTeam();

            if (wasDeleted) {
                Console.WriteLine("The developer was deleted. ");
            }
            else {
                Console.WriteLine("The developer could not be deleted.");
                break;
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
        //Create a New Team
        private void CreateTeam() {
            Console.Clear();
            DevTeam team = new DevTeam();
            //Get Name
            Console.WriteLine("What is the name of the new team? ");
            team.TeamName = Console.ReadLine();
            //Get ID
            Console.WriteLine($"Enter the Team ID for {team.TeamName}");
            team.TeamID = int.Parse(Console.ReadLine());  //One step Parse
            //Loop to add multiple Developers at one time
            Console.WriteLine($"How many developers would you like to add to {team.TeamName}?");
            int numOfDev = int.Parse(Console.ReadLine());
            List<Developer> newList = new List<Developer>(numOfDev);
            ViewDevelopers();
            for (int i = 0; i < numOfDev; i++) {
                Console.WriteLine($"Enter the badge number of the next developer you wish to add to {team.TeamName}: ");
                int id = int.Parse(Console.ReadLine());

                foreach (Developer dev in _developerRepo._listOfDevelopers) {  //code to also add this team to the developer object
                    if (id == dev.BadgeNumber) {
                        dev.Team = team;
                    }
                }
               
                newList.Add(_developerRepo.GetDeveloper(id));
            }
            team.TeamMembers = newList;
            _devTeamRepo.CreateNewTeam(team);


        }//View List of teams
        private void ViewListOfTeams() {
            List<DevTeam> listOfTeams = _devTeamRepo.GetListOfTeams();
            foreach (DevTeam team in listOfTeams) {
                Console.WriteLine(team.TeamName);
            }
        }//Choose a team to see details
        private void ViewDetailsForTeam() {
            Console.Clear();
            ViewListOfTeams();
            Console.WriteLine("Type the team name to see details:");
            string team = Console.ReadLine();
            Console.Clear();
            _devTeamRepo.TeamDetails(team);  //print details for team

        }
        //Add developers and make any other changes to team
        private void AddDevelopersToTeam() {
            Console.Clear();
            ViewListOfTeams();
            //Choose team to edit
            Console.WriteLine("\n To which Team would you like to add a developer?");
            string teamNameString = Console.ReadLine();
            DevTeam team = new DevTeam();
            //Create a new instance of DevTeam
            Console.WriteLine("Re-enter or update team name: ");  //Name
            team.TeamName = Console.ReadLine();
            Console.WriteLine($"Confirm or update Team ID"); //ID
            team.TeamID = int.Parse(Console.ReadLine());
            Console.WriteLine($"How many developers would you like to add to {team.TeamName}?"); //Loop for Developers
            int numOfDev = int.Parse(Console.ReadLine());
            List<Developer> newList = new List<Developer>(numOfDev);
            ViewDevelopers();
            for (int i = 0; i < numOfDev; i++) {
                Console.WriteLine($"Enter the badge number of the next developer you wish to add to {team.TeamName}: ");
                int id = int.Parse(Console.ReadLine());

                foreach (Developer dev in _developerRepo._listOfDevelopers) {  //code to also add this team to the developer object
                    if (id == dev.BadgeNumber) {
                        dev.Team = team;
                    }
                }

                // Developer next = _developerRepo.GetDeveloper(id);
                newList.Add(_developerRepo.GetDeveloper(id));
            }
            team.TeamMembers = newList;
            _devTeamRepo.UpdateTeam(teamNameString, team);

        }
        // For example, why was I not able to call this public method directly from DevTeamRepository ?? TeamDetails(Colts);
        private void RemoveDevFromTeam() {

            Console.Clear();
            ViewListOfTeams();
            Console.WriteLine("From which team do you wish to remove developers?");
            string team = Console.ReadLine();
            Console.Clear();
            _devTeamRepo.TeamDetails(team);
            _devTeamRepo.RemoveDeveloper(team);


            //print details for team
            //right here I want to call to a method to add or remove directly from list of developers on a team
        }
        //private void AddRemove() {



        private void SeedContentList() {
            Developer b = new Developer("Brian", 101, AccessType.yes); //showing both styles here for calling to enum class
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
        //public DevTeam GetTeamMethodMainFile(string name) {
        //    foreach (DevTeam team in _devTeamRepo) {
        //        if (team.TeamName == name) {
        //            return team;
        //        }
        //    }
        //    return null;
        //
    }
}

