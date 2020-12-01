using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komodo_Repository;


namespace Komodo_Console {
    public class ProgramUI {
        private Developer devy = new Developer();
        private DevTeam _devTeam = new DevTeam();
        private DevTeamRepository _devTeamRepo = new DevTeamRepository();
        private DeveloperRepository _developerRepo = new DeveloperRepository();
        public void Run() {
            SeedWithDevsAndTeam();
            Menu();
        }
        ///Main Menu
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
            Developer developer = new Developer();
            Console.WriteLine("Enter the name of the new developer:");
            developer.Name = Console.ReadLine();

            Console.WriteLine($"Enter the badge number for {developer.Name}");
            string badgeAsString = Console.ReadLine();
            int.TryParse(badgeAsString, out int k);

            developer.BadgeNumber = k;
            if (k == 0) {
                Console.WriteLine("Invalid entry.  Badge number has been assigned to 0. \n " +
                    "Please finish adding developer and then update badge number.");
            }
            string yesOrNo;
            do {
                Console.WriteLine($"Would you like to assign {developer.Name} to a team? y/n");

                yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "y") {
                    ViewListOfTeams();
                    Console.WriteLine($"Enter the index number of the team you wish to assign {developer.Name} to.");

                    string numOfDevString = (Console.ReadLine());
                    int.TryParse(numOfDevString, out int j);
                      
                    while (j <= 0 || j > _devTeamRepo._listOfTeams.Count) {
                        Console.WriteLine("Invalid entry. Are you sure you entered the index number on the left?  Please try again.  Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        ViewListOfTeams();
                        Console.WriteLine($"Enter the index number of the team you wish to assign {developer.Name} to.");

                        numOfDevString = (Console.ReadLine());
                        int.TryParse(numOfDevString, out  j);
                    }

                    //foreach (DevTeam team in _devTeamRepo._listOfTeams) {
                    //    if (Console.ReadLine().ToLower() == team.TeamName.ToLower()) {
                            developer.Team = _devTeamRepo._listOfTeams.ElementAt(j-1);
                    _devTeamRepo._listOfTeams.ElementAt(j - 1).TeamMembers.Add(developer);
                        
                    if (developer.Team == null) {
                        Console.WriteLine("Sorry that team name was not found and team affiliation was not updated");
                    }
                }
                else if (yesOrNo == "n") {
                    break;
                }
                else {
                    Console.WriteLine("Please enter a (y) or (n)");
                }
            } while (yesOrNo != "y" && yesOrNo != "n");  //end of long do while loop

            do {
                Console.WriteLine($"Does {developer.Name} have Pluralsight access?  y/n ");
                yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "y") {
                    developer.TypeOfAccess = (AccessType)1;
                }
                else if (yesOrNo == "n") {
                    developer.TypeOfAccess = (AccessType)0;
                }
                else {
                    Console.WriteLine("Please enter a (y) or (n)");
                }
            } while (yesOrNo != "y" && yesOrNo != "n");  //just a loop to deal with user not entering n or y

            _developerRepo.AddToList(developer);
        }
        //View
        private void ViewDevelopers() {
            Console.Clear();
            int index = 1;
            string team;
            List<DevTeam> listOfTeams = _devTeamRepo.GetListOfTeams();
            List<Developer> listOfDevelopers = _developerRepo.GetList();
            foreach (Developer developer in listOfDevelopers) {
                foreach (DevTeam devTeam in listOfTeams) {
                    if (developer.Team == devTeam) {
                        team = devTeam.TeamName;
                    }
                    else
                        team = null;
                        //team = developer.Team.TeamName;
                        Console.WriteLine(index + $".  \nName:  {developer.Name}\n" +
                            $"\tBadge Number: {developer.BadgeNumber}\n" +
                            $"\tPluralSight Access Status: {developer.TypeOfAccess}\n" +
                            $"\tTeam Affiliation: " + team);
                        index++;
                }
            }

        }
        //Update

        private void UpdateList() {
            Console.Clear();
            ViewDevelopers();
            Console.WriteLine("Enter the index number of the developer you wish to edit: ");

            string numOfDevString = (Console.ReadLine());
            int.TryParse(numOfDevString, out int k);

            while (k <= 0 || k > _developerRepo._listOfDevelopers.Count) {
                Console.WriteLine("Invalid entry. Are you sure you entered the index number on the left?  Please try again.  Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                ViewDevelopers();
                Console.WriteLine("Enter the index number of the fired developer: ");
                numOfDevString = (Console.ReadLine());
                int.TryParse(numOfDevString, out k);

            }


            Developer developer = new Developer();

            Console.WriteLine("Enter the new name: ");
            developer.Name = Console.ReadLine();

            Console.WriteLine($"Enter the badge number for {developer.Name}");
            string badgeAsString = Console.ReadLine();
            int.TryParse(badgeAsString, out int el);

            developer.BadgeNumber = el;
            if (el == 0) {
                Console.WriteLine("Invalid entry.  Badge number has been assigned to 0. \n " +
                    "Please finish adding developer and then update badge number.\n\n\n");
            }


            //start

            string yesOrNo;
            do {
                Console.WriteLine($"Would you like to assign {developer.Name} to a team? y/n");

                yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "y") {
                    ViewListOfTeams();
                    Console.WriteLine($"Enter the index number of the team you wish to assign {developer.Name} to.");

                    string numOfDevString2 = (Console.ReadLine());
                    int.TryParse(numOfDevString2, out int j);

                    while (j <= 0 || j > _devTeamRepo._listOfTeams.Count) {
                        Console.WriteLine("Invalid entry. Are you sure you entered the index number on the left?  Please try again.  Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        ViewListOfTeams();
                        Console.WriteLine($"Enter the index number of the team you wish to assign {developer.Name} to.");

                        numOfDevString2 = (Console.ReadLine());
                        int.TryParse(numOfDevString2, out j);
                    }

                    //foreach (DevTeam team in _devTeamRepo._listOfTeams) {
                    //    if (Console.ReadLine().ToLower() == team.TeamName.ToLower()) {
                    developer.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                    _devTeamRepo._listOfTeams.ElementAt(j - 1).TeamMembers.Add(developer);

                    if (developer.Team == null) {
                        Console.WriteLine("Sorry that team name was not found and team affiliation was not updated");
                    }
                }
                else if (yesOrNo == "n") {
                    break;
                }
                else {
                    Console.WriteLine("Please enter a (y) or (n)");
                }
            } while (yesOrNo != "y" && yesOrNo != "n");  //end of long do while loop

            do {
                Console.WriteLine($"Does {developer.Name} have Pluralsight access?  y/n ");
                yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "y") {
                    developer.TypeOfAccess = (AccessType)1;
                }
                else if (yesOrNo == "n") {
                    developer.TypeOfAccess = (AccessType)0;
                }
                else {
                    Console.WriteLine("Please enter a (y) or (n)");
                }
            } while (yesOrNo != "y" && yesOrNo != "n");  //just a loop to deal with user not entering n or y

           
            //end

            bool wasUpdated = _developerRepo.UpdateDeveloper(k, developer);
            if (wasUpdated) {
                Console.WriteLine("Deveolper successfully updated");
            }
            else {
                Console.WriteLine("Update failed!");
            }



        }

        //Delete
        private void DeleteDeveloper() {
            Console.Clear();
            ViewDevelopers();
            Console.WriteLine("Enter the index number of the fired developer: ");
            string numOfDevString = (Console.ReadLine());
            int.TryParse(numOfDevString, out int k);

            while (k <= 0 || k > _developerRepo._listOfDevelopers.Count) {
               Console.WriteLine("Invalid entry. Are you sure you entered the index number on the left?  Please try again.  Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                ViewDevelopers();
                Console.WriteLine("Enter the index number of the fired developer: ");
                numOfDevString = (Console.ReadLine());
                int.TryParse(numOfDevString, out k);

            }
             
              

            bool wasDeleted = _developerRepo.DeleteDeveloper(k);
            // bool wasDeletedFromTeam = _developerRepo.DeleteFromTeam();

            if (wasDeleted) {
                Console.WriteLine("The developer was deleted and removed from most recently assigned team.  \n" +
                    "If developer was assigned to multiple teams you may have to manually remove from additional teams.");
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
        //Create a New Team
        private void CreateTeam() {
            Console.Clear();
            DevTeam team = new DevTeam();
            //Get Name
            Console.WriteLine("What is the name of the new team? ");
            team.TeamName = Console.ReadLine();
            //Get ID
            do {
                Console.WriteLine($"Enter the numerical Team ID for {team.TeamName}");
                string s = Console.ReadLine();

                int.TryParse(s, out int k);
                team.TeamID = k;
            } while (team.TeamID == 0);
            //Loop to add multiple Developers at one time
            int numOfDev;
            ViewDevelopers();
            do {
                Console.WriteLine($"How many developers would you like to add to {team.TeamName}?\n" +
                    $"A minimum of 1 team member is required."); //had to include because what if user intentionally enters 0
                string s = Console.ReadLine();

                int.TryParse(s, out int k);
                numOfDev = k;
            } while (numOfDev == 0);
            if (numOfDev > _developerRepo._listOfDevelopers.Count) {
                Console.WriteLine($"{numOfDev} is greater than the number of available developers.  Please try again.");
                Menu();
            }
            List<Developer> newList = new List<Developer>(numOfDev);
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


        }///View List of teams
        int index = 1;
        private void ViewListOfTeams() {
            List<DevTeam> listOfTeams = _devTeamRepo.GetListOfTeams();
            foreach (DevTeam team in listOfTeams) {
                Console.WriteLine(index + $"  {team.TeamName}");
            }
        }///Choose a team to see details
        private void ViewDetailsForTeam() {
            Console.Clear();
            ViewListOfTeams();
            Console.WriteLine("Type the team name to see details:");
            string teamChoice = Console.ReadLine().ToLower();
            Console.Clear();
            // List<DevTeam> listOfTeams = _devTeamRepo.GetListOfTeams();
            int count = 0;
            foreach (DevTeam team in _devTeamRepo.GetListOfTeams()) {
                if (teamChoice == team.TeamName.ToLower()) {
                    _devTeamRepo.TeamDetails(teamChoice);  //print details for team
                    count++;
                }
            }
            if (count == 0) {                      //condition for "if there was no teamchoice==team match"??
                Console.WriteLine("Please check your spelling and try again");
            }


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
            string teamChoice = Console.ReadLine().ToLower();

            Console.Clear();

            int count = 0;
            foreach (DevTeam team in _devTeamRepo.GetListOfTeams()) {
                if (teamChoice == team.TeamName.ToLower()) {
                    _devTeamRepo.TeamDetails(teamChoice);  //print details for team
                    count++;
                }
            }
            if (count == 0) {                      //condition for "if there was no teamchoice==team match"??
                Console.WriteLine("Please check your spelling and try again");
                return;
            }

            _devTeamRepo.RemoveDeveloper(teamChoice);


            //print details for team
            //right here I want to call to a method to add or remove directly from list of developers on a team
        }
        //private void AddRemove() {



        private void SeedWithDevsAndTeam() {
            Developer b = new Developer("Brian", 101, AccessType.yes); //showing both styles here for calling to enum class
            Developer e = new Developer("Ellen", 102, (AccessType)1);
            Developer c = new Developer("Carter", 103, AccessType.no);

            _developerRepo.AddToList(b);
            _developerRepo.AddToList(e);
            _developerRepo.AddToList(c);

            DevTeam Colts = new DevTeam("Colts", 88);
            _devTeamRepo.CreateNewTeam(Colts);

            Developer a = new Developer("Austin", 104, AccessType.no, Colts);
            List<Developer> test = new List<Developer>();
            test.Add(a);
            _developerRepo.AddToList(a);
            Colts.TeamMembers = test;
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

