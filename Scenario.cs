using System;
using System.Collections.Generic;
using System.Text;

namespace standfordprisiongame
{
    public class Scenario
    {
        public string Description { get; set; } = "";

        // Button prompts for Prisoners
        public string PrisonerChoiceA { get; set; } = ""; // Obey option
        public string PrisonerChoiceB { get; set; } = ""; // Rebel option

        // Button prompts for Guards
        public string GuardChoiceA { get; set; } = ""; // Enforce option
        public string GuardChoiceB { get; set; } = ""; // Leniency option

        // Stat impacts [Stress, Stability]
        public double[] ImpactA { get; set; } = new double[2];
        public double[] ImpactB { get; set; } = new double[2];

        public Scenario(string desc, string pA, string pB, string gA, string gB, double[] impA, double[] impB)
        {
            Description = desc;
            PrisonerChoiceA = pA;
            PrisonerChoiceB = pB;
            GuardChoiceA = gA;
            GuardChoiceB = gB;
            ImpactA = impA;
            ImpactB = impB;
        }
    }
}
