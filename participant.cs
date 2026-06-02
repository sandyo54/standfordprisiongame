using System;
using System.Collections.Generic;
using System.Text;

namespace standfordprisiongame
{
    // First, define the roles so the game knows who is who
    public enum Role { Guard, Prisoner }

    public class Player
    {
        // Identification, sets prisoner number if prisoner
        public string ID { get; set; } = "Prisoner #819";
        public Role AssignedRole { get; set; } = Role.Prisoner;

        // Psychological Stats
        public double MentalStability { get; set; } = 100.0;
        public double StressLevel { get; set; } = 0.0;
        public double Compliance { get; set; } = 100.0; // How much they follow orders

        // Status States
        public bool IsDistressed => MentalStability < 30;
        public bool HasBrokenDown { get; private set; } = false;
        public bool HasEscaped { get; set; } = false;

        ///explanation:
        /// This handles the "Mental Suffering" logic.
        /// As stress goes up, stability goes down.

        public void ProcessPsychologicalImpact(double stressIncr, double stabilityDecr)
        {
            StressLevel += stressIncr;
            MentalStability -= stabilityDecr;
            //method ensures the progress bars wont break
            // Clamp values between 0 and 100
            StressLevel = Math.Clamp(StressLevel, 0, 100);
            MentalStability = Math.Clamp(MentalStability, 0, 100);

            if (MentalStability <= 0)
            {
                HasBrokenDown = true;
            }
        }

        ///explanation:
        /// Logic for an escape attempt.
        /// Success is harder if stress is high or stability is low.
        
        public bool AttemptEscape()
        {
            Random rng = new Random();
            // Chance calculation: Higher stability and lower stress help focus
            double escapeChance = (MentalStability * 0.5) - (StressLevel * 0.2);

            // Random roll vs the calculated chance
            if (rng.Next(0, 100) < escapeChance)
            {
                HasEscaped = true;
                return true;
            }

            // If failed, suffer more mental trauma
            ProcessPsychologicalImpact(20, 15);
            return false;
        }
    }
}
