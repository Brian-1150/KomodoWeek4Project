using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komodo_Repository;


namespace Komodo_Console {
    public class ProgramUI {
        //private Developer devy = new Developer();
        //private DevTeam _devTeam = new DevTeam();
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
                        Console.WriteLine("Please enter a valid number 1-10");
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
        Badge: Console.WriteLine("");
            Console.WriteLine($"Enter the badge number for {developer.Name}");
            string badgeAsString = Console.ReadLine();
            int.TryParse(badgeAsString, out int k);

            developer.BadgeNumber = k;
            if (k == 0) {
                Console.WriteLine("Invalid entry.  Try again.");
                goto Badge;
            }
            foreach (Developer dev in _developerRepo._listOfDevelopers) {
                if (k == dev.BadgeNumber) {
                    Console.WriteLine("That number is alredy assigned to another developer.  Please enter a unique ID");
                    goto Badge;
                }
            }
            string yesOrNo;
            do {
                Console.WriteLine($"Would you like to assign {developer.Name} to a team? y/n");

                yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "y") {
                    Console.Clear();
                    ViewListOfTeams();
                    Console.WriteLine($"Enter the index number of the team you wish to assign {developer.Name} to.");

                    string numOfDevString = (Console.ReadLine());
                    int.TryParse(numOfDevString, out int j);

                    while (j <= 0 || j > _devTeamRepo._listOfTeams.Count) {
                        Console.WriteLine("Invalid entry.   Please try again.");

                        Console.WriteLine($"Enter the index number of the team you wish to assign {developer.Name} to.");

                        numOfDevString = (Console.ReadLine());
                        int.TryParse(numOfDevString, out j);
                    }
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

            _developerRepo.AddToList(developer);
        }
        //View
        private void ViewDevelopers() {
            Console.Clear();
            int index = 1;
            //List<DevTeam> listOfTeams = _devTeamRepo.GetListOfTeams();
            List<Developer> listOfDevelopers = _developerRepo.GetList();



            foreach (Developer developer in listOfDevelopers) {

                Console.WriteLine("\n" + index + $".      Name:  {developer.Name}\n" +
                    $"\tBadge Number: {developer.BadgeNumber}\n" +
                    $"\tPluralSight Access Status: {developer.TypeOfAccess}");
                if (developer.Team != null) {
                    Console.WriteLine($"\tTeam Affiliation: { developer.Team.TeamName}");
                    index++;
                }
                else {
                    Console.WriteLine("\tTeam Affiliatin:  None ");
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
                Console.WriteLine("Invalid entry. Please try again. ");

                Console.WriteLine("Enter the index number of the developer you wish to edit: ");
                numOfDevString = (Console.ReadLine());
                int.TryParse(numOfDevString, out k);

            }


            Developer developer = new Developer();

            Console.WriteLine("Enter the new name: ");
            developer.Name = Console.ReadLine();

            Console.WriteLine($"FYI badge numbers cannot be changed.");
            developer.BadgeNumber = GetDeveloperByIndex(numOfDevString).BadgeNumber;


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
                        Console.WriteLine("Invalid entry. Please try again.");

                        Console.WriteLine($"Enter the index number of the team you wish to assign {developer.Name} to.");

                        numOfDevString2 = (Console.ReadLine());
                        int.TryParse(numOfDevString2, out j);
                    }

                    if (GetDeveloperByIndex(numOfDevString).Team == null) {
                        developer.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                        _devTeamRepo._listOfTeams.ElementAt(j - 1).TeamMembers.Add(developer);  //avoiding null exception for next if..
                    }
                    else if (GetDeveloperByIndex(numOfDevString).Team == _devTeamRepo._listOfTeams.ElementAt(j - 1)) {
                        Console.WriteLine("Developer was already assigned to that team and cannot be added agian.");
                        developer.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                    }
                    else {
                        developer.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                        _devTeamRepo._listOfTeams.ElementAt(j - 1).TeamMembers.Add(developer);
                    }

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
                Console.WriteLine("Invalid entry.  Please try again.  ");

                Console.WriteLine("Enter the index number of the fired developer: ");
                numOfDevString = (Console.ReadLine());
                int.TryParse(numOfDevString, out k);

            }



            bool wasDeleted = _developerRepo.DeleteDeveloper(k);

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
            List<Developer> newList = new List<Developer>();
            Console.Clear();
            DevTeam team = new DevTeam();
            //Get Name
            Console.WriteLine("What is the name of the new team? ");
            team.TeamName = Console.ReadLine();
            //Get ID
            do {
            TeamBadge:
                Console.WriteLine($"Enter the numerical Team ID for {team.TeamName}");
                string s = Console.ReadLine();

                int.TryParse(s, out int k);
                team.TeamID = k;
                foreach (DevTeam tm in _devTeamRepo._listOfTeams) {
                    if (tm.TeamID == k) {
                        Console.WriteLine("That ID has already been assigned.  Please enter a unique TeamID");
                        goto TeamBadge;
                    }
                }
            } while (team.TeamID <= 0);

            //Loop to add multiple Developers at one time
            string yesOrNo;
            do {
                Console.WriteLine($"Would you like to assign developers to {team.TeamName}? y/n");

                yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "y") {
                    ViewDevelopers();
                    Console.WriteLine($"How many developers would you like to add to {team.TeamName}?"); //Loop for Developers
                    string tempString2 = (Console.ReadLine());
                    int.TryParse(tempString2, out int k);
                    while (k <= 0 || k > _developerRepo.GetList().Count) {
                        Console.WriteLine("Invalid entry.  Please try again. ");

                        Console.WriteLine($"How many developers would you like to add to {team.TeamName}?");
                        tempString2 = Console.ReadLine();
                        int.TryParse(tempString2, out k);

                    }

                    Console.Clear();
                    ViewDevelopers();

                    string x;

                    for (int i = 0; i < k; i++) {

                        Console.WriteLine($"What is the index number of the next developer you would like to add to {team.TeamName}?");
                        x = Console.ReadLine();
                        Developer ex = GetDeveloperByIndex(x);
                        if (ex != null) {
                            foreach (Developer dev in newList) {
                                if (dev.Name == ex.Name) {
                                    Console.WriteLine("That developer was already added and could not be added again");
                                    goto GetOutOfLoop;

                                }

                            }

                            newList.Add(ex);
                        }

                        while (GetDeveloperByIndex(x) == null) {
                            Console.WriteLine("Invalid input. Try again!\n");

                            Console.WriteLine($"What is the index number of the next developer you would like to add to {team.TeamName}?");
                            x = Console.ReadLine();
                            ex = GetDeveloperByIndex(x);
                            if (ex != null)
                                newList.Add(ex);

                        }
                    GetOutOfLoop: Console.WriteLine(""); //label to break out of loop if user trys to add the same dev

                    }
                    team.TeamMembers = newList;

                }
                else
                if (yesOrNo == "n") {
                    break;
                }
                else {
                    Console.WriteLine("Please enter y or n.");
                }
            }
            while (yesOrNo != "y" && yesOrNo != "n");

            _devTeamRepo.CreateNewTeam(team);
            if (newList != null) {
                foreach (Developer dev in newList) {
                    dev.Team = team;
                }
            }

        }///View List of teams
        private void ViewListOfTeams() {
            int index = 1;
            //List<DevTeam> listOfTeams = _devTeamRepo.GetListOfTeams();   //lets try omitting this step 12/1 920am
            //foreach (DevTeam team in _devTeamRepo.GetListOfTeams()) {
            foreach (DevTeam team in _devTeamRepo._listOfTeams) {
                Console.WriteLine(index + $"  {team.TeamName}");
                index++;
            }
        }///Choose a team to see details
        private void ViewDetailsForTeam() {
            Console.Clear();
            ViewListOfTeams();
            Console.WriteLine($"Enter the index number of the team you wish to view.");
            string numOfDevString = (Console.ReadLine());
            int.TryParse(numOfDevString, out int j);

            while (j <= 0 || j > _devTeamRepo._listOfTeams.Count) {
                Console.WriteLine("Invalid entry.   Please try again.");

                Console.WriteLine($"Enter an index number to see details for the team.");

                numOfDevString = (Console.ReadLine());
                int.TryParse(numOfDevString, out j);
            }

            _devTeamRepo.TeamDetails(j);


        }
        //Add developers and make any other changes to team
        private void AddDevelopersToTeam() {

            Console.Clear();

            //Choose team to edit


            ViewListOfTeams();
            Console.WriteLine($"Enter the index number of the team you wish to edit.");

            string tempString = (Console.ReadLine());
            int.TryParse(tempString, out int j);

            while (j <= 0 || j > _devTeamRepo._listOfTeams.Count) {
                Console.WriteLine("Invalid entry.  Please try again.");

                Console.WriteLine($"Enter the index number of the team you wish to edit.");

                tempString = (Console.ReadLine());
                int.TryParse(tempString, out j);
            }

            DevTeam team = new DevTeam();
            List<Developer> newList = new List<Developer>();
            //Create a new instance of DevTeam
            string response;
            _devTeamRepo.TeamDetails(j);
            Console.WriteLine("Do you wish to update team name at this time? y/n");
            response = Console.ReadLine().ToLower();
            if (response == "y") {
                Console.WriteLine("What is the new team name: ");  //Name
                team.TeamName = Console.ReadLine();
            }
            else {
                string oldName = _devTeamRepo._listOfTeams.ElementAt(j - 1).TeamName;
                team.TeamName = oldName;
            }
            Console.WriteLine($"FYI Team ID cannot be changed.\n"); //ID
            //Loop to add multiple Developers at one time
            string yesOrNo;
            do {
                Console.WriteLine($"Would you like to assign developers to {team.TeamName}? y/n");

                yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "y") {
                    ViewDevelopers();

                    Console.WriteLine($"How many developers would you like to add to {team.TeamName}?"); //Loop for Developers
                    string tempString2 = (Console.ReadLine());
                    int.TryParse(tempString2, out int k);
                    while (k <= 0 || k > _developerRepo.GetList().Count) {
                        Console.WriteLine("Invalid entry.  Please try again.");

                        Console.WriteLine($"How many developers would you like to add to {team.TeamName}?");
                        tempString2 = Console.ReadLine();
                        int.TryParse(tempString2, out k);

                    }

                    Console.Clear();
                    ViewDevelopers();

                    string x;

                    for (int i = 0; i < k; i++) {

                        Console.WriteLine($"What is the index number of the next developer you would like to add to {team.TeamName}?");
                        x = Console.ReadLine();
                        Developer ex = GetDeveloperByIndex(x);
                        if (ex != null) {

                            if (ex.Team == null) {
                                ex.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                                newList.Add(ex);  //this if statement avoids null exception for the next if...
                            }
                            else
                            if (ex.Team.TeamName == _devTeamRepo._listOfTeams.ElementAt(j - 1).TeamName) {
                                /*catch duplicate dev*/
                                Console.WriteLine("That developer was already assigned and cannot be added again");
                            }
                            else {
                                newList.Add(ex);
                                ex.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                            }
                        }
                        //else continue;
                        while (ex == null) {
                            Console.WriteLine("Invalid input. Try again!\n");



                            x = Console.ReadLine();
                            ex = GetDeveloperByIndex(x);
                            if (ex != null) {

                                if (ex.Team == null) {
                                    ex.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                                    newList.Add(ex);  //this if statement avoids null exception for the next if...
                                }
                                else
                                if (ex.Team.TeamName == _devTeamRepo._listOfTeams.ElementAt(j - 1).TeamName) {
                                    /*catch duplicate dev*/
                                    Console.WriteLine("That developer was already assigned and cannot be added again");
                                }
                                else {
                                    newList.Add(ex);
                                    ex.Team = _devTeamRepo._listOfTeams.ElementAt(j - 1);
                                }
                            }
                        }


                    }
                    team.TeamMembers = newList;

                }
                else
                if (yesOrNo == "n") {
                    break;
                }
                else {
                    Console.WriteLine("Please enter y or n.");
                }
            }
            while (yesOrNo != "y" && yesOrNo != "n");

            if (_devTeamRepo.UpdateTeam(j, team)) {
                Console.WriteLine("Success");
            }
            else {
                Console.WriteLine("Update failed");
            }
        }


        private void RemoveDevFromTeam() {

            Console.Clear();
            ViewListOfTeams();
            Console.WriteLine("From which team do you wish to remove developers?  Enter the index number on the left.");

            string numOfDevString = (Console.ReadLine());
            int.TryParse(numOfDevString, out int j);

            while (j <= 0 || j > _devTeamRepo._listOfTeams.Count) {
                Console.WriteLine("Invalid entry.  Please try again.");

                Console.WriteLine($"Enter an index number for the team to remove a developer.");

                numOfDevString = (Console.ReadLine());
                int.TryParse(numOfDevString, out j);
            }

            _devTeamRepo.RemoveDeveloper(j);

        }




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

        //Helper methods
        public Developer GetDeveloperByIndex(string index) {
            int.TryParse(index, out int k);
            if (k <= 0 || k > _developerRepo._listOfDevelopers.Count) {
                return null;
            }
            else {
                return _developerRepo._listOfDevelopers.ElementAt(k - 1);
            }


        }


    }

}


