﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository {
    public class DevTeamRepository {
        private DeveloperRepository _developerRepo = new DeveloperRepository();
        public List<DevTeam> _listOfTeams = new List<DevTeam>();
        //Create
        public void CreateNewTeam(DevTeam team) {
            _listOfTeams.Add(team);
        }
        //Read
        public List<DevTeam> GetListOfTeams() {
            return _listOfTeams;
        }


        //Update
        public bool UpdateTeam(string name, DevTeam updatedInfo) {
            //Find the team
            DevTeam previousInfo = GetTeam(name);
            //Update the team info
                                                   // List<Developer> tempList = previousInfo.TeamMembers; //workaround to update team members

            if (previousInfo == null) {
                return false;
            }
            else {
                previousInfo.TeamName = updatedInfo.TeamName;
                previousInfo.TeamID = updatedInfo.TeamID;
                                                    //tempList.AddRange(updatedInfo.TeamMembers); 
                previousInfo.TeamMembers.AddRange(updatedInfo.TeamMembers);  //combine 2 lists instead of replacing one with the other
                return true;
            }
        }

        //Delete 
        public bool DeleteTeam(string name) {   //These delete method styles only remove them from the list we created.  
            //Find Developer                    // They are not actually removed from repository and memory space saved???
            DevTeam tempTeam = GetTeam(name);
            if (tempTeam == null) {
                return false;
            }
            else {
                int count = _listOfTeams.Count;
                _listOfTeams.Remove(tempTeam);
                if (count > _listOfTeams.Count) {

                    return true;
                }
                else {
                    return false;
                }
            }
        }
        //Remove Developers from a team
        public bool RemoveDeveloper(string name) {
            DevTeam previousInfo = GetTeam(name);
            int count;
            if (previousInfo.TeamMembers.Count == 0) {
                Console.WriteLine("There are no members on the team to remove");
                return false;
            }
            Console.WriteLine("How many developers would you like to remove?");
            string countString = Console.ReadLine();
            int.TryParse(countString, out count);
         if (count <= 0 || count > previousInfo.TeamMembers.Count) {
                Console.WriteLine("You must enter a valid number, not greater than the current developers on the team.  Start over");
                return false;
            }
            //Find the team


            if (previousInfo == null) {
                return false;
            }
            else {
                int index = 1;
                foreach (Developer developer in previousInfo.TeamMembers) {
                    Console.WriteLine(index + $":  {developer.Name}\n");
                    index++;
                }
                int id;
                for (int i = 0; i < count; i++) {
                    
                        Console.WriteLine($"Enter the number of the next developer you wish to remove: ");
                        string idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                     while (id <= 0 || id > previousInfo.TeamMembers.Count) {
                        Console.WriteLine("Please enter a valid index number");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                    }
                    Developer dev = previousInfo.TeamMembers.ElementAt(id - 1);
                    dev.Team = null;    //removes Team from developer object
                    previousInfo.TeamMembers.RemoveAt(id-1);
                }


            } return true;
        }

        //Helper Methods
        public DevTeam GetTeam(string name) {
            foreach (DevTeam team in _listOfTeams) {
                if (team.TeamName.ToLower() == name) {
                    return team;
                }
            }
            return null;
        }

        public void TeamDetails(string name) {
            foreach (DevTeam team in _listOfTeams) {
                if (team.TeamName.ToLower() == name) {
                    Console.WriteLine($"Team name {team.TeamName} \n" +
                $"Team ID: {team.TeamID}\n" +
                $"List of Developers on Team: \n");
                    List<Developer> listOfDevelopers = team.TeamMembers;
                    foreach (Developer developer in listOfDevelopers) {
                        Console.WriteLine($"Name:  {developer.Name}\n");
                    }

                }

            }
        }

        private void ViewDevelopers() {
            Console.Clear();
            List<Developer> listOfDevelopers = _developerRepo.GetList();
            foreach (Developer developer in listOfDevelopers) {
                Console.WriteLine($"Name:  {developer.Name}\n" +
                    $"\tBadge Number: {developer.BadgeNumber}\n" +
                    $"\tPluralSight Access Status: {developer.TypeOfAccess}\n\n");

            }
            //Here I want add a method that adds and deletes developers from the list on a team and call it from ProgramUI
        }
    }
}
