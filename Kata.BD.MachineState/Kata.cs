using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Kata.BD.MachineState
{
    public class Kata
    {
        private const int INDEX_INITIAL_STATE = 0;
        private const int INDEX_EVENT = 1;
        private const int INDEX_NEW_STATE = 2;
        public static string MachineState(string input)
        {
            /// Arrange
            string[] states = input.Split(' ').Select(x => x.Trim()).ToArray();
            
            List<List<string>> actions = GetActions(input);
            string newState = "CLOSED";
            int i = 0;
            
            /// Filtering by states
            foreach (var state in states)
            {
                var actionsByDefault = actions
                .Where(x =>
                    x[INDEX_INITIAL_STATE].Equals(newState) &&
                    x[INDEX_EVENT].Equals(states[i]))
                .FirstOrDefault();

                if (actionsByDefault == null)
                    return "ERROR";

                newState = actionsByDefault[INDEX_NEW_STATE];
                i++;
            }

            /// Response
            return newState;
        }

        private static List<List<string>> GetActions(string input)
        {
            string currentPath = Directory.GetCurrentDirectory();
            var content = File.ReadAllLines(Path.Combine(currentPath, "ACTIONS.txt"));
            List<string> splitedLine;
            List<List<string>> actions = new List<List<string>>();
            foreach (var line in content)
            {
                splitedLine = line.Split(new string[] { ":", "->" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                if (splitedLine.Any())
                    actions.Add(splitedLine);
            }
            return actions;
        }
    }
}
