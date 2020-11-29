using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Repository {
    public class DevTeamRepository {

        private List<DevTeam> _listOfTeams = new List<DevTeam>();
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
            //Find the developer
            DevTeam previousInfo = GetTeam(name);
            //Update the developer info
            if (previousInfo == null) {
                return false;
            }
            else {
                previousInfo.TeamName = updatedInfo.TeamName;
                previousInfo.TeamID = updatedInfo.TeamID;
                previousInfo.TeamMembers = updatedInfo.TeamMembers;
                return true;
            }
        }

        //Delete

        //Helper Methods
        public DevTeam GetTeam(string name) {
            foreach (DevTeam team in _listOfTeams) {
                if (team.TeamName == name) {
                    return team;
                }
            }
            return null;
        }
        public void TeamDetails(string name) {
            Console.Clear();
            DevTeam details = GetTeam(name);
            Console.WriteLine($"Team name {details.TeamName} \n" +
                $"Team ID: {details.TeamID}\n" +
                $"List of Developers on Team: \n");
            List<Developer> listOfDevelopers = details.TeamMembers;
            foreach (Developer developer in listOfDevelopers) {
                Console.WriteLine($"Name:  {developer.Name}\n");
            }
        }
    }
}
